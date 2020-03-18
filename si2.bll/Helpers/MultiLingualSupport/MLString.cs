using System.Collections.Generic;
using System.Text.Json;

namespace si2.bll.Helpers.MultiLingualSupport
{ 
    public class MLString
    {
        private Dictionary<string, string> _values;
        public MLString(ILanguage language, string value)
        {
            _values = new Dictionary<string, string>();
            _values[language.Code] = value;
        }

        public MLString(string s)
        {
            _values = JsonSerializer.Deserialize<Dictionary<string, string>>(s);
        }

        public string Value(ILanguage language)
        {
            if (_values.ContainsKey(language.Code))
            {
                return _values[language.Code];
            }
            throw new UndefinedValueForLanguageException("Undefined value for language " + language.Name);
        }

        public void Set(ILanguage language, string value)
        {
            _values[language.Code] = value;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(_values);
        }
    }
}
