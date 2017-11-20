namespace MMVIC
{
  partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.selectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnSelectFile = new System.Windows.Forms.Button();
      this.grpConvertFile = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtSingleFile = new System.Windows.Forms.TextBox();
      this.grpOutputFolder = new System.Windows.Forms.GroupBox();
      this.txtOutputFolder = new System.Windows.Forms.TextBox();
      this.btnSelectOutputFolder = new System.Windows.Forms.Button();
      this.btnConvertToXls = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.btnMemberDirectory = new System.Windows.Forms.Button();
      this.txtProgress = new System.Windows.Forms.Label();
      this.btnSampleData = new System.Windows.Forms.Button();
      this.grpConvertFile.SuspendLayout();
      this.grpOutputFolder.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSelectFile
      // 
      this.btnSelectFile.Location = new System.Drawing.Point(467, 26);
      this.btnSelectFile.Name = "btnSelectFile";
      this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
      this.btnSelectFile.TabIndex = 0;
      this.btnSelectFile.Text = "Browse";
      this.btnSelectFile.UseVisualStyleBackColor = true;
      this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
      // 
      // grpConvertFile
      // 
      this.grpConvertFile.Controls.Add(this.label1);
      this.grpConvertFile.Controls.Add(this.txtSingleFile);
      this.grpConvertFile.Controls.Add(this.btnSelectFile);
      this.grpConvertFile.Location = new System.Drawing.Point(13, 13);
      this.grpConvertFile.Name = "grpConvertFile";
      this.grpConvertFile.Size = new System.Drawing.Size(548, 77);
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
      // txtSingleFile
      // 
      this.txtSingleFile.Location = new System.Drawing.Point(7, 28);
      this.txtSingleFile.Name = "txtSingleFile";
      this.txtSingleFile.Size = new System.Drawing.Size(454, 20);
      this.txtSingleFile.TabIndex = 1;
      this.txtSingleFile.TextChanged += new System.EventHandler(this.txtSingleFile_TextChanged);
      // 
      // grpOutputFolder
      // 
      this.grpOutputFolder.Controls.Add(this.txtOutputFolder);
      this.grpOutputFolder.Controls.Add(this.btnSelectOutputFolder);
      this.grpOutputFolder.Location = new System.Drawing.Point(13, 105);
      this.grpOutputFolder.Name = "grpOutputFolder";
      this.grpOutputFolder.Size = new System.Drawing.Size(548, 69);
      this.grpOutputFolder.TabIndex = 3;
      this.grpOutputFolder.TabStop = false;
      this.grpOutputFolder.Text = "Select output folder";
      // 
      // txtOutputFolder
      // 
      this.txtOutputFolder.Location = new System.Drawing.Point(7, 28);
      this.txtOutputFolder.Name = "txtOutputFolder";
      this.txtOutputFolder.Size = new System.Drawing.Size(454, 20);
      this.txtOutputFolder.TabIndex = 5;
      // 
      // btnSelectOutputFolder
      // 
      this.btnSelectOutputFolder.Location = new System.Drawing.Point(467, 26);
      this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
      this.btnSelectOutputFolder.Size = new System.Drawing.Size(75, 23);
      this.btnSelectOutputFolder.TabIndex = 4;
      this.btnSelectOutputFolder.Text = "Browse";
      this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
      this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
      // 
      // btnConvertToXls
      // 
      this.btnConvertToXls.Location = new System.Drawing.Point(194, 194);
      this.btnConvertToXls.Name = "btnConvertToXls";
      this.btnConvertToXls.Size = new System.Drawing.Size(175, 44);
      this.btnConvertToXls.TabIndex = 4;
      this.btnConvertToXls.Text = "Convert to XLS";
      this.btnConvertToXls.UseVisualStyleBackColor = true;
      this.btnConvertToXls.Click += new System.EventHandler(this.btnConvert_Click);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(13, 259);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(507, 22);
      this.progressBar1.TabIndex = 5;
      // 
      // btnMemberDirectory
      // 
      this.btnMemberDirectory.Location = new System.Drawing.Point(13, 194);
      this.btnMemberDirectory.Name = "btnMemberDirectory";
      this.btnMemberDirectory.Size = new System.Drawing.Size(175, 44);
      this.btnMemberDirectory.TabIndex = 6;
      this.btnMemberDirectory.Text = "Create member directory PDF";
      this.btnMemberDirectory.UseVisualStyleBackColor = true;
      this.btnMemberDirectory.Click += new System.EventHandler(this.btnMemberDirectory_Click);
      // 
      // txtProgress
      // 
      this.txtProgress.AutoSize = true;
      this.txtProgress.Location = new System.Drawing.Point(534, 259);
      this.txtProgress.Name = "txtProgress";
      this.txtProgress.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
      this.txtProgress.Size = new System.Drawing.Size(21, 17);
      this.txtProgress.TabIndex = 7;
      this.txtProgress.Text = "0%";
      // 
      // btnSampleData
      // 
      this.btnSampleData.Location = new System.Drawing.Point(375, 194);
      this.btnSampleData.Name = "btnSampleData";
      this.btnSampleData.Size = new System.Drawing.Size(175, 44);
      this.btnSampleData.TabIndex = 8;
      this.btnSampleData.Text = "Write sample data";
      this.btnSampleData.UseVisualStyleBackColor = true;
      this.btnSampleData.Click += new System.EventHandler(this.btnSampleData_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(573, 293);
      this.Controls.Add(this.btnSampleData);
      this.Controls.Add(this.txtProgress);
      this.Controls.Add(this.btnMemberDirectory);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.btnConvertToXls);
      this.Controls.Add(this.grpOutputFolder);
      this.Controls.Add(this.grpConvertFile);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Select File";
      this.grpConvertFile.ResumeLayout(false);
      this.grpConvertFile.PerformLayout();
      this.grpOutputFolder.ResumeLayout(false);
      this.grpOutputFolder.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FolderBrowserDialog selectFolderDialog;
    private System.Windows.Forms.OpenFileDialog selectFileDialog;
    private System.Windows.Forms.Button btnSelectFile;
    private System.Windows.Forms.GroupBox grpConvertFile;
    private System.Windows.Forms.TextBox txtSingleFile;
    private System.Windows.Forms.GroupBox grpOutputFolder;
    private System.Windows.Forms.TextBox txtOutputFolder;
    private System.Windows.Forms.Button btnSelectOutputFolder;
    private System.Windows.Forms.Button btnConvertToXls;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnMemberDirectory;
    private System.Windows.Forms.Label txtProgress;
    private System.Windows.Forms.Button btnSampleData;
  }
}

