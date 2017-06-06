namespace ECatalogRecommendations
{
    partial class GeneralSettings
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
            this.btnClearDeleted = new System.Windows.Forms.Button();
            this.lblDeleted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkUseMaxForAnalyze = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.maxForAnalyze = new System.Windows.Forms.NumericUpDown();
            this.lblUseMax = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxForAnalyze)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnClearDeleted);
            this.groupBox1.Controls.Add(this.lblDeleted);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Удаленные рекомендации";
            // 
            // btnClearDeleted
            // 
            this.btnClearDeleted.Enabled = false;
            this.btnClearDeleted.Location = new System.Drawing.Point(6, 32);
            this.btnClearDeleted.Name = "btnClearDeleted";
            this.btnClearDeleted.Size = new System.Drawing.Size(103, 23);
            this.btnClearDeleted.TabIndex = 2;
            this.btnClearDeleted.Text = "Восстановить";
            this.btnClearDeleted.UseVisualStyleBackColor = true;
            this.btnClearDeleted.Click += new System.EventHandler(this.btnClearDeleted_Click);
            // 
            // lblDeleted
            // 
            this.lblDeleted.AutoSize = true;
            this.lblDeleted.Location = new System.Drawing.Point(99, 16);
            this.lblDeleted.Name = "lblDeleted";
            this.lblDeleted.Size = new System.Drawing.Size(13, 13);
            this.lblDeleted.TabIndex = 1;
            this.lblDeleted.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Всего удалено: ";
            // 
            // checkUseMaxForAnalyze
            // 
            this.checkUseMaxForAnalyze.AutoSize = true;
            this.checkUseMaxForAnalyze.Location = new System.Drawing.Point(6, 19);
            this.checkUseMaxForAnalyze.Name = "checkUseMaxForAnalyze";
            this.checkUseMaxForAnalyze.Size = new System.Drawing.Size(257, 17);
            this.checkUseMaxForAnalyze.TabIndex = 1;
            this.checkUseMaxForAnalyze.Text = "Ограничить количество записей для анализа";
            this.checkUseMaxForAnalyze.UseVisualStyleBackColor = true;
            this.checkUseMaxForAnalyze.CheckedChanged += new System.EventHandler(this.checkUseMaxForAnalyze_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.maxForAnalyze);
            this.groupBox2.Controls.Add(this.lblUseMax);
            this.groupBox2.Controls.Add(this.checkUseMaxForAnalyze);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 66);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Анализ записей в журнале";
            // 
            // maxForAnalyze
            // 
            this.maxForAnalyze.Enabled = false;
            this.maxForAnalyze.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxForAnalyze.Location = new System.Drawing.Point(79, 37);
            this.maxForAnalyze.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
            this.maxForAnalyze.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxForAnalyze.Name = "maxForAnalyze";
            this.maxForAnalyze.Size = new System.Drawing.Size(72, 20);
            this.maxForAnalyze.TabIndex = 4;
            this.maxForAnalyze.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblUseMax
            // 
            this.lblUseMax.AutoSize = true;
            this.lblUseMax.Enabled = false;
            this.lblUseMax.Location = new System.Drawing.Point(6, 39);
            this.lblUseMax.Name = "lblUseMax";
            this.lblUseMax.Size = new System.Drawing.Size(67, 13);
            this.lblUseMax.TabIndex = 3;
            this.lblUseMax.Text = "Максимум: ";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(12, 154);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Отменить";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(205, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GeneralSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 181);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GeneralSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Общие настройки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxForAnalyze)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDeleted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearDeleted;
        private System.Windows.Forms.CheckBox checkUseMaxForAnalyze;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown maxForAnalyze;
        private System.Windows.Forms.Label lblUseMax;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}