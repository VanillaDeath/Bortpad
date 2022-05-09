﻿namespace Bortpad
{
    partial class GoToPrompt
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
            this.lineNumberLabel = new System.Windows.Forms.Label();
            this.lineNumber = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.GoToButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lineNumberLabel
            // 
            this.lineNumberLabel.AutoSize = true;
            this.lineNumberLabel.Location = new System.Drawing.Point(13, 13);
            this.lineNumberLabel.Name = "lineNumberLabel";
            this.lineNumberLabel.Size = new System.Drawing.Size(68, 13);
            this.lineNumberLabel.TabIndex = 0;
            this.lineNumberLabel.Text = "&Line number:";
            // 
            // lineNumber
            // 
            this.lineNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineNumber.Location = new System.Drawing.Point(16, 30);
            this.lineNumber.Name = "lineNumber";
            this.lineNumber.Size = new System.Drawing.Size(223, 20);
            this.lineNumber.TabIndex = 1;
            this.lineNumber.Text = "1";
            this.lineNumber.TextChanged += new System.EventHandler(this.lineNumber_TextChanged);
            this.lineNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.lineNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(164, 67);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // GoToButton
            // 
            this.GoToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GoToButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.GoToButton.Location = new System.Drawing.Point(83, 67);
            this.GoToButton.Name = "GoToButton";
            this.GoToButton.Size = new System.Drawing.Size(75, 23);
            this.GoToButton.TabIndex = 3;
            this.GoToButton.Text = "Go To";
            this.GoToButton.UseVisualStyleBackColor = true;
            this.GoToButton.Click += new System.EventHandler(this.GoToButton_Click);
            // 
            // GoToPrompt
            // 
            this.AcceptButton = this.GoToButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(251, 102);
            this.Controls.Add(this.GoToButton);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.lineNumber);
            this.Controls.Add(this.lineNumberLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go To Line";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lineNumberLabel;
        private System.Windows.Forms.TextBox lineNumber;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button GoToButton;
    }
}