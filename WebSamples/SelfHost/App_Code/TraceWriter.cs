using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace SelfHost.App_Code
{
    public class TraceWriter : ITraceWriter
    {
        private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> LoggingMap
            = new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>>
        {
            {TraceLevel.Info,LOGGER.Info },
            {TraceLevel.Debug,LOGGER.Debug },
            {TraceLevel.Error,LOGGER.Error },
            {TraceLevel.Fatal,LOGGER.Fatal },
            {TraceLevel.Warn,LOGGER.Warn }
        });

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get { return LoggingMap.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)//未禁用日志跟踪
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    category = category + Environment.NewLine + "Action Parameters:" + JsonConvert.SerializeObject(traceAction.Target);
                }
                var record = new TraceRecord(request, category, level);
                traceAction?.Invoke(record);
                Log(record);
            }
        }

        private void Log(TraceRecord record)
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
                builder.Append("").Append(record.Message).Append(Environment.NewLine);

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    builder.Append("Method:").Append(record.Request.Method).Append(Environment.NewLine);

                if (record.Request.RequestUri != null)
                    builder.Append("URL:").Append(record.Request.RequestUri).Append(Environment.NewLine);

                if (record.Request.Headers != null && record.Request.Headers.Contains("Token")
                    && record.Request.Headers.GetValues("Token") != null
                    && record.Request.Headers.GetValues("Token").FirstOrDefault() != null)
                {
                    builder.Append("Token").Append(record.Request.Headers.GetValues("Token").FirstOrDefault()).Append(Environment.NewLine);
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
            {
                builder.Append(record.Category);
            }

            if (!string.IsNullOrWhiteSpace(record.Operator))
            {
                builder.Append(record.Operator).Append(" ").Append(record.Operation);
            }

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                //var exceptionType = record.Exception.GetType();
                builder.Append(Environment.NewLine);
                builder.Append("Error:" + record.Exception.GetBaseException().Message).Append(Environment.NewLine);
            }

            Logger[record.Level](Convert.ToString(builder));
        }
    }
}
