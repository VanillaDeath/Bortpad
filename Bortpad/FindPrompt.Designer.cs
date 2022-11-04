namespace Bortpad
{
    partial class FindPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindPrompt));
            this.findWhat = new System.Windows.Forms.Label();
            this.searchQuery = new System.Windows.Forms.TextBox();
            this.findNextButton = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.direction = new System.Windows.Forms.GroupBox();
            this.down = new System.Windows.Forms.RadioButton();
            this.up = new System.Windows.Forms.RadioButton();
            this.wrapAround = new System.Windows.Forms.CheckBox();
            this.matchCase = new System.Windows.Forms.CheckBox();
            this.direction.SuspendLayout();
            this.SuspendLayout();
            // 
            // findWhat
            // 
            resources.ApplyResources(this.findWhat, "findWhat");
            this.findWhat.Name = "findWhat";
            // 
            // searchQuery
            // 
            resources.ApplyResources(this.searchQuery, "searchQuery");
            this.searchQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchQuery.Name = "searchQuery";
            this.searchQuery.TextChanged += new System.EventHandler(this.SearchQuery_TextChanged);
            // 
            // findNextButton
            // 
            resources.ApplyResources(this.findNextButton, "findNextButton");
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.FindNext_Click);
            // 
            // cancelBtn
            // 
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // direction
            // 
            this.direction.Controls.Add(this.down);
            this.direction.Controls.Add(this.up);
            resources.ApplyResources(this.direction, "direction");
            this.direction.Name = "direction";
            this.direction.TabStop = false;
            // 
            // down
            // 
            resources.ApplyResources(this.down, "down");
            this.down.Checked = true;
            this.down.Name = "down";
            this.down.TabStop = true;
            this.down.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            resources.ApplyResources(this.up, "up");
            this.up.Name = "up";
            this.up.UseVisualStyleBackColor = true;
            // 
            // wrapAround
            // 
            resources.ApplyResources(this.wrapAround, "wrapAround");
            this.wrapAround.Name = "wrapAround";
            this.wrapAround.UseVisualStyleBackColor = true;
            // 
            // matchCase
            // 
            resources.ApplyResources(this.matchCase, "matchCase");
            this.matchCase.Name = "matchCase";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // FindPrompt
            // 
            this.AcceptButton = this.findNextButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.Controls.Add(this.direction);
            this.Controls.Add(this.matchCase);
            this.Controls.Add(this.wrapAround);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.searchQuery);
            this.Controls.Add(this.findWhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.FindPrompt_VisibleChanged);
            this.direction.ResumeLayout(false);
            this.direction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label findWhat;
        private System.Windows.Forms.TextBox searchQuery;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox direction;
        private System.Windows.Forms.RadioButton down;
        private System.Windows.Forms.RadioButton up;
        private System.Windows.Forms.CheckBox wrapAround;
        private System.Windows.Forms.CheckBox matchCase;
    }
}