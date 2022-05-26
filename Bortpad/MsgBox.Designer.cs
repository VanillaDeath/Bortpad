
namespace WilsonUtils;

partial class MsgBox
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.subMessageLabel = new System.Windows.Forms.Label();
            this.messageIcon = new System.Windows.Forms.PictureBox();
            this.messagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contentPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.messageIcon)).BeginInit();
            this.messagePanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.messageLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.messageLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.messageLabel.Location = new System.Drawing.Point(42, 0);
            this.messageLabel.MinimumSize = new System.Drawing.Size(0, 36);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(0, 36);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.messageLabel.UseMnemonic = false;
            // 
            // subMessageLabel
            // 
            this.subMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subMessageLabel.AutoEllipsis = true;
            this.subMessageLabel.AutoSize = true;
            this.subMessageLabel.Location = new System.Drawing.Point(23, 49);
            this.subMessageLabel.Margin = new System.Windows.Forms.Padding(23, 3, 23, 3);
            this.subMessageLabel.Name = "subMessageLabel";
            this.subMessageLabel.Size = new System.Drawing.Size(114, 13);
            this.subMessageLabel.TabIndex = 5;
            this.subMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.subMessageLabel.UseMnemonic = false;
            // 
            // messageIcon
            // 
            this.messageIcon.Location = new System.Drawing.Point(3, 3);
            this.messageIcon.Name = "messageIcon";
            this.messageIcon.Size = new System.Drawing.Size(33, 34);
            this.messageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.messageIcon.TabIndex = 6;
            this.messageIcon.TabStop = false;
            // 
            // messagePanel
            // 
            this.messagePanel.AutoSize = true;
            this.messagePanel.Controls.Add(this.messageIcon);
            this.messagePanel.Controls.Add(this.messageLabel);
            this.messagePanel.Location = new System.Drawing.Point(20, 3);
            this.messagePanel.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(45, 40);
            this.messagePanel.TabIndex = 7;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.AutoSize = true;
            this.buttonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(0, 70);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.buttonPanel.MinimumSize = new System.Drawing.Size(160, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(8);
            this.buttonPanel.Size = new System.Drawing.Size(160, 16);
            this.buttonPanel.TabIndex = 8;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoSize = true;
            this.contentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentPanel.Controls.Add(this.messagePanel);
            this.contentPanel.Controls.Add(this.subMessageLabel);
            this.contentPanel.Controls.Add(this.buttonPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(144, 81);
            this.contentPanel.TabIndex = 9;
            this.contentPanel.WrapContents = false;
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(144, 81);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(160, 120);
            this.Name = "MsgBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.messageIcon)).EndInit();
            this.messagePanel.ResumeLayout(false);
            this.messagePanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label messageLabel;
    private System.Windows.Forms.Label subMessageLabel;
    private System.Windows.Forms.PictureBox messageIcon;
    private System.Windows.Forms.FlowLayoutPanel messagePanel;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    private System.Windows.Forms.FlowLayoutPanel contentPanel;
}