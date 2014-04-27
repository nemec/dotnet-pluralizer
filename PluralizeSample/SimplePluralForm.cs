using System;

namespace PluralizeSample
{
    public class SimplePluralForm : IPluralForm
    {
        public SimplePluralForm(string plural, string description = null)
        {
            Value = plural;
            Description = description;
        }

        public string Value { get; private set; }

        public string Description { get; private set; }
    }
}

