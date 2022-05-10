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
            this.findWhat.AutoSize = true;
            this.findWhat.Location = new System.Drawing.Point(10, 13);
            this.findWhat.Name = "findWhat";
            this.findWhat.Size = new System.Drawing.Size(56, 13);
            this.findWhat.TabIndex = 0;
            this.findWhat.Text = "Fi&nd what:";
            // 
            // searchQuery
            // 
            this.searchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchQuery.Location = new System.Drawing.Point(75, 11);
            this.searchQuery.Name = "searchQuery";
            this.searchQuery.Size = new System.Drawing.Size(190, 20);
            this.searchQuery.TabIndex = 1;
            this.searchQuery.TextChanged += new System.EventHandler(this.searchQuery_TextChanged);
            // 
            // findNextButton
            // 
            this.findNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findNextButton.Enabled = false;
            this.findNextButton.Location = new System.Drawing.Point(282, 8);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(75, 23);
            this.findNextButton.TabIndex = 5;
            this.findNextButton.Text = "&Find Next";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.findNext_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(282, 37);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // direction
            // 
            this.direction.Controls.Add(this.down);
            this.direction.Controls.Add(this.up);
            this.direction.Location = new System.Drawing.Point(150, 37);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(115, 43);
            this.direction.TabIndex = 4;
            this.direction.TabStop = false;
            this.direction.Text = "Direction";
            // 
            // down
            // 
            this.down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.down.AutoSize = true;
            this.down.Checked = true;
            this.down.Location = new System.Drawing.Point(52, 20);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(53, 17);
            this.down.TabIndex = 1;
            this.down.TabStop = true;
            this.down.Text = "&Down";
            this.down.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            this.up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.up.AutoSize = true;
            this.up.Location = new System.Drawing.Point(6, 20);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(39, 17);
            this.up.TabIndex = 0;
            this.up.Text = "&Up";
            this.up.UseVisualStyleBackColor = true;
            // 
            // wrapAround
            // 
            this.wrapAround.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wrapAround.AutoSize = true;
            this.wrapAround.Location = new System.Drawing.Point(13, 95);
            this.wrapAround.Name = "wrapAround";
            this.wrapAround.Size = new System.Drawing.Size(88, 17);
            this.wrapAround.TabIndex = 3;
            this.wrapAround.Text = "W&rap around";
            this.wrapAround.UseVisualStyleBackColor = true;
            // 
            // matchCase
            // 
            this.matchCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.matchCase.AutoSize = true;
            this.matchCase.Location = new System.Drawing.Point(13, 72);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(82, 17);
            this.matchCase.TabIndex = 2;
            this.matchCase.Text = "Match &case";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // FindPrompt
            // 
            this.AcceptButton = this.findNextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(369, 124);
            this.Controls.Add(this.matchCase);
            this.Controls.Add(this.wrapAround);
            this.Controls.Add(this.direction);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.searchQuery);
            this.Controls.Add(this.findWhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FindPrompt_Shown);
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