namespace si2.bll.Helpers.MultiLingualSupport
{
    class Language: ILanguage
    {
        public string Code { get; }
        public string Name { get; }

        public Language(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
