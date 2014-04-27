using System;

namespace PluralizeSample
{
    public class ExactInflection : IInflection
    {
        private readonly string _wordToMatch;

        private readonly string _replacement;

        public ExactInflection(string wordToMatch, string replacement)
        {
            _wordToMatch = wordToMatch;
            _replacement = replacement;
        }

        public bool TryInflect(string word, out IPluralForm inflection)
        {
            // to allow for compound words like salesperson -> salespeople
            if(word.EndsWith(_wordToMatch))
            {
                inflection = new SimplePluralForm(
                    word.Substring(0, word.Length - _wordToMatch.Length) + _replacement, 
                    GetRule(word));
                return true;
            }
            inflection = null;
            return false;
        }

        private string GetRule(string word)
        {
            return String.Format("Match: {0} -> {1}", word, _replacement);
        }

        public string RuleDisplay { get { return GetRule(_wordToMatch); } }
    }
}

