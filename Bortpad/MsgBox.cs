using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WilsonUtils;

// Generic static MessageBox class - but customizable
public partial class MsgBox : Form
{
    private static MsgBox _thisMsgBox;

    private static readonly Button[] _presets =
    {
        new() { Name = "okBtn", Text = "OK", DialogResult = DialogResult.OK },
        new() { Name = "cancelBtn", Text = "Cancel", DialogResult = DialogResult.Cancel },
        new() { Name = "yesBtn", Text = "&Yes", DialogResult = DialogResult.Yes },
        new() { Name = "noBtn", Text = "&No", DialogResult = DialogResult.No },
        new() { Name = "abortBtn", Text = "&Abort", DialogResult = DialogResult.Abort },
        new() { Name = "retryBtn", Text = "&Retry", DialogResult = DialogResult.Retry },
        new() { Name = "ignoreBtn", Text = "&Ignore", DialogResult = DialogResult.Ignore }
    };

    private static readonly IDictionary<MessageBoxButtons, List<Button>> _buttonSet = new Dictionary<MessageBoxButtons, List<Button>>()
    {
        { MessageBoxButtons.OK, new() { _presets[0] } },
        { MessageBoxButtons.OKCancel, new() { _presets[0], _presets[1] } },
        { MessageBoxButtons.YesNo, new() { _presets[2], _presets[3] } },
        { MessageBoxButtons.YesNoCancel, new() { _presets[2], _presets[3], _presets[1] } },
        { MessageBoxButtons.RetryCancel, new() { _presets[5], _presets[1] } },
        { MessageBoxButtons.AbortRetryIgnore, new() { _presets[4], _presets[5], _presets[6] } }
    };

    public object Result
    {
        get; private set;
    }

    private MsgBox(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon icon, string defaultButton)
    {
        InitializeComponent();
        SetMessage(message);
        SetCaption(caption);
        SetSubMessage(subMessage);
        SetMessageIcon(icon);
        SetButtons(buttons, defaultButton);
    }

    private void SetMessage(string message)
    {
        if (message == null)
        {
            messagePanel.Controls.Remove(messageLabel);
            return;
        }
        messageLabel.Text = message;
    }

    private void SetCaption(string caption)
    {
        Text = caption ?? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
    }

    private void SetSubMessage(string subMessage)
    {
        if (subMessage == null)
        {
            contentPanel.Controls.Remove(subMessageLabel);
            messageLabel.Font = null;
            messageLabel.ForeColor = SystemColors.WindowText;
            return;
        }
        subMessageLabel.Text = subMessage;
    }

    private void SetButtons(List<Button> buttons, string defaultButton)
    {
        // Show an OK button if no buttons are specified
        buttons ??= new();
        if (buttons.Count < 1)
        {
            buttons.Add(_presets[0]);
        }

        // Iterate through each button
        buttons.ForEach(button =>
        {
            // Trigger each to close dialog, setting the Result property to the button's Tag object
            button.Click += (sender, e) =>
            {
                Result = ((Button)sender).Tag;
                Close();
            };

            // Set a CancelButton and AcceptButton for the prompt according to the DialogResult
            switch (button.DialogResult)
            {
                case DialogResult.Cancel:
                    CancelButton ??= button;
                    break;

                case DialogResult.OK:
                    AcceptButton ??= button;
                    break;
            }

            button.AutoSize = true;
        });

        // Reverse order since flow panel populates right to left
        buttons.Reverse();

        // Add buttons to flow panel
        buttonPanel.Controls.AddRange(buttons.ToArray());

        // Add a spacer to the left
        buttonPanel.Controls.Add(new Label() { Width = 20 });

        // Set pre-selected (default) button
        if (defaultButton == null)
        {
            return;
        }
        Control[] findDefault = buttonPanel.Controls.Find(defaultButton, false);
        if (findDefault.Length > 0)
        {
            findDefault.First().Select();
        }
    }

    private void SetMessageIcon(MessageBoxIcon icon)
    {
        messageIcon.Image = icon switch
        {
            MessageBoxIcon.Information => SystemIcons.Information.ToBitmap(),
            MessageBoxIcon.Error => SystemIcons.Error.ToBitmap(),
            MessageBoxIcon.Question => SystemIcons.Question.ToBitmap(),
            MessageBoxIcon.Warning => SystemIcons.Warning.ToBitmap(),
            _ => null,
        };
        if (messageIcon.Image == null)
        {
            messagePanel.Controls.Remove(messageIcon);
        }
    }

    // Hide the base method ShowDialog() so user can only use Show()
    private new DialogResult ShowDialog()
    {
        return base.ShowDialog();
    }

    #region Show Method Overloads

    // Buttons should at least have Text and DialogResult properties, Tag and TabIndex optionally, and Name if setting as defaultButton
    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon icon, string defaultButton)
    {
        _thisMsgBox = new(message, subMessage, caption, buttons, icon, defaultButton);
        return _thisMsgBox.ShowDialog();
    }

    public static DialogResult Show(string message)
    {
        return Show(message, null, null, null, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, string caption)
    {
        return Show(message, null, caption, null, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption)
    {
        return Show(message, subMessage, caption, null, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, MessageBoxIcon icon)
    {
        return Show(message, null, null, null, icon, null);
    }

    public static DialogResult Show(string message, List<Button> buttons)
    {
        return Show(message, null, null, buttons, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, null, buttons, MessageBoxIcon.None, defaultButton);
    }

    public static DialogResult Show(string message, List<Button> buttons, MessageBoxIcon icon)
    {
        return Show(message, null, null, buttons, icon, null);
    }

    public static DialogResult Show(string message, List<Button> buttons, MessageBoxIcon icon, string defaultButton)
    {
        return Show(message, null, null, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string caption, MessageBoxIcon icon)
    {
        return Show(message, null, caption, null, icon, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons)
    {
        return Show(message, null, caption, buttons, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, caption, buttons, MessageBoxIcon.None, defaultButton);
    }

    public static DialogResult Show(string message, string caption, MessageBoxIcon icon, List<Button> buttons)
    {
        return Show(message, null, caption, buttons, icon, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons, MessageBoxIcon icon, string defaultButton)
    {
        return Show(message, null, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxIcon icon)
    {
        return Show(message, subMessage, caption, null, icon, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons)
    {
        return Show(message, subMessage, caption, buttons, MessageBoxIcon.None, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, string defaultButton)
    {
        return Show(message, subMessage, caption, buttons, MessageBoxIcon.None, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon icon)
    {
        return Show(message, subMessage, caption, buttons, icon, null);
    }

    #endregion Show Method Overloads

    #region Emulate a few standard MessageBox.Show overloads

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
    {
        List<Button> buttonSet = _buttonSet[buttons];
        string defaultString = defaultButton switch
        {
            MessageBoxDefaultButton.Button1 => buttonSet.Count >= 1 ? buttonSet[0].Name : null,
            MessageBoxDefaultButton.Button2 => buttonSet.Count >= 2 ? buttonSet[1].Name : null,
            MessageBoxDefaultButton.Button3 => buttonSet.Count >= 3 ? buttonSet[2].Name : null,
            _ => null
        };
        return Show(message, subMessage, caption, buttonSet, icon, defaultString);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        return Show(message, subMessage, caption, buttons, icon, MessageBoxDefaultButton.Button1);
    }

    public static DialogResult Show(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        return Show(message, null, caption, buttons, icon, MessageBoxDefaultButton.Button1);
    }

    public static DialogResult Show(string message, string caption, MessageBoxButtons buttons)
    {
        return Show(message, null, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
    }

    #endregion Emulate a few standard MessageBox.Show overloads
}