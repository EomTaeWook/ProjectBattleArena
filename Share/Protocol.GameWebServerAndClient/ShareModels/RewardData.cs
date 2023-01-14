using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.GameWebServerAndClient.ShareModels
{
    public class RewardData
    {
        public int GachaSkillDiff { get; set; }

        public int CashDiff { get; set; }

        public List<SkillData> AcquiredSkills { get; set; } = new List<SkillData>();

        public void Add(RewardData other)
        {
            AcquiredSkills.AddRange(other.AcquiredSkills);

            GachaSkillDiff += other.GachaSkillDiff;
            CashDiff += other.CashDiff;
        }
    }
}
