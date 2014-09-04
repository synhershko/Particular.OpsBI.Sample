using System;
using ServiceControl.Plugin.CustomChecks;

namespace CustomCheckSample
{
    public class UnreliableBillingServiceAvailabilityCheck : PeriodicCheck
    {
        public UnreliableBillingServiceAvailabilityCheck()
            : base("Billing service availability check", "Network", TimeSpan.FromSeconds(5)) { }

        public override CheckResult PerformCheck()
        {
            if (SampleRandomization.IsPeak())
            {
                if (SampleRandomization.Random.Next()%3 == 0)
                    return CheckResult.Failed("Connection Timeout");
            }
            return CheckResult.Pass;
        }
    }
}
