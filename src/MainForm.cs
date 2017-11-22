// 
// This file is part of - MMVIC Report Generator
// Copyright 2017 Mihir Mone
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MMVIC.Models;
using MMVIC.Properties;

namespace MMVIC
{
  /// <summary>
  ///   Main form
  /// </summary>
  public partial class MainForm : Form, IObserver
  {
    private static Guid DownloadsFolderGuid = new Guid("374DE290-123F-4565-9164-39C4925E467B");
    private readonly DataProcessor m_dataProcessor;

    /// <summary>
    ///   Constructor
    /// </summary>
    public MainForm()
    {
      InitializeComponent();

      xlsConvertOutputFolder.Enabled = false;
      mdOutputFolder.Enabled = false;

      selectFileDialog.AddExtension = true;
      selectFileDialog.DefaultExt = "psv";
      selectFileDialog.InitialDirectory = GetDownloadsPath();
      selectFileDialog.Filter = "Pipe separated files (*.psv)|*.psv";

      progressBar1.Minimum = 0;
      progressBar1.Maximum = 100;

      m_dataProcessor = new DataProcessor();
      m_dataProcessor.RegisterObserver(this);

      if (!Settings.Default.EnableTestMode)
        return;

      // set defaults for xls convert tab
      xlsConvertInputFile.Text = Path.Combine(Constants.Paths.CacheDirectory, Constants.SampleOrdersDataFileName);
      xlsConvertOutputFolder.Text = GetDownloadsPath();

      // set defaults for member directory tab
      mdInputFile.Text = Path.Combine(Constants.Paths.CacheDirectory, Constants.SampleMembershipDataFileName);
      mdOutputFolder.Text = GetDownloadsPath();
      mdReportHeader.Text = "MMVIC Members Directory " + DateTime.Now.Year;


      Random rand = new Random(DateTime.Now.Millisecond);

      m_dataProcessor.WriteSampleMemberships(rand.Next(50, 400));
      m_dataProcessor.WriteSampleOrders(rand.Next(50, 400));
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

    private void SetProgress(int percentage)
    {
      progressBar1.Value = percentage;
      txtProgress.Text = percentage + "%";
    }

    private string GetDownloadsPath()
    {
      string path = null;
      if (Environment.OSVersion.Version.Major >= 6)
      {
        IntPtr pathPtr;
        int hr = SHGetKnownFolderPath(ref DownloadsFolderGuid, 0, IntPtr.Zero, out pathPtr);
        if (hr == 0)
        {
          path = Marshal.PtrToStringUni(pathPtr);
          Marshal.FreeCoTaskMem(pathPtr);
          return path;
        }
      }
      path = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
      path = Path.Combine(path, "Downloads");
      return path;
    }

    #region XLS Convert Tasks

    private void btnSelectXLSConvertInputFile_Click(object sender, EventArgs e)
    {
      selectFileDialog.ShowDialog();

      xlsConvertInputFile.Text = selectFileDialog.FileName;
    }

    private void btnSelectXLSConvertOutputFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(xlsConvertInputFile.Text))
        selectFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

      selectFolderDialog.Description = "Select Output Folder";

      selectFolderDialog.ShowDialog();

      xlsConvertOutputFolder.Text = selectFolderDialog.SelectedPath;
    }

    private void btnConvertToXls_Click(object sender, EventArgs e)
    {
      btnConvertToXls.Enabled = false;

      if (!File.Exists(xlsConvertInputFile.Text))
      {
        MessageBox.Show("Input file not found :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Thread t = new Thread(() =>
      {
        string outputFileName = string.Format("{0}\\{1}.xlsx", xlsConvertOutputFolder.Text, Path.GetFileNameWithoutExtension(xlsConvertInputFile.Text));
        m_dataProcessor.ConvertToXls(xlsConvertInputFile.Text, outputFileName);
      });
      t.Start();
    }

    #endregion XLS Convert Tasks

    #region Implementation of IObserver

    /// <inheritdoc />
    public void Notify(int progress)
    {
      this.RunOnUiThread(f =>
      {
        f.SetProgress(progress);
        if (progress == 100)
        {
          f.btnConvertToXls.Enabled = true;
          f.btnCreateMemberDirectory.Enabled = true;
        }
      });
    }

    #endregion

    #region MemberDirectory Tasks

    private void btnSelectMDInputFile_Click(object sender, EventArgs e)
    {
      selectFileDialog.ShowDialog();

      mdInputFile.Text = selectFileDialog.FileName;
    }

    private void btnSelectMDOutputFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(mdInputFile.Text))
        selectFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

      selectFolderDialog.Description = "Select Output Folder";

      selectFolderDialog.ShowDialog();

      mdOutputFolder.Text = selectFolderDialog.SelectedPath;
    }

    private void btnMemberDirectory_Click(object sender, EventArgs e)
    {
      if (!File.Exists(mdInputFile.Text))
      {
        MessageBox.Show("Input file not found :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      btnCreateMemberDirectory.Enabled = false;

      Thread t = new Thread(() =>
      {
        Notify(0);
        Membership[] members = Membership.ProcessFile(Path.GetFullPath(mdInputFile.Text));
        Notify(50);

        string outputFilePath = string.Format("{0}\\{1}.pdf", mdOutputFolder.Text, Path.GetFileNameWithoutExtension(mdInputFile.Text));
        var conf = new MemberDirectoryConfig
        {
          OutputFilePath = outputFilePath,
          HeaderText = mdReportHeader.Text,
          EnableFooter = chkPageNumbers.Checked,
          PageOffset = (int)numPageOffset.Value
        };

        m_dataProcessor.MakeMembershipDirectory(members, conf);
      });
      t.Start();
    }

    #endregion MemberDirectory Tasks

    private void chkPageNumbers_CheckedChanged(object sender, EventArgs e)
    {
      numPageOffset.Enabled = chkPageNumbers.Checked;
    }
  }
}