namespace MMVIC
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.selectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnSelectMDInputFile = new System.Windows.Forms.Button();
      this.grpConvertFile = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.mdInputFile = new System.Windows.Forms.TextBox();
      this.grpOutputFolder = new System.Windows.Forms.GroupBox();
      this.mdOutputFolder = new System.Windows.Forms.TextBox();
      this.btnSelectMDOutputFolder = new System.Windows.Forms.Button();
      this.btnConvertToXls = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.btnCreateMemberDirectory = new System.Windows.Forms.Button();
      this.txtProgress = new System.Windows.Forms.Label();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.xlsTab = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.xlsConvertInputFile = new System.Windows.Forms.TextBox();
      this.btnSelectXLSConvertInputFile = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.xlsConvertOutputFolder = new System.Windows.Forms.TextBox();
      this.btnSelectXLSConvertOutputFolder = new System.Windows.Forms.Button();
      this.memberDirectoryTab = new System.Windows.Forms.TabPage();
      this.chkPageNumbers = new System.Windows.Forms.CheckBox();
      this.numPageOffset = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.mdReportHeader = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.grpConvertFile.SuspendLayout();
      this.grpOutputFolder.SuspendLayout();
      this.tabControl.SuspendLayout();
      this.xlsTab.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.memberDirectoryTab.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPageOffset)).BeginInit();
      this.SuspendLayout();
      // 
      // btnSelectMDInputFile
      // 
      this.btnSelectMDInputFile.Location = new System.Drawing.Point(467, 26);
      this.btnSelectMDInputFile.Name = "btnSelectMDInputFile";
      this.btnSelectMDInputFile.Size = new System.Drawing.Size(75, 23);
      this.btnSelectMDInputFile.TabIndex = 0;
      this.btnSelectMDInputFile.Text = "Browse";
      this.btnSelectMDInputFile.UseVisualStyleBackColor = true;
      this.btnSelectMDInputFile.Click += new System.EventHandler(this.btnSelectMDInputFile_Click);
      // 
      // grpConvertFile
      // 
      this.grpConvertFile.Controls.Add(this.label1);
      this.grpConvertFile.Controls.Add(this.mdInputFile);
      this.grpConvertFile.Controls.Add(this.btnSelectMDInputFile);
      this.grpConvertFile.Location = new System.Drawing.Point(6, 6);
      this.grpConvertFile.Name = "grpConvertFile";
      this.grpConvertFile.Size = new System.Drawing.Size(549, 77);
      this.grpConvertFile.TabIndex = 1;
      this.grpConvertFile.TabStop = false;
      this.grpConvertFile.Text = "Select file to convert";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(7, 55);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(233, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "(Select a *.psv file by clicking the Browse button)";
      // 
      // mdInputFile
      // 
      this.mdInputFile.Location = new System.Drawing.Point(7, 28);
      this.mdInputFile.Name = "mdInputFile";
      this.mdInputFile.Size = new System.Drawing.Size(454, 20);
      this.mdInputFile.TabIndex = 1;
      // 
      // grpOutputFolder
      // 
      this.grpOutputFolder.Controls.Add(this.mdOutputFolder);
      this.grpOutputFolder.Controls.Add(this.btnSelectMDOutputFolder);
      this.grpOutputFolder.Location = new System.Drawing.Point(6, 89);
      this.grpOutputFolder.Name = "grpOutputFolder";
      this.grpOutputFolder.Size = new System.Drawing.Size(549, 69);
      this.grpOutputFolder.TabIndex = 3;
      this.grpOutputFolder.TabStop = false;
      this.grpOutputFolder.Text = "Select output folder";
      // 
      // mdOutputFolder
      // 
      this.mdOutputFolder.Location = new System.Drawing.Point(7, 28);
      this.mdOutputFolder.Name = "mdOutputFolder";
      this.mdOutputFolder.Size = new System.Drawing.Size(454, 20);
      this.mdOutputFolder.TabIndex = 5;
      // 
      // btnSelectMDOutputFolder
      // 
      this.btnSelectMDOutputFolder.Location = new System.Drawing.Point(467, 26);
      this.btnSelectMDOutputFolder.Name = "btnSelectMDOutputFolder";
      this.btnSelectMDOutputFolder.Size = new System.Drawing.Size(75, 23);
      this.btnSelectMDOutputFolder.TabIndex = 4;
      this.btnSelectMDOutputFolder.Text = "Browse";
      this.btnSelectMDOutputFolder.UseVisualStyleBackColor = true;
      this.btnSelectMDOutputFolder.Click += new System.EventHandler(this.btnSelectMDOutputFolder_Click);
      // 
      // btnConvertToXls
      // 
      this.btnConvertToXls.Location = new System.Drawing.Point(380, 265);
      this.btnConvertToXls.Name = "btnConvertToXls";
      this.btnConvertToXls.Size = new System.Drawing.Size(175, 25);
      this.btnConvertToXls.TabIndex = 4;
      this.btnConvertToXls.Text = "Convert to XLS";
      this.btnConvertToXls.UseVisualStyleBackColor = true;
      this.btnConvertToXls.Click += new System.EventHandler(this.btnConvertToXls_Click);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(12, 341);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(533, 22);
      this.progressBar1.TabIndex = 5;
      // 
      // btnCreateMemberDirectory
      // 
      this.btnCreateMemberDirectory.Location = new System.Drawing.Point(380, 268);
      this.btnCreateMemberDirectory.Name = "btnCreateMemberDirectory";
      this.btnCreateMemberDirectory.Size = new System.Drawing.Size(175, 25);
      this.btnCreateMemberDirectory.TabIndex = 6;
      this.btnCreateMemberDirectory.Text = "Create member directory PDF";
      this.btnCreateMemberDirectory.UseVisualStyleBackColor = true;
      this.btnCreateMemberDirectory.Click += new System.EventHandler(this.btnMemberDirectory_Click);
      // 
      // txtProgress
      // 
      this.txtProgress.AutoSize = true;
      this.txtProgress.Location = new System.Drawing.Point(551, 344);
      this.txtProgress.Name = "txtProgress";
      this.txtProgress.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
      this.txtProgress.Size = new System.Drawing.Size(21, 15);
      this.txtProgress.TabIndex = 7;
      this.txtProgress.Text = "0%";
      // 
      // tabControl
      // 
      this.tabControl.Controls.Add(this.xlsTab);
      this.tabControl.Controls.Add(this.memberDirectoryTab);
      this.tabControl.Location = new System.Drawing.Point(13, 13);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(569, 322);
      this.tabControl.TabIndex = 8;
      // 
      // xlsTab
      // 
      this.xlsTab.Controls.Add(this.groupBox2);
      this.xlsTab.Controls.Add(this.groupBox1);
      this.xlsTab.Controls.Add(this.btnConvertToXls);
      this.xlsTab.Location = new System.Drawing.Point(4, 22);
      this.xlsTab.Name = "xlsTab";
      this.xlsTab.Padding = new System.Windows.Forms.Padding(3);
      this.xlsTab.Size = new System.Drawing.Size(561, 296);
      this.xlsTab.TabIndex = 0;
      this.xlsTab.Text = "XLS Convert";
      this.xlsTab.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.xlsConvertInputFile);
      this.groupBox2.Controls.Add(this.btnSelectXLSConvertInputFile);
      this.groupBox2.Location = new System.Drawing.Point(6, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(549, 77);
      this.groupBox2.TabIndex = 3;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Select file to convert";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(7, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(233, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "(Select a *.psv file by clicking the Browse button)";
      // 
      // xlsConvertInputFile
      // 
      this.xlsConvertInputFile.Location = new System.Drawing.Point(7, 28);
      this.xlsConvertInputFile.Name = "xlsConvertInputFile";
      this.xlsConvertInputFile.Size = new System.Drawing.Size(454, 20);
      this.xlsConvertInputFile.TabIndex = 1;
      // 
      // btnSelectXLSConvertInputFile
      // 
      this.btnSelectXLSConvertInputFile.Location = new System.Drawing.Point(467, 26);
      this.btnSelectXLSConvertInputFile.Name = "btnSelectXLSConvertInputFile";
      this.btnSelectXLSConvertInputFile.Size = new System.Drawing.Size(75, 23);
      this.btnSelectXLSConvertInputFile.TabIndex = 0;
      this.btnSelectXLSConvertInputFile.Text = "Browse";
      this.btnSelectXLSConvertInputFile.UseVisualStyleBackColor = true;
      this.btnSelectXLSConvertInputFile.Click += new System.EventHandler(this.btnSelectXLSConvertInputFile_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.xlsConvertOutputFolder);
      this.groupBox1.Controls.Add(this.btnSelectXLSConvertOutputFolder);
      this.groupBox1.Location = new System.Drawing.Point(6, 89);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(549, 69);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select output folder";
      // 
      // xlsConvertOutputFolder
      // 
      this.xlsConvertOutputFolder.Location = new System.Drawing.Point(7, 28);
      this.xlsConvertOutputFolder.Name = "xlsConvertOutputFolder";
      this.xlsConvertOutputFolder.Size = new System.Drawing.Size(454, 20);
      this.xlsConvertOutputFolder.TabIndex = 5;
      // 
      // btnSelectXLSConvertOutputFolder
      // 
      this.btnSelectXLSConvertOutputFolder.Location = new System.Drawing.Point(467, 26);
      this.btnSelectXLSConvertOutputFolder.Name = "btnSelectXLSConvertOutputFolder";
      this.btnSelectXLSConvertOutputFolder.Size = new System.Drawing.Size(75, 23);
      this.btnSelectXLSConvertOutputFolder.TabIndex = 4;
      this.btnSelectXLSConvertOutputFolder.Text = "Browse";
      this.btnSelectXLSConvertOutputFolder.UseVisualStyleBackColor = true;
      this.btnSelectXLSConvertOutputFolder.Click += new System.EventHandler(this.btnSelectXLSConvertOutputFolder_Click);
      // 
      // memberDirectoryTab
      // 
      this.memberDirectoryTab.Controls.Add(this.chkPageNumbers);
      this.memberDirectoryTab.Controls.Add(this.numPageOffset);
      this.memberDirectoryTab.Controls.Add(this.label4);
      this.memberDirectoryTab.Controls.Add(this.mdReportHeader);
      this.memberDirectoryTab.Controls.Add(this.label3);
      this.memberDirectoryTab.Controls.Add(this.grpOutputFolder);
      this.memberDirectoryTab.Controls.Add(this.grpConvertFile);
      this.memberDirectoryTab.Controls.Add(this.btnCreateMemberDirectory);
      this.memberDirectoryTab.Location = new System.Drawing.Point(4, 22);
      this.memberDirectoryTab.Name = "memberDirectoryTab";
      this.memberDirectoryTab.Padding = new System.Windows.Forms.Padding(3);
      this.memberDirectoryTab.Size = new System.Drawing.Size(561, 296);
      this.memberDirectoryTab.TabIndex = 1;
      this.memberDirectoryTab.Text = "Members Directory";
      this.memberDirectoryTab.UseVisualStyleBackColor = true;
      // 
      // chkPageNumbers
      // 
      this.chkPageNumbers.AutoSize = true;
      this.chkPageNumbers.Checked = true;
      this.chkPageNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkPageNumbers.Location = new System.Drawing.Point(6, 217);
      this.chkPageNumbers.Name = "chkPageNumbers";
      this.chkPageNumbers.Size = new System.Drawing.Size(123, 17);
      this.chkPageNumbers.TabIndex = 12;
      this.chkPageNumbers.Text = "Show page numbers";
      this.chkPageNumbers.UseVisualStyleBackColor = true;
      this.chkPageNumbers.CheckedChanged += new System.EventHandler(this.chkPageNumbers_CheckedChanged);
      // 
      // numPageOffset
      // 
      this.numPageOffset.Location = new System.Drawing.Point(261, 216);
      this.numPageOffset.Name = "numPageOffset";
      this.numPageOffset.Size = new System.Drawing.Size(48, 20);
      this.numPageOffset.TabIndex = 11;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(156, 218);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(99, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "Page number offset";
      // 
      // mdReportHeader
      // 
      this.mdReportHeader.Location = new System.Drawing.Point(7, 182);
      this.mdReportHeader.Name = "mdReportHeader";
      this.mdReportHeader.Size = new System.Drawing.Size(548, 20);
      this.mdReportHeader.TabIndex = 9;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 165);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Page header text";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(592, 378);
      this.Controls.Add(this.tabControl);
      this.Controls.Add(this.txtProgress);
      this.Controls.Add(this.progressBar1);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MMVIC Report Generator";
      this.grpConvertFile.ResumeLayout(false);
      this.grpConvertFile.PerformLayout();
      this.grpOutputFolder.ResumeLayout(false);
      this.grpOutputFolder.PerformLayout();
      this.tabControl.ResumeLayout(false);
      this.xlsTab.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.memberDirectoryTab.ResumeLayout(false);
      this.memberDirectoryTab.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPageOffset)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FolderBrowserDialog selectFolderDialog;
    private System.Windows.Forms.OpenFileDialog selectFileDialog;
    private System.Windows.Forms.Button btnSelectMDInputFile;
    private System.Windows.Forms.GroupBox grpConvertFile;
    private System.Windows.Forms.TextBox mdInputFile;
    private System.Windows.Forms.GroupBox grpOutputFolder;
    private System.Windows.Forms.TextBox mdOutputFolder;
    private System.Windows.Forms.Button btnSelectMDOutputFolder;
    private System.Windows.Forms.Button btnConvertToXls;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCreateMemberDirectory;
    private System.Windows.Forms.Label txtProgress;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage xlsTab;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox xlsConvertInputFile;
    private System.Windows.Forms.Button btnSelectXLSConvertInputFile;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox xlsConvertOutputFolder;
    private System.Windows.Forms.Button btnSelectXLSConvertOutputFolder;
    private System.Windows.Forms.TabPage memberDirectoryTab;
    private System.Windows.Forms.TextBox mdReportHeader;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox chkPageNumbers;
    private System.Windows.Forms.NumericUpDown numPageOffset;
    private System.Windows.Forms.Label label4;
  }
}

