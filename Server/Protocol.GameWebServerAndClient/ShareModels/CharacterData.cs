using Protocol.GameWebServerAndClient.ShareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.GameWebServerAndClient.ShareModel
{
    public class CharacterData
    {
        public string CharacterName { get; set; }

        public JobType Job { get; set; }

        public int Exp { get; set; }

        public int Str { get; set; }

        public int Wiz { get; set; }

        public int Con { get; set; }

        public int Dex { get; set; }

        public int StatPoint { get; set; }

        public long CreateTime { get; set; }
    }
}
