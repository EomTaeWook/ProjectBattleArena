using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Models
{
    public class GiveRewardLog
    {
        public RewardData RewardData { get; set; }

        public GiveRewardReason GiveRewardReason { get; set; }
    }
}
