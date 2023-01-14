using Dapper;
using System.Text;

namespace BA.Repository.Helper
{
    public static class DynamicParameterExtensions
    {
        public static void AddParam<T>(this DynamicParameters param, T value, string name, System.Data.ParameterDirection parameterDirection = System.Data.ParameterDirection.Input)
        {
            var paramName = $"param_{DBHelper.GetCamelCase(name)}";
            param.Add(paramName, value, direction: parameterDirection);
        }
    }
    public static class DBHelper
    {
        private static Dictionary<SP, string> _spToMap = new Dictionary<SP, string>();
        public static string GetSPName(this SP sp)
        {
            return _spToMap[sp];
        }
        public static void Build()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            for (var e = SP.LoadAuth; e < SP.Max; ++e)
            {
                _spToMap.Add(e, e.ToSPName());
            }

            DBServiceHelper.LoadAssembly();
        }
        public static string GetCamelCase(string stringValue)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in stringValue)
            {
                var ch = char.ToLower(item);
                if (char.IsUpper(item) == true && sb.Length > 0)
                {
                    sb.Append('_');
                }
                sb.Append(ch);
            }
            return sb.ToString();
        }
        private static string ToSPName(this SP sp)
        {
            return GetCamelCase(sp.ToString());
        }
        public enum SP
        {
            LoadAuth,
            CreateAuth,
            InsertUserLog,
            CreateCharacter,
            LoadCharacters,
            InsertUserAsset,
            LoadUserAsset,
            UpdateGold,
            UpdateCash,
            UpdateArenaTicket,
            LoadCharacterByCharacterName,
            LoadLatestSecurityKey,
            InsertSecurityKey,
            CreateMountingSkill,
            UpdateMountingSkill,
            UnMountingSkill,
            UpdateGachaSkill,
            InsertSkill,
            LoadSkillByCharacterName,
            LoadMountingSkillByCharacterName,
            LoadSkillByCharacterNameAndId,

            Max
        }
    }
}
