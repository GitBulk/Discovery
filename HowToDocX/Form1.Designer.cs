namespace HowToDocX
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
            this.btnSample1 = new System.Windows.Forms.Button();
            this.btnFormatText = new System.Windows.Forms.Button();
            this.btnReplaceText = new System.Windows.Forms.Button();
            this.btnCreateTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSample1
            // 
            this.btnSample1.Location = new System.Drawing.Point(19, 19);
            this.btnSample1.Name = "btnSample1";
            this.btnSample1.Size = new System.Drawing.Size(75, 23);
            this.btnSample1.TabIndex = 0;
            this.btnSample1.Text = "Sample 1";
            this.btnSample1.UseVisualStyleBackColor = true;
            this.btnSample1.Click += new System.EventHandler(this.btnSample1_Click);
            // 
            // btnFormatText
            // 
            this.btnFormatText.Location = new System.Drawing.Point(19, 59);
            this.btnFormatText.Name = "btnFormatText";
            this.btnFormatText.Size = new System.Drawing.Size(75, 23);
            this.btnFormatText.TabIndex = 1;
            this.btnFormatText.Text = "Format text";
            this.btnFormatText.UseVisualStyleBackColor = true;
            this.btnFormatText.Click += new System.EventHandler(this.btnFormatText_Click);
            // 
            // btnReplaceText
            // 
            this.btnReplaceText.Location = new System.Drawing.Point(149, 168);
            this.btnReplaceText.Name = "btnReplaceText";
            this.btnReplaceText.Size = new System.Drawing.Size(75, 23);
            this.btnReplaceText.TabIndex = 2;
            this.btnReplaceText.Text = "Replace Text";
            this.btnReplaceText.UseVisualStyleBackColor = true;
            this.btnReplaceText.Click += new System.EventHandler(this.btnReplaceText_Click);
            // 
            // btnCreateTemplate
            // 
            this.btnCreateTemplate.Location = new System.Drawing.Point(19, 168);
            this.btnCreateTemplate.Name = "btnCreateTemplate";
            this.btnCreateTemplate.Size = new System.Drawing.Size(98, 23);
            this.btnCreateTemplate.TabIndex = 3;
            this.btnCreateTemplate.Text = "Create Template";
            this.btnCreateTemplate.UseVisualStyleBackColor = true;
            this.btnCreateTemplate.Click += new System.EventHandler(this.btnCreateTemplate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCreateTemplate);
            this.Controls.Add(this.btnReplaceText);
            this.Controls.Add(this.btnFormatText);
            this.Controls.Add(this.btnSample1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSample1;
        private System.Windows.Forms.Button btnFormatText;
        private System.Windows.Forms.Button btnReplaceText;
        private System.Windows.Forms.Button btnCreateTemplate;
    }
}

