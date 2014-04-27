using System;

namespace PluralizeSample
{
    public interface IInflection
    {
        bool TryInflect(string word, out IPluralForm inflection);

        string RuleDisplay { get; }
    }
}

