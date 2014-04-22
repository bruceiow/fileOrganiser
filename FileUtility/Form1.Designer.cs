namespace FileUtility
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.directorySelection = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.outputDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dlgSourceBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSourceSearch = new System.Windows.Forms.Button();
            this.btnDestSearch = new System.Windows.Forms.Button();
            this.lblException = new System.Windows.Forms.Label();
            this.chkFileTypes = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.grpFolderOptions = new System.Windows.Forms.GroupBox();
            this.rbFolderDay = new System.Windows.Forms.RadioButton();
            this.rbFolderMonth = new System.Windows.Forms.RadioButton();
            this.grpFolderOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(16, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Organise Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Controls";
            // 
            // directorySelection
            // 
            this.directorySelection.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directorySelection.Location = new System.Drawing.Point(16, 55);
            this.directorySelection.Name = "directorySelection";
            this.directorySelection.Size = new System.Drawing.Size(100, 21);
            this.directorySelection.TabIndex = 2;
            this.directorySelection.Text = "D:\\F1 2013";
            this.directorySelection.TextChanged += new System.EventHandler(this.directorySelection_TextChanged);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(13, 347);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(39, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "results";
            // 
            // outputDirectory
            // 
            this.outputDirectory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputDirectory.Location = new System.Drawing.Point(16, 82);
            this.outputDirectory.Name = "outputDirectory";
            this.outputDirectory.Size = new System.Drawing.Size(100, 21);
            this.outputDirectory.TabIndex = 4;
            this.outputDirectory.Text = "D:\\F1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Where are the files you wish  to organise?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(170, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Where would you like the files to output?";
            // 
            // dlgSourceBrowser
            // 
            this.dlgSourceBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnSourceSearch
            // 
            this.btnSourceSearch.Location = new System.Drawing.Point(126, 53);
            this.btnSourceSearch.Name = "btnSourceSearch";
            this.btnSourceSearch.Size = new System.Drawing.Size(25, 23);
            this.btnSourceSearch.TabIndex = 9;
            this.btnSourceSearch.Text = "...";
            this.btnSourceSearch.UseVisualStyleBackColor = true;
            this.btnSourceSearch.Click += new System.EventHandler(this.directorySelection_Click);
            // 
            // btnDestSearch
            // 
            this.btnDestSearch.Location = new System.Drawing.Point(126, 80);
            this.btnDestSearch.Name = "btnDestSearch";
            this.btnDestSearch.Size = new System.Drawing.Size(25, 23);
            this.btnDestSearch.TabIndex = 10;
            this.btnDestSearch.Text = "...";
            this.btnDestSearch.UseVisualStyleBackColor = true;
            this.btnDestSearch.Click += new System.EventHandler(this.btnDestSearch_Click);
            // 
            // lblException
            // 
            this.lblException.AutoSize = true;
            this.lblException.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblException.Location = new System.Drawing.Point(13, 369);
            this.lblException.Name = "lblException";
            this.lblException.Size = new System.Drawing.Size(59, 13);
            this.lblException.TabIndex = 11;
            this.lblException.Text = "exceptions";
            // 
            // chkFileTypes
            // 
            this.chkFileTypes.CheckOnClick = true;
            this.chkFileTypes.FormattingEnabled = true;
            this.chkFileTypes.Location = new System.Drawing.Point(16, 201);
            this.chkFileTypes.Name = "chkFileTypes";
            this.chkFileTypes.Size = new System.Drawing.Size(241, 94);
            this.chkFileTypes.Sorted = true;
            this.chkFileTypes.TabIndex = 13;
            this.chkFileTypes.ThreeDCheckBoxes = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Included File Types";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(126, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 394);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(357, 24);
            this.progressBar.TabIndex = 16;
            // 
            // grpFolderOptions
            // 
            this.grpFolderOptions.Controls.Add(this.rbFolderMonth);
            this.grpFolderOptions.Controls.Add(this.rbFolderDay);
            this.grpFolderOptions.Location = new System.Drawing.Point(19, 110);
            this.grpFolderOptions.Name = "grpFolderOptions";
            this.grpFolderOptions.Size = new System.Drawing.Size(238, 72);
            this.grpFolderOptions.TabIndex = 17;
            this.grpFolderOptions.TabStop = false;
            this.grpFolderOptions.Text = "Output Folders";
            // 
            // rbFolderDay
            // 
            this.rbFolderDay.AutoSize = true;
            this.rbFolderDay.Checked = true;
            this.rbFolderDay.Location = new System.Drawing.Point(12, 19);
            this.rbFolderDay.Name = "rbFolderDay";
            this.rbFolderDay.Size = new System.Drawing.Size(97, 17);
            this.rbFolderDay.TabIndex = 0;
            this.rbFolderDay.TabStop = true;
            this.rbFolderDay.Text = "Folders per day";
            this.rbFolderDay.UseVisualStyleBackColor = true;
            // 
            // rbFolderMonth
            // 
            this.rbFolderMonth.AutoSize = true;
            this.rbFolderMonth.Location = new System.Drawing.Point(12, 42);
            this.rbFolderMonth.Name = "rbFolderMonth";
            this.rbFolderMonth.Size = new System.Drawing.Size(109, 17);
            this.rbFolderMonth.TabIndex = 1;
            this.rbFolderMonth.TabStop = true;
            this.rbFolderMonth.Text = "Folders per month";
            this.rbFolderMonth.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 423);
            this.Controls.Add(this.grpFolderOptions);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkFileTypes);
            this.Controls.Add(this.lblException);
            this.Controls.Add(this.btnDestSearch);
            this.Controls.Add(this.btnSourceSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputDirectory);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.directorySelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "File Organiser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpFolderOptions.ResumeLayout(false);
            this.grpFolderOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directorySelection;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox outputDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog dlgSourceBrowser;
        private System.Windows.Forms.Button btnSourceSearch;
        private System.Windows.Forms.Button btnDestSearch;
        private System.Windows.Forms.Label lblException;
        private System.Windows.Forms.CheckedListBox chkFileTypes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox grpFolderOptions;
        private System.Windows.Forms.RadioButton rbFolderMonth;
        private System.Windows.Forms.RadioButton rbFolderDay;
    }
}

