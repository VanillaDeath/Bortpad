using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WilsonUtils;

// Generic static MessageBox class - but customizable
public partial class MsgBox : Form
{
    #region Static _fields

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

    #endregion Static _fields

    #region Result Objects

    public Button ButtonClicked
    {
        get; private set;
    }

    public object Result
    {
        get; private set;
    }

    #endregion Result Objects

    #region Constructors

    private MsgBox(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        InitializeComponent();
        SetMessage(message);
        SetCaption(caption);
        SetSubMessage(subMessage);
        SetMessageIcon(icon);
        SetButtons(buttons, defaultButton);
    }

    #endregion Constructors

    #region Setters

    private void SetMessage(string message)
    {
        if (message is null)
        {
            messagePanel.Controls.Remove(messageLabel);
            return;
        }
        messageLabel.Text = message;
    }

    private void SetCaption(string caption)
    {
        Text = caption;
    }

    private void SetSubMessage(string subMessage)
    {
        if (subMessage is null)
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
        // Reduce List to usable Button objects
        buttons ??= new();
        buttons = buttons
            .Where(button => button?.Text is not null && button.Text.Trim().Length > 0)
            .ToList();

        // Show an OK Button if no usable Buttons
        if (buttons.Count < 1)
        {
            buttons.Add(_presets[0]);
        }

        // See if all TabIndex properties are equal
        int i = buttons.FirstOrDefault().TabIndex;
        bool allEqual = buttons.All(button => button.TabIndex == i);

        // Iterate through each button that has Text
        buttons.ForEach(button =>
        {
            // Trigger each to close dialog, setting the ButtonClicked property to the Button object,
            // and the Result property to the button's Tag object
            button.Click += (sender, e) =>
            {
                ButtonClicked = (Button)sender;
                Result = ButtonClicked.Tag;
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

            // Maintain button Tab order as defined if TabIndexes were not set (all 0) or were all set the same
            if (allEqual)
            {
                button.TabIndex = i++;
            }

            // Stretch buttons to fit Text
            button.AutoSize = true;
        });

        // Reverse order since flow panel populates right to left
        buttons.Reverse();

        // Add buttons to flow panel
        buttonPanel.Controls.AddRange(buttons.ToArray());

        // Add a spacer to the left
        buttonPanel.Controls.Add(new Label() { Width = 20 });

        // Set pre-selected (default) button
        if (defaultButton is null)
        {
            return;
        }
        buttonPanel.Controls.Find(defaultButton, false)?.FirstOrDefault()?.Select();
    }

    private void SetMessageIcon(MessageBoxIcon? icon)
    {
        if (icon is null)
        {
            messagePanel.Controls.Remove(messageIcon);
            return;
        }
        messageIcon.Image = icon switch
        {
            MessageBoxIcon.Information => SystemIcons.Information.ToBitmap(),
            MessageBoxIcon.Error => SystemIcons.Error.ToBitmap(),
            MessageBoxIcon.Question => SystemIcons.Question.ToBitmap(),
            MessageBoxIcon.Warning => SystemIcons.Warning.ToBitmap(),
            _ => null,
        };
    }

    #endregion Setters

    #region Hide the base ShowDialog() method overloads so we can only call using static MsgBox.Show() method

    private new DialogResult ShowDialog(IWin32Window owner)
    {
        return base.ShowDialog(owner);
    }

    private new DialogResult ShowDialog()
    {
        return base.ShowDialog();
    }

    #endregion Hide the base ShowDialog() method overloads so we can only call using static MsgBox.Show() method

    #region Root methods that all Show() overloads call

    private static DialogResult ShowCore(IWin32Window owner, string text, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        // Instantiate an ephemeral MsgBox object using private constructor
        _thisMsgBox = new(text, subMessage, caption, buttons, icon, defaultButton);
        // Return DialogResult back to the static MsgBox.Show() call
        return owner is null ? _thisMsgBox.ShowDialog() : _thisMsgBox.ShowDialog(owner);
    }

    private static DialogResult ShowCore(IWin32Window owner, string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon, MessageBoxDefaultButton? defaultButton)
    {
        // Convert MessageBoxButtons to matching List<Button>
        List<Button> buttonSet = _buttonSet[buttons];
        // Convert MessageBoxDefaultButton to expected string
        int index = ((int?)defaultButton ?? 0) / 256;
        string defaultString = buttonSet.ElementAtOrDefault(index)?.Name;

        // Pass to ShowCore Prime
        return ShowCore(owner, message, subMessage, caption, buttonSet, icon, defaultString);
    }

    #endregion Root methods that all Show() overloads call

    #region Show with SubMessage support

    // Buttons should at least have Text and DialogResult properties, Tag and TabIndex optionally, and Name if setting as defaultButton
    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        return ShowCore(null, message, subMessage, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon)
    {
        return ShowCore(null, message, subMessage, caption, buttons, icon, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, List<Button> buttons)
    {
        return ShowCore(null, message, subMessage, caption, buttons, null, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption)
    {
        return ShowCore(null, message, subMessage, caption, null, null, null);
    }

    #endregion Show with SubMessage support

    #region Show overloads

    public static DialogResult Show(string message, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        return ShowCore(null, message, null, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons, MessageBoxIcon? icon)
    {
        return ShowCore(null, message, null, caption, buttons, icon, null);
    }

    public static DialogResult Show(string message, string caption, List<Button> buttons)
    {
        return ShowCore(null, message, null, caption, buttons, null, null);
    }

    public static DialogResult Show(string message, string caption)
    {
        return ShowCore(null, message, null, caption, null, null, null);
    }

    public static DialogResult Show(string message)
    {
        return ShowCore(null, message, null, null, null, null, null);
    }

    #endregion Show overloads

    #region Show with SubMessage and Owner support

    // Buttons should at least have Text and DialogResult properties, Tag and TabIndex optionally, and Name if setting as defaultButton
    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, List<Button> buttons, MessageBoxIcon? icon)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, icon, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, List<Button> buttons)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, null, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption)
    {
        return ShowCore(owner, message, subMessage, caption, null, null, null);
    }

    #endregion Show with SubMessage and Owner support

    #region Show with Owner support

    public static DialogResult Show(IWin32Window owner, string message, string caption, List<Button> buttons, MessageBoxIcon? icon, string defaultButton)
    {
        return ShowCore(owner, message, null, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption, List<Button> buttons, MessageBoxIcon? icon)
    {
        return ShowCore(owner, message, null, caption, buttons, icon, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption, List<Button> buttons)
    {
        return ShowCore(owner, message, null, caption, buttons, null, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption)
    {
        return ShowCore(owner, message, null, caption, null, null, null);
    }

    public static DialogResult Show(IWin32Window owner, string message)
    {
        return ShowCore(owner, message, null, null, null, null, null);
    }

    #endregion Show with Owner support

    #region Emulate standard MessageBox.Show overloads (just no messageboxoptions or helpinfo params)

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon, MessageBoxDefaultButton? defaultButton)
    {
        return ShowCore(null, message, subMessage, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon)
    {
        return ShowCore(null, message, subMessage, caption, buttons, icon, null);
    }

    public static DialogResult Show(string message, string subMessage, string caption, MessageBoxButtons buttons)
    {
        return ShowCore(null, message, subMessage, caption, buttons, null, null);
    }

    public static DialogResult Show(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon, MessageBoxDefaultButton? defaultButton)
    {
        return ShowCore(null, message, null, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon)
    {
        return ShowCore(null, message, null, caption, buttons, icon, null);
    }

    public static DialogResult Show(string message, string caption, MessageBoxButtons buttons)
    {
        return ShowCore(null, message, null, caption, buttons, null, null);
    }

    #endregion Emulate standard MessageBox.Show overloads (just no messageboxoptions or helpinfo params)

    #region Standard MessageBox.Show emulation with owner

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon, MessageBoxDefaultButton? defaultButton)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, icon, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string subMessage, string caption, MessageBoxButtons buttons)
    {
        return ShowCore(owner, message, subMessage, caption, buttons, null, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon, MessageBoxDefaultButton? defaultButton)
    {
        return ShowCore(owner, message, null, caption, buttons, icon, defaultButton);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon? icon)
    {
        return ShowCore(owner, message, null, caption, buttons, icon, null);
    }

    public static DialogResult Show(IWin32Window owner, string message, string caption, MessageBoxButtons buttons)
    {
        return ShowCore(owner, message, null, caption, buttons, null, null);
    }

    #endregion Standard MessageBox.Show emulation with owner
}