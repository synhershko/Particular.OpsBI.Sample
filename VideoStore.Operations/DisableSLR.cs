using NServiceBus;
using NServiceBus.Features;

namespace VideoStore.Operations
{
    public class DisableSLR : INeedInitialization
    {
        public void Init()
        {
            // Using code we disable the second level retries.            
            Configure.Features.Disable<SecondLevelRetries>();
        }
    }
}