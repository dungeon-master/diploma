namespace ECatalogRecommendations
{
    partial class ExternalCatalogSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbDatabase = new System.Windows.Forms.RadioButton();
            this.rbXml = new System.Windows.Forms.RadioButton();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPath);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.lblPath);
            this.groupBox1.Controls.Add(this.rbXml);
            this.groupBox1.Controls.Add(this.rbDatabase);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Источник внешнего электронного каталога";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(12, 141);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(343, 141);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbDatabase
            // 
            this.rbDatabase.AutoSize = true;
            this.rbDatabase.Location = new System.Drawing.Point(6, 19);
            this.rbDatabase.Name = "rbDatabase";
            this.rbDatabase.Size = new System.Drawing.Size(215, 17);
            this.rbDatabase.TabIndex = 0;
            this.rbDatabase.TabStop = true;
            this.rbDatabase.Text = "Использовать таблицу в текущей БД";
            this.rbDatabase.UseVisualStyleBackColor = true;
            this.rbDatabase.CheckedChanged += new System.EventHandler(this.rbDatabase_CheckedChanged);
            // 
            // rbXml
            // 
            this.rbXml.AutoSize = true;
            this.rbXml.Location = new System.Drawing.Point(6, 42);
            this.rbXml.Name = "rbXml";
            this.rbXml.Size = new System.Drawing.Size(145, 17);
            this.rbXml.TabIndex = 1;
            this.rbXml.TabStop = true;
            this.rbXml.Text = "Использовать xml-файл";
            this.rbXml.UseVisualStyleBackColor = true;
            this.rbXml.CheckedChanged += new System.EventHandler(this.rbXml_CheckedChanged);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(4, 68);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(74, 13);
            this.lblPath.TabIndex = 2;
            this.lblPath.Text = "Путь к файлу";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(84, 65);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(316, 20);
            this.txtPath.TabIndex = 3;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(7, 91);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(88, 23);
            this.btnPath.TabIndex = 4;
            this.btnPath.Text = "Выбрать...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // ExternalCatalogSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 169);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExternalCatalogSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Внешний электронный каталог";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbDatabase;
        private System.Windows.Forms.RadioButton rbXml;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblPath;
    }
}