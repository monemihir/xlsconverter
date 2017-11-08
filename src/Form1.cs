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
using System.Threading.Tasks;
using System.Windows.Forms;
using MMVIC.Models;
using OfficeOpenXml;

namespace MMVIC
{
  public partial class Form1 : Form, IObserver
  {
    private static Guid DownloadsFolderGuid = new Guid("374DE290-123F-4565-9164-39C4925E467B");
    private volatile int m_currentProgress;
    private readonly DataProcessor m_dataProcessor;

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

      SampleDataGenerator.WriteSampleOrders(120);
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

    private void EnableExportActions(bool enable = true)
    {
      btnConvertToXls.Enabled = enable;
      btnMemberDirectory.Enabled = enable;
    }

    private void SetProgress(int percentage)
    {
      progressBar1.Value = percentage;
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
        m_dataProcessor.MakeOrdersXls(txtSingleFile.Text, outputFileName);
      });
      t.Start();
    }

    private void DoWork()
    {
      m_currentProgress = 5;
      this.RunOnUiThread(f =>
      {
        EnableExportActions(false);
        SetProgress(m_currentProgress);
      });

      string[] lines = File.ReadAllLines(txtSingleFile.Text);
      m_currentProgress += 10;
      this.RunOnUiThread(f => SetProgress(m_currentProgress));

      int valA = 'A', valZ = 'Z';

      ExcelPackage pkg = new ExcelPackage();
      ExcelWorksheet dataSheet = pkg.Workbook.Worksheets.Add("Orders");

      int progressAvailable = progressBar1.Maximum - m_currentProgress;
      int progressStepSize = (int)(1.0 / lines.Length * progressAvailable);
      for (int i = 0; i < lines.Length; i++)
      {
        int rowNum = i + 1;
        string line = lines[i];
        string[] buff = line.Split('|');

        int colNum = valA;
        foreach (string val in buff)
        {
          string address = (char)colNum + rowNum.ToString();

          object finalValue = val;

          // check if double
          double dblValue;
          bool parseOk = double.TryParse(val, out dblValue);

          if (parseOk)
            finalValue = Math.Abs(dblValue % 1) < double.Epsilon ? (int)dblValue : dblValue;

          dataSheet.Cells[address].Value = finalValue;

          colNum++;

          if (colNum > valZ)
            throw new OverflowException("Can not handle more than 26 columns");
        }

        m_currentProgress += progressStepSize;
        this.RunOnUiThread(f => SetProgress(m_currentProgress));
      }

      m_currentProgress = 100;
      this.RunOnUiThread(f => SetProgress(m_currentProgress));
      pkg.SaveAs(new FileInfo(string.Format("{0}\\{1}.xlsx", txtOutputFolder.Text, Path.GetFileNameWithoutExtension(txtSingleFile.Text))));

      pkg.Dispose();
      MessageBox.Show("Converting succeeded");

      this.RunOnUiThread(f => EnableExportActions());
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
  }
}