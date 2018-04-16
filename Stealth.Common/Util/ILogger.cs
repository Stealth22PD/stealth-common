using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Util
{
    public interface ILogger
    {
        void LogTrivial(string pMessage);

        void LogVerbose(string pMessage);

        void LogVeryVerbose(string pMessage);

        void LogExtremelyVerbose(string pMessage);

        void LogTrivialDebug(string pMessage);

        void LogVerboseDebug(string pMessage);

        void LogExtremelyVerboseDebug(string pMessage);
    }
}
