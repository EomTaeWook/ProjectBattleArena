using Protocol.GameWebServerAndClient.ShareModels;

namespace BA.Models
{
    public class ChangedAssetLogModel
    {
        public long CurrentValue { get; set; }

        public long UpdateValue { get; set; }

        public ChangedAssetReason Reason { get; set; }
    }
}
