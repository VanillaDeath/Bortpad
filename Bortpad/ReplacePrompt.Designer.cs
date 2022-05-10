namespace Bortpad
{
    partial class ReplacePrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplacePrompt));
            this.findWhat = new System.Windows.Forms.Label();
            this.searchQuery = new System.Windows.Forms.TextBox();
            this.findNextButton = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.wrapAround = new System.Windows.Forms.CheckBox();
            this.matchCase = new System.Windows.Forms.CheckBox();
            this.replaceWithLabel = new System.Windows.Forms.Label();
            this.replaceWith = new System.Windows.Forms.TextBox();
            this.replaceButton = new System.Windows.Forms.Button();
            this.replaceAllButton = new System.Windows.Forms.Button();
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
            this.searchQuery.Location = new System.Drawing.Point(88, 11);
            this.searchQuery.Name = "searchQuery";
            this.searchQuery.Size = new System.Drawing.Size(177, 20);
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
            this.cancelBtn.Location = new System.Drawing.Point(282, 95);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // wrapAround
            // 
            this.wrapAround.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wrapAround.AutoSize = true;
            this.wrapAround.Location = new System.Drawing.Point(13, 129);
            this.wrapAround.Name = "wrapAround";
            this.wrapAround.Size = new System.Drawing.Size(88, 17);
            this.wrapAround.TabIndex = 4;
            this.wrapAround.Text = "W&rap around";
            this.wrapAround.UseVisualStyleBackColor = true;
            // 
            // matchCase
            // 
            this.matchCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.matchCase.AutoSize = true;
            this.matchCase.Location = new System.Drawing.Point(13, 106);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(82, 17);
            this.matchCase.TabIndex = 3;
            this.matchCase.Text = "Match &case";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // replaceWithLabel
            // 
            this.replaceWithLabel.AutoSize = true;
            this.replaceWithLabel.Location = new System.Drawing.Point(10, 39);
            this.replaceWithLabel.Name = "replaceWithLabel";
            this.replaceWithLabel.Size = new System.Drawing.Size(72, 13);
            this.replaceWithLabel.TabIndex = 7;
            this.replaceWithLabel.Text = "Re&place with:";
            // 
            // replaceWith
            // 
            this.replaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replaceWith.Location = new System.Drawing.Point(88, 37);
            this.replaceWith.Name = "replaceWith";
            this.replaceWith.Size = new System.Drawing.Size(177, 20);
            this.replaceWith.TabIndex = 2;
            // 
            // replaceButton
            // 
            this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceButton.Enabled = false;
            this.replaceButton.Location = new System.Drawing.Point(282, 37);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(75, 23);
            this.replaceButton.TabIndex = 6;
            this.replaceButton.Text = "&Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceAllButton.Enabled = false;
            this.replaceAllButton.Location = new System.Drawing.Point(282, 66);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(75, 23);
            this.replaceAllButton.TabIndex = 7;
            this.replaceAllButton.Text = "Replace &All";
            this.replaceAllButton.UseVisualStyleBackColor = true;
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // ReplacePrompt
            // 
            this.AcceptButton = this.findNextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(369, 158);
            this.Controls.Add(this.replaceAllButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.replaceWith);
            this.Controls.Add(this.replaceWithLabel);
            this.Controls.Add(this.matchCase);
            this.Controls.Add(this.wrapAround);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.searchQuery);
            this.Controls.Add(this.findWhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplacePrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FindPrompt_Shown);
            this.VisibleChanged += new System.EventHandler(this.FindPrompt_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label findWhat;
        private System.Windows.Forms.TextBox searchQuery;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox wrapAround;
        private System.Windows.Forms.CheckBox matchCase;
        private System.Windows.Forms.Label replaceWithLabel;
        private System.Windows.Forms.TextBox replaceWith;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button replaceAllButton;
    }
}