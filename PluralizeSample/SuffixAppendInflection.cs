using System;

namespace PluralizeSample
{
    public class SuffixAppendInflection : IInflection
    {
        private readonly string _suffix;

        private readonly string _extraSuffix;

        public SuffixAppendInflection(string suffix, string extraSuffix)
        {
            _suffix = suffix;
            _extraSuffix = extraSuffix;
        }

        public bool TryInflect(string word, out IPluralForm inflection)
        {
            if(word.EndsWith(_suffix))
            {
                inflection = new SimplePluralForm(word + _extraSuffix, GetRule(word));
                return true;
            }
            inflection = null;
            return false;
        }

        private string GetRule(string word)
        {
            return String.Format("append: {0}({1}) -> {0}({1}{2})",
                word.Substring(0, word.Length - _suffix.Length), 
                _suffix, _extraSuffix);
        }

        public string RuleDisplay { get { return GetRule("word"); } }
    }
}

