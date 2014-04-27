using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PluralizeSample
{
	public class EnglishPluralizer
	{
		private readonly EnglishPluralizerData _data;

		public EnglishPluralizer()
		{
			_data = new EnglishPluralizerData();
		}

        public string GetPluralWord(string word)
        {
            return GetPluralForm(word).Value;
        }

		public IPluralForm GetPluralForm(string word)
		{
            IPluralForm plural;
            foreach(var cust in _data.CustomInflections)
            {
                if(cust.TryInflect(word, out plural))
                {
                    return plural;
                }
            }

            foreach(var infl in _data.SingularIrregular)
            {
                if(infl.TryInflect(word, out plural))
                {
                    return plural;
                }
            }

			// Handle words that do not inflect in the plural (such as fish)
			if (_data.InflectionlessSuffixes.Any (s => word.EndsWith (s)) ||
				_data.InflectionlessRegex.Any (r => r.IsMatch (word)))
			{
                return new SimplePluralForm(word, "inflectionless: word -> word");
			}

            // Handle pronouns in the nominative, accusative, and dative
            // NOTE: skipping prepositional phrases
            if(_data.SingularPronoun.TryGetValue(word, out plural))
            {
                return plural;
            }

            if(_data.SingularCategorySuffix.TryGetValue(word, out plural))
            {
                return plural;
            }

            foreach(var infl in _data.SingularSuffix)
            {
                if(infl.TryInflect(word, out plural))
                {
                    return plural;
                }
            }
            if(!word.EndsWith("s")){
                return new SimplePluralForm(word + "s", "fallback: word -> words");
            }
            else{
                return new SimplePluralForm(word + "es", "fallback s: alias -> alias(es)");
            }
		}
	}
}

