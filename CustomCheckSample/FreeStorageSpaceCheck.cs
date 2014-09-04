using System;
using ServiceControl.Plugin.CustomChecks;

namespace CustomCheckSample
{
    public class FreeStorageSpaceCheck : PeriodicCheck
    {
        public FreeStorageSpaceCheck()
            : base("Free disk space check", "Storage", TimeSpan.FromSeconds(5)) { }

        public override CheckResult PerformCheck()
        {
            return CheckResult.Pass;
        }
    }
}
