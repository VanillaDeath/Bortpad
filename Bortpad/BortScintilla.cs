using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bortpad
{
    public class BortScintilla : ScintillaNET.Scintilla
    {
        public BortScintilla() : base()
        {
        }

        public int SelectionLength
        {
            get { return SelectionEnd - SelectionStart; }
        }
    }
}