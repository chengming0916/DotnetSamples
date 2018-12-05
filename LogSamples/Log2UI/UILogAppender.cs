using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log2UI
{
    public class UILogAppender : AppenderSkeleton
    {
        public event EventHandler<UILogEventArgs> UILogReceived;

        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = RenderLoggingEvent(loggingEvent);
            UILogReceived?.Invoke(this, new UILogEventArgs(message));
        }
    }
}
