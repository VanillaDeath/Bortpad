
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
        this.subMessagePanel = new System.Windows.Forms.FlowLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)(this.messageIcon)).BeginInit();
        this.messagePanel.SuspendLayout();
        this.subMessagePanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // MessageLabel
        // 
        this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.messageLabel.AutoSize = true;
        this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
        this.messageLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
        this.messageLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.messageLabel.Location = new System.Drawing.Point(42, 0);
        this.messageLabel.Name = "MessageLabel";
        this.messageLabel.Size = new System.Drawing.Size(0, 40);
        this.messageLabel.TabIndex = 3;
        this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // subMessageLabel
        // 
        this.subMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.subMessageLabel.AutoEllipsis = true;
        this.subMessageLabel.AutoSize = true;
        this.subMessageLabel.Location = new System.Drawing.Point(3, 0);
        this.subMessageLabel.Name = "subMessageLabel";
        this.subMessageLabel.Size = new System.Drawing.Size(0, 13);
        this.subMessageLabel.TabIndex = 5;
        this.subMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // messageIcon
        // 
        this.messageIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.messageIcon.Location = new System.Drawing.Point(3, 3);
        this.messageIcon.Name = "messageIcon";
        this.messageIcon.Size = new System.Drawing.Size(33, 34);
        this.messageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        this.messageIcon.TabIndex = 6;
        this.messageIcon.TabStop = false;
        // 
        // messagePanel
        // 
        this.messagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.messagePanel.AutoSize = true;
        this.messagePanel.Controls.Add(this.messageIcon);
        this.messagePanel.Controls.Add(this.messageLabel);
        this.messagePanel.Location = new System.Drawing.Point(12, 12);
        this.messagePanel.Name = "messagePanel";
        this.messagePanel.Size = new System.Drawing.Size(343, 40);
        this.messagePanel.TabIndex = 7;
        // 
        // buttonPanel
        // 
        this.buttonPanel.AutoSize = true;
        this.buttonPanel.BackColor = System.Drawing.SystemColors.Control;
        this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.buttonPanel.Location = new System.Drawing.Point(0, 108);
        this.buttonPanel.Name = "buttonPanel";
        this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
        this.buttonPanel.Size = new System.Drawing.Size(367, 20);
        this.buttonPanel.TabIndex = 8;
        // 
        // subMessagePanel
        // 
        this.subMessagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.subMessagePanel.AutoSize = true;
        this.subMessagePanel.Controls.Add(this.subMessageLabel);
        this.subMessagePanel.Location = new System.Drawing.Point(12, 58);
        this.subMessagePanel.Name = "subMessagePanel";
        this.subMessagePanel.Size = new System.Drawing.Size(343, 13);
        this.subMessagePanel.TabIndex = 9;
        // 
        // MessageBorx
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.BackColor = System.Drawing.SystemColors.Window;
        this.ClientSize = new System.Drawing.Size(367, 128);
        this.Controls.Add(this.subMessagePanel);
        this.Controls.Add(this.buttonPanel);
        this.Controls.Add(this.messagePanel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.KeyPreview = true;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MessageBorx";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        ((System.ComponentModel.ISupportInitialize)(this.messageIcon)).EndInit();
        this.messagePanel.ResumeLayout(false);
        this.messagePanel.PerformLayout();
        this.subMessagePanel.ResumeLayout(false);
        this.subMessagePanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label messageLabel;
    private System.Windows.Forms.Label subMessageLabel;
    private System.Windows.Forms.PictureBox messageIcon;
    private System.Windows.Forms.FlowLayoutPanel messagePanel;
    private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    private System.Windows.Forms.FlowLayoutPanel subMessagePanel;
}