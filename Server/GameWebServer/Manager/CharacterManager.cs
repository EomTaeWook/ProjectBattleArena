using BA.Repository;
using BA.Repository.Helper;
using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;

namespace GameWebServer.Manager
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private CharacterRepository _characterRepository;
        public CharacterManager()
        {
            _characterRepository = DBServiceHelper.GetService<CharacterRepository>();
        }
        public async Task<List<CharacterData>> LoadCharacterAsync(string account)
        {
            var loadCharacter = await _characterRepository.LoadCharacters(account);

            if(loadCharacter == null)
            {
                return null;
            }

            return loadCharacter;
        }
    }
}
