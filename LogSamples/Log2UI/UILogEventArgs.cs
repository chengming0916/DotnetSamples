using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log2UI
{
    public class UILogEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public UILogEventArgs(string message)
        {
            Message = message;
        }
    }
}
