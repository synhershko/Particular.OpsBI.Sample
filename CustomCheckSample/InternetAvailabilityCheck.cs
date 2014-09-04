using System;
using ServiceControl.Plugin.CustomChecks;

namespace CustomCheckSample
{
    public class InternetAvailabilityCheck : PeriodicCheck
    {
        public InternetAvailabilityCheck()
            : base("Internet availability check", "Network", TimeSpan.FromSeconds(5)) { }

        public override CheckResult PerformCheck()
        {

            if (!CheckInternetConnection())
                return CheckResult.Failed("Internet connection is not available");
            else
                return CheckResult.Pass;
        }

        private static bool CheckInternetConnection()
        {
            var dir = @"C:\Checks\internet";
            return System.IO.Directory.Exists(dir);
        }
    }
}
