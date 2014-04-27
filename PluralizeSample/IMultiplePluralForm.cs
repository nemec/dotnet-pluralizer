using System;
using System.Collections.Generic;

namespace PluralizeSample
{
    public interface IMultiplePluralForm : IPluralForm
    {
        IEnumerable<string> Forms { get; }
    }
}

