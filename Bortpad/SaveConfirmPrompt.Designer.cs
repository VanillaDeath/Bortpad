
namespace Bortpad
{
    partial class SaveConfirmPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveConfirmPrompt));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DontSaveButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveQuestion = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(331, 11);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(88, 27);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // DontSaveButton
            // 
            this.DontSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DontSaveButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.DontSaveButton.Location = new System.Drawing.Point(235, 11);
            this.DontSaveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DontSaveButton.Name = "DontSaveButton";
            this.DontSaveButton.Size = new System.Drawing.Size(88, 27);
            this.DontSaveButton.TabIndex = 1;
            this.DontSaveButton.Text = "Do&n\'t Save";
            this.DontSaveButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.SaveButton.Location = new System.Drawing.Point(139, 11);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(88, 27);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "&Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // SaveQuestion
            // 
            this.SaveQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveQuestion.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.SaveQuestion.Location = new System.Drawing.Point(14, 10);
            this.SaveQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SaveQuestion.Name = "SaveQuestion";
            this.SaveQuestion.Size = new System.Drawing.Size(400, 60);
            this.SaveQuestion.TabIndex = 3;
            this.SaveQuestion.Text = "Do you want to save changes to Untitled?";
            this.SaveQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.CancelBtn);
            this.flowLayoutPanel1.Controls.Add(this.DontSaveButton);
            this.flowLayoutPanel1.Controls.Add(this.SaveButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 86);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 8, 5, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(428, 51);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // SaveConfirmPrompt
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(428, 137);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.SaveQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveConfirmPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bortpad";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button DontSaveButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label SaveQuestion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}