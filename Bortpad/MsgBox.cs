using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WilsonUtils;

// Generic static MessageBox class - but customizable
public partial class MsgBox : Form
{
    private static MsgBox _thisMsgBox;

    private static readonly List<Button> _presetButtons = new()
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
        { MessageBoxButtons.OK,  GetPresets("ok") },
        { MessageBoxButtons.OKCancel, GetPresets("ok", "cancel") },
        { MessageBoxButtons.YesNo, GetPresets("yes", "no") },
        { MessageBoxButtons.YesNoCancel, GetPresets("yes", "no", "cancel") },
        { MessageBoxButtons.RetryCancel, GetPresets("retry", "cancel") },
        { MessageBoxButtons.AbortRetryIgnore, GetPresets("abort", "retry", "ignore") }
    };

    public object Result
    {
        get; private set;
    }

    private MsgBox(string message, string subMessage, string caption, MessageBoxIcon icon, List<Button> buttons, string defaultButton)
    {
        InitializeComponent();
        SetMessage(message);
        SetCaption(caption);
        SetSubMessage(subMessage);
        SetMessageIcon(icon);
        SetButtons(buttons, defaultButton);
    }

    private static List<Button> GetPresets(params string[] name)
    {
        List<Button> result = new();
        foreach (string n in name)
        {
            result.Add(_presetButtons.Find(b => Equals(b.Name, n + "Btn")));
        }
        return result;
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
            Controls.Remove(subMessageLabel);
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
            buttons.Add(
                _presetButtons.FirstOrDefault()
            );
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
        });

        // Reverse order since flow panel populates right to left
        buttons.Reverse();

        // Add buttons to flow panel
        buttonPanel.Controls.AddRange(buttons.ToArray());

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
    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxIcon icon, List<Button> buttons, string defaultButton)
    {
        _thisMsgBox = new(message, subMessage, caption, icon, buttons, defaultButton);
        return _thisMsgBox.ShowDialog();
    }

    public static DialogResult Show(string message)
    {
        return Show(message, null, null, MessageBoxIcon.None, null, null);
    }

    public static DialogResult Show(string message, string caption)
    {
        return Show(message, null, caption, MessageBoxIcon.None, null, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption)
    {
        return Show(message, subMessage, caption, MessageBoxIcon.None, null, null);
    }

    public static DialogResult Show(string message, MessageBoxIcon icon)
    {
        return Show(message, null, null, icon, null, null);
    }

    public static DialogResult Show(string message, List<Button> buttons)
    {
        return Show(message, null, null, MessageBoxIcon.None, buttons, null);
    }

    public static DialogResult Show(string message, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, null, MessageBoxIcon.None, buttons, defaultButton);
    }

    public static DialogResult Show(string message, MessageBoxIcon icon, List<Button> buttons)
    {
        return Show(message, null, null, icon, buttons, null);
    }

    public static DialogResult Show(string message, MessageBoxIcon icon, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, null, icon, buttons, defaultButton);
    }

    public static DialogResult Show(string message, string caption, MessageBoxIcon icon)
    {
        return Show(message, null, caption, icon, null, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons)
    {
        return Show(message, null, caption, MessageBoxIcon.None, buttons, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, caption, MessageBoxIcon.None, buttons, defaultButton);
    }

    public static DialogResult Show(string message, string caption, MessageBoxIcon icon, List<Button> buttons)
    {
        return Show(message, null, caption, icon, buttons, null);
    }

    public static DialogResult Show(string message, string caption, MessageBoxIcon icon, List<Button> buttons, string defaultButton)
    {
        return Show(message, null, caption, icon, buttons, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxIcon icon)
    {
        return Show(message, subMessage, caption, icon, null, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons)
    {
        return Show(message, subMessage, caption, MessageBoxIcon.None, buttons, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, string defaultButton)
    {
        return Show(message, subMessage, caption, MessageBoxIcon.None, buttons, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxIcon icon, List<Button> buttons)
    {
        return Show(message, subMessage, caption, icon, buttons, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxIcon icon, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
    {
        List<Button> buttonSet = _buttonSet[buttons];
        string defaultString = defaultButton switch
        {
            MessageBoxDefaultButton.Button1 => buttonSet.Count >= 1 ? buttonSet[0].Name : null,
            MessageBoxDefaultButton.Button2 => buttonSet.Count >= 2 ? buttonSet[1].Name : null,
            MessageBoxDefaultButton.Button3 => buttonSet.Count >= 3 ? buttonSet[2].Name : null,
            _ => null
        };
        return Show(message, subMessage, caption, icon, _buttonSet[buttons], defaultString);
    }

    #endregion Show Method Overloads
}