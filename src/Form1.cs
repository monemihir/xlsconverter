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
using System.Configuration;
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
  public partial class Form1 : Form, IObserver
  {
    private static Guid DownloadsFolderGuid = new Guid("374DE290-123F-4565-9164-39C4925E467B");
    private readonly DataProcessor m_dataProcessor;

    /// <summary>
    ///   Constructor
    /// </summary>
    public Form1()
    {
      InitializeComponent();

      EnableExportActions(false);

      txtOutputFolder.Enabled = false;

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

      txtSingleFile.Text = Path.Combine(Constants.Paths.CacheDirectory, Constants.SampleMembershipDataFileName);
      txtOutputFolder.Text = GetDownloadsPath();

      Random rand = new Random(DateTime.Now.Millisecond);

      m_dataProcessor.WriteSampleMemberships(rand.Next(50, 400));
      m_dataProcessor.WriteSampleOrders(rand.Next(50, 400));
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

    private void EnableExportActions(bool enable = true)
    {
      btnConvertToXls.Enabled = enable;
      //btnMemberDirectory.Enabled = enable;
    }

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

    private void btnSelectFile_Click(object sender, EventArgs e)
    {
      selectFileDialog.ShowDialog();

      txtSingleFile.Text = selectFileDialog.FileName;
    }

    private void txtSingleFile_TextChanged(object sender, EventArgs e)
    {
      if (File.Exists(txtSingleFile.Text))
      {
        txtOutputFolder.Text = Path.GetDirectoryName(txtSingleFile.Text);
        EnableExportActions();
      }
      else
        EnableExportActions(false);
    }

    private void btnConvert_Click(object sender, EventArgs e)
    {
      EnableExportActions(false);

      Thread t = new Thread(() =>
      {
        string outputFileName = string.Format("{0}\\{1}.xlsx", txtOutputFolder.Text, Path.GetFileNameWithoutExtension(txtSingleFile.Text));
        m_dataProcessor.ConvertToXls(txtSingleFile.Text, outputFileName);
      });
      t.Start();
    }

    private void btnSelectOutputFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(txtSingleFile.Text))
        selectFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

      selectFolderDialog.Description = "Select Output Folder";

      selectFolderDialog.ShowDialog();

      txtOutputFolder.Text = selectFolderDialog.SelectedPath;
    }

    #region Implementation of IObserver

    /// <inheritdoc />
    public void Notify(int progress)
    {
      this.RunOnUiThread(f =>
      {
        f.SetProgress(progress);
        if (progress == 100)
          f.EnableExportActions();
      });
    }

    #endregion

    private void btnMemberDirectory_Click(object sender, EventArgs e)
    {
      SetProgress(0);
      Membership[] members = Membership.ProcessFile(Path.GetFullPath(txtSingleFile.Text));
      SetProgress(50);

      string outputFilePath = string.Format("{0}\\{1}.pdf", txtOutputFolder.Text, Path.GetFileNameWithoutExtension(txtSingleFile.Text));
      m_dataProcessor.MakeMembershipDirectory(members, outputFilePath);
      SetProgress(100);
    }
  }
}