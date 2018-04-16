using Rage;
using Stealth.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Functions
{
    public static class UpdateFuncs
    {
        public static void CheckForUpdates(int fileId, Version installedVersion, string productName, bool displayNotification = false, bool beta = false)
        {
            System.Threading.Tasks.Task.Factory.StartNew(async() =>
            {
                Logger.LogTrivial($"Checking for updates for {productName}...");
                Tuple<bool, string> updateResult = new Tuple<bool, string>(false, string.Empty);

                try
                {
                    updateResult = await IsNewerVersionAvailable(fileId, installedVersion, beta);

                    if (updateResult.Item1 == true)
                    {
                        Game.LogTrivial($"UPDATE AVAILABLE: Newer version (v{updateResult.Item2}) is available for {productName}");

                        if (displayNotification && updateResult.Item2 != string.Empty)
                        {
                            GameFuncs.DisplayNotification(productName, "Update Available!", $"v{updateResult.Item2}");
                        }
                    }
                    else
                    {
                        Game.LogTrivial($"No updates available for {productName}.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogTrivial($"ERROR: {ex.Message}");
                }
            });
        }

        private static async Task<Tuple<bool, string>> IsNewerVersionAvailable(int fileId, Version installedVersion, bool beta = false)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, string.Empty);

            string mApiURL = string.Format("http://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId={0}&beta={1}&textOnly=1", fileId, beta.ToString());
            string mApiString = "";

            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    mApiString = await wc.DownloadStringTaskAsync(mApiURL);
                }
            }
            catch (Exception ex)
            {
                mApiString = "";
                throw new Exception("Cannot connect to LCPDFR.com for update check: " + ex.Message);
            }

            try
            {
                if (mApiString != "")
                {
                    Version webVersion = Version.Parse(mApiString);
                    int versionFactor = webVersion.CompareTo(installedVersion);

                    if (webVersion > installedVersion)
                    {
                        //Game.DisplayNotification(string.Format("~g~NOTE: ~w~There is a newer version of ~b~{0} ~w~available.", mVersionInfo.ProductName));
                        //Logger.LogTrivial(string.Format("There is a newer version of {0} available. Please visit LCPDFR.com and download it.", mVersionInfo.ProductName));
                        result = new Tuple<bool, string>(true, webVersion.ToString());
                    }
                    else
                    {
                        result = new Tuple<bool, string>(false, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An exception occurred while comparing versions: " + ex.Message);
            }

            return result;
        }
    }
}
