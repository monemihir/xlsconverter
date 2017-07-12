using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using OfficeOpenXml;

namespace XLSConverter
{
  public partial class Form1 : Form
  {
    private static Guid m_folderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");

    public Form1()
    {
      InitializeComponent();

      EnableConverting(false);

      txtOutputFolder.Enabled = false;

      selectFileDialog.AddExtension = true;
      selectFileDialog.DefaultExt = "psv";
      selectFileDialog.InitialDirectory = GetDownloadsPath();
      selectFileDialog.Filter = "Pipe separated files (*.psv)|*.psv";

      progressBar1.Minimum = 0;
      progressBar1.Maximum = 100;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

    private void EnableConverting(bool enable = true)
    {
      btnConvert.Enabled = enable;
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
        int hr = SHGetKnownFolderPath(ref m_folderDownloads, 0, IntPtr.Zero, out pathPtr);
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
        EnableConverting();
      }
      else
        EnableConverting(false);
    }

    private void btnConvert_Click(object sender, EventArgs e)
    {
      Thread t = new Thread(DoWork);
      t.Start();
    }

    private void DoWork()
    {
      int currentProgress = 5;
      this.RunOnUiThread(f =>
      {
        EnableConverting(false);
        SetProgress(currentProgress);
      });

      string[] lines = File.ReadAllLines(txtSingleFile.Text);
      currentProgress += 10;
      this.RunOnUiThread(f => SetProgress(currentProgress));

      int valA = 'A', valZ = 'Z';

      ExcelPackage pkg = new ExcelPackage();
      ExcelWorksheet dataSheet = pkg.Workbook.Worksheets.Add("Orders");

      int progressAvailable = progressBar1.Maximum - currentProgress;
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

        currentProgress += progressStepSize;
        this.RunOnUiThread(f => SetProgress(currentProgress));
      }

      currentProgress = 100;
      this.RunOnUiThread(f => SetProgress(currentProgress));
      pkg.SaveAs(new FileInfo(string.Format("{0}\\{1}.xlsx", txtOutputFolder.Text, Path.GetFileNameWithoutExtension(txtSingleFile.Text))));

      pkg.Dispose();
      MessageBox.Show("Converting succeeded");

      this.RunOnUiThread(f => EnableConverting());
    }

    private void btnSelectOutputFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(txtSingleFile.Text))
        selectFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

      selectFolderDialog.Description = "Select Output Folder";

      selectFolderDialog.ShowDialog();

      txtOutputFolder.Text = selectFolderDialog.SelectedPath;
    }
  }
}