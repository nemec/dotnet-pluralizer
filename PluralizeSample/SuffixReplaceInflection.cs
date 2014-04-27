using System;

namespace PluralizeSample
{
    public class SuffixReplaceInflection : IInflection
    {
        private readonly string _suffixMatch;

        private readonly string _suffixToReplace;

        private readonly string _suffixReplacement;

        public SuffixReplaceInflection(string suffix, string suffixReplacement)
        {
            _suffixMatch = suffix;
            _suffixToReplace = suffix;
            _suffixReplacement = suffixReplacement;
        }

        public SuffixReplaceInflection(string suffixMatch, string suffixToReplace, string suffixReplacement)
        {
            if(!suffixMatch.EndsWith(suffixToReplace))
            {
                throw new ArgumentException(String.Format(
                    "Suffix {0} must end with {1}.", suffixMatch, suffixToReplace));
            }
            _suffixMatch = suffixMatch;
            _suffixToReplace = suffixToReplace;
            _suffixReplacement = suffixReplacement;
        }

        public bool TryInflect(string word, out IPluralForm inflection)
        {
            if(word.EndsWith(_suffixMatch))
            {
                inflection = new SimplePluralForm(
                    word.Substring(0, word.Length - _suffixToReplace.Length) + _suffixReplacement, 
                    GetRule(word));
                return true;
            }
            inflection = null;
            return false;
        }

        private string GetRule(string word)
        {
            if(_suffixToReplace == null)
            {
                return String.Format("replace: {0}({1}) -> {0}({2})", 
                     word.Substring(0, word.Length - _suffixMatch.Length), 
                     _suffixMatch, _suffixReplacement);
            }
            var suffixRemaining = _suffixMatch.Substring(0, 
                _suffixMatch.Length - _suffixToReplace.Length);
            return String.Format("replace: {0}({1}) -> {0}({2}{3})",
                word.Substring(0, word.Length - _suffixMatch.Length), 
                _suffixMatch, suffixRemaining, _suffixReplacement);
        }

        
        public string RuleDisplay { get { return GetRule("word"); } }
    }
}

