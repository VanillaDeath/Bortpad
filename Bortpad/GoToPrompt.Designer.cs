namespace Bortpad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoToPrompt));
            this.lineNumberLabel = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.GoToButton = new System.Windows.Forms.Button();
            this.lineNumber = new WilsonUtils.NumBox();
            this.SuspendLayout();
            // 
            // lineNumberLabel
            // 
            resources.ApplyResources(this.lineNumberLabel, "lineNumberLabel");
            this.lineNumberLabel.Name = "lineNumberLabel";
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // GoToButton
            // 
            resources.ApplyResources(this.GoToButton, "GoToButton");
            this.GoToButton.Name = "GoToButton";
            this.GoToButton.UseVisualStyleBackColor = true;
            this.GoToButton.Click += new System.EventHandler(this.GoToButton_Click);
            // 
            // lineNumber
            // 
            resources.ApplyResources(this.lineNumber, "lineNumber");
            this.lineNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineNumber.Name = "lineNumber";
            // 
            // GoToPrompt
            // 
            this.AcceptButton = this.GoToButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
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
            this.Shown += new System.EventHandler(this.GoToPrompt_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lineNumberLabel;
        private WilsonUtils.NumBox lineNumber;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button GoToButton;
    }
}