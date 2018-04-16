using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Util
{
    public class PluginLogger : ILogger
    {
        private string ProductName = "";
        
        public PluginLogger(string productName)
        {
            ProductName = productName;
        }

        public void LogTrivial(string pMessage)
        {
            Logger.LogTrivial(FormatMessage(pMessage));
        }

        public void LogVerbose(string pMessage)
        {
            Logger.LogVerbose(FormatMessage(pMessage));
        }

        public void LogVeryVerbose(string pMessage)
        {
            Logger.LogVeryVerbose(FormatMessage(pMessage));
        }

        public void LogExtremelyVerbose(string pMessage)
        {
            Logger.LogExtremelyVerbose(FormatMessage(pMessage));
        }

        public void LogTrivialDebug(string pMessage)
        {
            Logger.LogTrivialDebug(FormatMessage(pMessage));
        }

        public void LogVerboseDebug(string pMessage)
        {
            Logger.LogVerboseDebug(FormatMessage(pMessage));
        }

        public void LogExtremelyVerboseDebug(string pMessage)
        {
            Logger.LogExtremelyVerboseDebug(FormatMessage(pMessage));
        }

        private string FormatMessage(string pMessage)
        {
            return string.Format("[{0}] {1}", ProductName, pMessage);
        }
    }
}
