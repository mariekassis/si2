using System.Collections.Generic;

namespace si2.bll.Helpers.MultiLingualSupport
{
    public class Languages
    {
        private Dictionary<string, ILanguage> _supportedLanguages;

        private static Languages instance = null;

        private Languages()
        {
            _supportedLanguages = new Dictionary<string, ILanguage>();
            Add(new Language("EN", "English"));
            Add(new Language("FR", "Francais"));
            Add(new Language("AR", "Arabic"));
        }

        public static Languages getInstance()
        {
            if (instance == null)
            {
                instance = new Languages();
            }
            return instance;
        }

        public ILanguage get(string code)
        {
            if (_supportedLanguages.ContainsKey(code) == false)
            {
                throw new UnsupportedLanguageException(code);
            }
            return _supportedLanguages[code];
        }

        private void Add(ILanguage language)
        {
            _supportedLanguages[language.Code] = language;
        }

        public ICollection<ILanguage> SupportedLanguages()
        {
            return _supportedLanguages.Values;
        }
    }
}
