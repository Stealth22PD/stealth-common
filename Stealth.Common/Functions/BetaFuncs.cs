using Stealth.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Functions
{
    public static class BetaFuncs
    {
        private static string AuthenticationEndpoint = "http://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkAuthorized&fileId={0}&rand={1}&betaKey={2}&textOnly=true";

        public static async Task<bool> IsValidKey(int fileId, string fileSecretUUID, string betaKey)
        {
            try
            {
                string rand = System.Guid.NewGuid().ToString();
                string result = "";

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        result = await client.DownloadStringTaskAsync(string.Format(AuthenticationEndpoint, fileId, rand, betaKey));
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogTrivial("Could not connect to LCPDFR.com to authenticate beta key: " + ex.Message);
                    return false;
                }

                string[] resultArray = result.Split('|');
                if (resultArray[0] != "SUCCESS")
                {
                    Logger.LogTrivial("Beta key authentication failed: Non-standard response");
                    return false;
                }

                int userId = Convert.ToInt32(resultArray[1]);
                string rxChecksum = resultArray[2];
                string expectedChecksum = await ComputeChecksum(resultArray[0], userId, rand, betaKey, fileSecretUUID);

                if (rxChecksum != expectedChecksum)
                {
                    Logger.LogTrivial("Beta key authentication failed: Checksum does not match");
                    return false;
                }

                Logger.LogTrivial(String.Format("Beta key authenticated as LCPDFR.com user ID {0}", userId));
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogTrivial("Beta key authentication failed: " + ex.Message);
                return false;
            }
        }

        private static async Task<string> ComputeChecksum(string response, int userId, string rand, string apiKey, string fileSecretUUID)
        {
            return await Task.Run(() =>
            {
                System.Security.Cryptography.SHA256Managed crypto = new System.Security.Cryptography.SHA256Managed();
                byte[] hashValue = crypto.ComputeHash(System.Text.Encoding.ASCII.GetBytes(String.Format("{0}{1}{2}{3}{4}", response, userId.ToString(), rand, apiKey, fileSecretUUID)));
                return System.BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            });
        }
    }
}
