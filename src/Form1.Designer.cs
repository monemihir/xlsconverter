namespace XLSConverter
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
      this.selectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnSelectFile = new System.Windows.Forms.Button();
      this.grpConvertFile = new System.Windows.Forms.GroupBox();
      this.txtSingleFile = new System.Windows.Forms.TextBox();
      this.grpOutputFolder = new System.Windows.Forms.GroupBox();
      this.txtOutputFolder = new System.Windows.Forms.TextBox();
      this.btnSelectOutputFolder = new System.Windows.Forms.Button();
      this.btnConvert = new System.Windows.Forms.Button();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
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
      this.grpConvertFile.Text = "Convert A File";
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
      this.grpOutputFolder.Text = "Select Output Folder";
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
      // btnConvert
      // 
      this.btnConvert.Location = new System.Drawing.Point(480, 200);
      this.btnConvert.Name = "btnConvert";
      this.btnConvert.Size = new System.Drawing.Size(81, 22);
      this.btnConvert.TabIndex = 4;
      this.btnConvert.Text = "Convert";
      this.btnConvert.UseVisualStyleBackColor = true;
      this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(13, 200);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(461, 22);
      this.progressBar1.TabIndex = 5;
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
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(573, 240);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.btnConvert);
      this.Controls.Add(this.grpOutputFolder);
      this.Controls.Add(this.grpConvertFile);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Select File";
      this.grpConvertFile.ResumeLayout(false);
      this.grpConvertFile.PerformLayout();
      this.grpOutputFolder.ResumeLayout(false);
      this.grpOutputFolder.PerformLayout();
      this.ResumeLayout(false);

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
    private System.Windows.Forms.Button btnConvert;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label1;
  }
}

