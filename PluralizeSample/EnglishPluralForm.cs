using System;
using System.Collections.Generic;

namespace PluralizeSample
{
	public class EnglishPluralForm : IMultiplePluralForm
	{
		public EnglishPluralForm (string anglicized, string classical, 
            PluralFormPreference preference = PluralFormPreference.Classical,
            string description = null)
		{
			Anglicized = anglicized;
			Classical = classical;
            Preference = preference;
            Description = description;

            switch(Preference)
            {
                case PluralFormPreference.Classical:
                    Value = Classical ?? Anglicized;
                    break;
                case PluralFormPreference.Anglicized:
                    Value = Anglicized ?? Classical;
                    break;
            }
		}

        public readonly PluralFormPreference Preference;

		public readonly string Anglicized;

		public readonly string Classical;

		public string Value { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<string> Forms
        {
            get 
            {
                yield return Anglicized;
                yield return Classical;
            }
        }
    }

    public enum PluralFormPreference
    {
        Anglicized,
        Classical
    }
}

