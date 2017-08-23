using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogglyWebRequest.model
{
    public class clsLogglyModel
    {
        private char DoubleQuote = '"'; //34
        private char SingleQuote = (char)39; 
        public DateTime timestamp { get; set; }
        public LEVEL level { get; set; }
        public string hostName { get; set; }
        public string process { get; set; }
        public string threadName { get; set; }
        public string loggerName { get; set; }
        public string message { get; set; }
        public string toLogglyJsonString()
        {
            string content = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}"
                , Json_timestamp
                , Json_level
                , Json_hostName
                , Json_process
                , Json_threadName
                , Json_loggerName
                , Json_message);

            return string.Format("{0}{1}{2}",
                "{", content, "}");
        }
        private string Json_timestamp =>
                 string.Format("{0}timestamp{0}: {0}{1}{2}{0}"
                , DoubleQuote
                , timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fff")
                , "+0800");
        private string Json_level =>  string.Format("{0}level{0}: {0}{1}{0}", DoubleQuote, level);
        private string Json_hostName => string.Format("{0}hostName{0}: {0}{1}{0}", DoubleQuote, hostName);
        private string Json_process => string.Format("{0}process{0}: {0}{1}{0}", DoubleQuote, process);
        private string Json_threadName => string.Format("{0}threadName{0}: {0}{1}{0}", DoubleQuote, threadName);
        private string Json_loggerName => string.Format("{0}loggerName{0}: {0}{1}{0}", DoubleQuote, loggerName);
        private string Json_message => string.Format("{0}message{0}: {0}{1}{0}", DoubleQuote
            , message.Replace(DoubleQuote.ToString(), "").Replace(SingleQuote.ToString(), ""));
    }
}
