using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralizeSample
{
    /// <summary>
    /// An inflection that performs multiple inflections and then combines
    /// them into one plural form. Useful when a single word can have
    /// multiple valid plural forms.
    /// </summary>
    public class CompositeInflection : IInflection
    {
        private readonly IEnumerable<IInflection> _inflections;

        private readonly Func<List<IPluralForm>, IPluralForm> _aggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralizeSample.CompositeInflection"/> class.
        /// </summary>
        /// <param name='aggregator'>
        /// Take all inflected plural forms and combine them into one.
        /// </param>
        /// <param name='inflections'>
        /// Inflections.
        /// </param>
        public CompositeInflection(
            Func<List<IPluralForm>, IPluralForm> aggregator,
            params IInflection[] inflections)
        {
            _inflections = inflections;
            _aggregator = aggregator;
        }

        public bool TryInflect(string word, out IPluralForm result)
        {
            var inflectionFound = false;
            var plurals = new List<IPluralForm>();
            foreach(var inflection in _inflections)
            {
                IPluralForm plural;
                if(inflection.TryInflect(word, out plural))
                {
                    plurals.Add(plural);
                    inflectionFound = true;
                }
                else
                {
                    plurals.Add(new SimplePluralForm(null));
                }
            }
            if(inflectionFound)
            {
                result = _aggregator(plurals);
            }
            else
            {
                result = null;
            }
            return inflectionFound;
        }

        public string RuleDisplay
        {
            get
            {
                return String.Join("\n", _inflections.Select(i => i.RuleDisplay));
            }
        }
    }
}

