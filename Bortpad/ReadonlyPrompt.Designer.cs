
namespace Bortpad
{
    partial class ReadonlyPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadonlyPrompt));
            this.cancelBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.ROQuestion = new System.Windows.Forms.Label();
            this.notice = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(179, 11);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(88, 27);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.editBtn.Location = new System.Drawing.Point(83, 11);
            this.editBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(88, 27);
            this.editBtn.TabIndex = 2;
            this.editBtn.Text = "&Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // ROQuestion
            // 
            this.ROQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ROQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ROQuestion.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ROQuestion.Location = new System.Drawing.Point(14, 10);
            this.ROQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ROQuestion.Name = "ROQuestion";
            this.ROQuestion.Size = new System.Drawing.Size(248, 26);
            this.ROQuestion.TabIndex = 1;
            this.ROQuestion.Text = "This file is marked as Read-Only";
            this.ROQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notice
            // 
            this.notice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notice.Location = new System.Drawing.Point(14, 36);
            this.notice.Name = "notice";
            this.notice.Size = new System.Drawing.Size(248, 79);
            this.notice.TabIndex = 5;
            this.notice.Text = "If you choose to edit this file, when you Save you will be prompted to either:\r\n•" +
    " Unset the Read-Only flag\r\n• Save As a new document";
            this.notice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.buttonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPanel.Controls.Add(this.cancelBtn);
            this.buttonPanel.Controls.Add(this.editBtn);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(0, 131);
            this.buttonPanel.Name = "flowLayoutPanel1";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0, 8, 5, 0);
            this.buttonPanel.Size = new System.Drawing.Size(276, 50);
            this.buttonPanel.TabIndex = 0;
            // 
            // ReadonlyPrompt
            // 
            this.AcceptButton = this.editBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(276, 181);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.notice);
            this.Controls.Add(this.ROQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadonlyPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bortpad";
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Label ROQuestion;
        private System.Windows.Forms.Label notice;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    }
}