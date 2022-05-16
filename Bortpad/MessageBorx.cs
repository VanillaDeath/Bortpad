using System.Collections.Generic;
using System.Windows.Forms;

namespace Bortpad
{
    // TODO: finsih this
    public partial class MessageBorx : Form
    {
        private string _message, _caption, _subMessage;
        private MessageBoxIcon _messageIcon;

        private MessageBorx(string message, string caption = null, string subMessage = null, MessageBoxIcon icon = MessageBoxIcon.None, List<Button> buttons = null)
        {
            InitializeComponent();
            Message = message;
            Caption = caption;
            SubMessage = subMessage;
            MessageIcon = icon;
            Buttons = buttons;
        }

        public string Message
        {
            get
            {
                return _message;
            }
            private set
            {
                _message = MessageLabel.Text = value;
            }
        }

        public string Caption
        {
            get
            {
                if (_caption == null) { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; }
                return _caption;
            }
            private set
            {
                Text = _caption = value;
            }
        }

        public string SubMessage
        {
            get
            {
                return _subMessage;
            }
            private set
            {
                _subMessage = subMessageLabel.Text = value;
            }
        }

        public List<Button> Buttons { get; private set; }

        public MessageBoxIcon MessageIcon
        {
            get
            {
                return _messageIcon;
            }
            private set
            {
                _messageIcon = value;
            }
        }

        public MessageBorx AddButton(string name, string text, DialogResult result = DialogResult.None, System.EventHandler onClick = null, bool isAcceptButton = false, bool isCancelButton = false, int tabIndex = -1)
        {
            Button button = new Button();
            button.Name = name;
            button.Text = text;
            button.DialogResult = result;
            if (onClick != null) { button.Click += onClick; }
            if (tabIndex > -1) { button.TabIndex = tabIndex; }
            Buttons.Add(button);
            if (isAcceptButton) { AcceptButton = Buttons.FindLast(i => ReferenceEquals(i, button)); }
            else if (isCancelButton) { CancelButton = Buttons.FindLast(i => ReferenceEquals(i, button)); }
            return this;
        }

        public new DialogResult Show()
        {
            return ShowDialog();
        }

        private void MessageBorx_Shown(object sender, System.EventArgs e)
        {
        }
    }
}