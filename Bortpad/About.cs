using Bortpad.Properties;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Bortpad
{
    internal partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Text = string.Format(Resources.About, AssemblyTitle);
            labelProductName.Text = string.Format(Resources.AssemblyProduct, AssemblyProduct);
            labelVersion.Text = string.Format(Resources.AssemblyVersion, AssemblyVersion);
            labelCopyright.Text = string.Format(Resources.AssemblyCopyright, AssemblyCopyright);
            labelCompanyName.Text = string.Format(Resources.AssemblyCompany, AssemblyCompany);
            textBoxDescription.Text = string.Format(Resources.AssemblyDescription, AssemblyDescription);
        }

        #region Assembly Attribute Accessors

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        #endregion Assembly Attribute Accessors

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}