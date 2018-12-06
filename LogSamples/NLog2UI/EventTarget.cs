using NLog;
using NLog.Common;
using NLog.Targets;
using NLog2UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log2UI
{
    [Target("Event")]
    public class EventTarget : TargetWithLayout
    {
        public event EventHandler<EventLogEventArgs> LogReceived;

        protected override void Write(LogEventInfo logEvent)
        {
            string message = RenderLogEvent(Layout, logEvent);
            LogReceived?.Invoke(this, new EventLogEventArgs(message));
            //base.Write(logEvent);
        }
    }
}
