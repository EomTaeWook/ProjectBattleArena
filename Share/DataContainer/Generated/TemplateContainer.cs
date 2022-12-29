using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
namespace TemplateContainers
{
    public class TemplateContainer<T> where T : BaseTemplate
    {
        public static IEnumerable<T> Values => _dataToMap.Values;
        private static readonly Dictionary<int, T> _dataToMap = new Dictionary<int, T>();
        private static readonly Dictionary<string, T> _nameKeyToMap = new Dictionary<string, T>();
        public static T Find(int id)
        {
            if (_dataToMap.ContainsKey(id) == false)
            {
                return default;
            }
            return _dataToMap[id];
        }
        public static T Find(string name)
        {
            if (_nameKeyToMap.ContainsKey(name) == false)
            {
               return default;
            }
            return _nameKeyToMap[name];
        }
        public static void Load(string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName);
            using (var stream = new StreamReader(File.OpenRead(fullPath)))
            {
                var content = stream.ReadToEnd();
                var templateDatas = JsonConvert.DeserializeObject<List<T>>(content);
                foreach (var template in templateDatas)
                {
                    _dataToMap.Add(template.Id, template);
                    _nameKeyToMap.Add(template.Name, template);
                }
            }
        }
        public static void MakeRefTemplate()
        {
            foreach(var template in _dataToMap.Values)
            {
                template.MakeRefTemplate();
                template.Combine();
            }
        }
    }
}
