using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PluralizeSample
{
	internal class EnglishPluralizerData
	{
		// http://www.csse.monash.edu.au/~damian/papers/HTML/Plurals.html

		// perhaps also http://www.clips.ua.ac.be/pages/pattern-en

        // http://www.visca.com/regexdict/ lookup suffixes here

        public readonly List<IInflection> CustomInflections = new List<IInflection>();

		/// <summary>
		/// Inflections that replace the suffix in a word with a different suffix.
        /// Matches on the exact word.
		/// </summary>
		public readonly Dictionary<string, IPluralForm> SingularCategorySuffix = new Dictionary<string, IPluralForm>();

        public readonly List<IInflection> SingularSuffix = new List<IInflection>();

		/// <summary>
		/// Hardcoded mappings from a singular word to its plural.
		/// </summary>
		public readonly List<IInflection> SingularIrregular = new List<IInflection>();

        /// <summary>
        /// The singular pronoun inflections.
        /// </summary>
        public readonly Dictionary<string, IPluralForm> SingularPronoun = new Dictionary<string, IPluralForm>();

		public readonly List<string> InflectionlessSuffixes = new List<string>();

		public readonly List<Regex> InflectionlessRegex = new List<Regex>();

		public EnglishPluralizerData()
		{
			#region hardcoded suffix mappings

			AddSuffixCategory("a", null, "ae", new[] {
				"alumna", "alga", "vertebra"
			});

			AddSuffixCategory("a", "as", "ae", new[] {
				"abscissa", "formula", "medusa", "amoeba", "hydra", "nebula",
				"antenna", "hyperbola", "nova", "aurora", "lacuna", "parabola"
			});

            AddSuffixCategory("a", "as", "ata", new[] {
                "anathema", "enema", "oedema", "bema", "enigma", "sarcoma", 
                "carcinoma", "gumma", "charisma", "lemma", "soma", "diploma", 
                "lymphoma", "dogma", "magma", "stoma", "drama", "melisma", 
                "trauma", "edema", "miasma"
            }, PluralFormPreference.Anglicized);

            AddSuffixCategory("a", "as", "ata", new[] {
                "stigma", "schema"
            }, PluralFormPreference.Classical);
			
			AddSuffixCategory("en", "ens", "ina", new[] {
				"stamen", "foramen", "lumen"
			});

			AddSuffixCategory("ex", null, "ices", new[] {
				"codex", "murex", "silex"
			});

			AddSuffixCategory("ex", "exes", "ices", new[] {
				"apex", "latex", "vertex", "cortex", "pontifex", "vortex", 
				"index", "simplex"
			});

			AddSuffixCategory("is", "ises", "ides", new[] {
				"iris", "clitoris"
			}, PluralFormPreference.Anglicized);

			AddSuffixCategory("o", "os", null, new[] {
				"albino", "generalissimo", "manifesto", "archipelago", 
				"ghetto", "medico", "armadillo", "guano", "octavo", 
				"commando", "inferno", "photo", "ditto", "jumbo", "pro", 
				"dynamo", "lingo", "quarto", "embryo", "lumbago", "rhino", 
				"fiasco", "magneto", "stylo"
			});

			AddSuffixCategory("o", "os", "i", new[] {
				"alto", "contralto", "soprano", "basso", "crescendo",
				"tempo", "canto", "solo"
			}, PluralFormPreference.Anglicized);

			AddSuffixCategory("on", null, "a", new[] {
				"aphelion", "hyperbaton", "perihelion", "asyndeton",
				"noumenon", "phenomenon", "criterion", "organon", 
				"prolegomenon"
			});

			AddSuffixCategory("um", null, "a", new[] {
				"agendum", "datum", "extremum", "bacterium", "desideratum",
				"stratum", "candelabrum", "erratum", "ovum"
			});

			AddSuffixCategory("um", "ums", "a", new[] {
				"aquarium", "interregnum", "quantum", "compendium",
				"lustrum", "rostrum", "consortium", "maximum", 
				"spectrum", "cranium", "medium", "speculum", 
				"curriculum", "memorandum", "stadium", "dictum", 
				"millenium", "trapezium", "emporium", "minimum", 
				"ultimatum", "enconium", "momentum", "vacuum", 
				"gymnasium", "optimum", "velum", "honorarium", 
				"phylum"
			});

            AddSuffixCategory("us", "uses", "i", new[] {
                "focus", "nimbus", "succubus", "fungus", "nucleolus", 
                "torus", "genius", "radius", "umbilicus", "incubus", 
                "stylus", "syllabus", "thrombus", "uterus"
            });

			AddSuffixCategory("us", "uses", "us", new[] {
				"apparatus", "impetus", "prospectus", "cantus", 
				"nexus", "sinus", "coitus", "plexus", "status", 
				"hiatus"
			}, PluralFormPreference.Anglicized);

			AddSuffixCategory("", null, "im", new[] {
				"cherub", "goy", "seraph"
			});

			#endregion

            #region pronouns
            
            SingularPronoun.Add("I", new SimplePluralForm("we"));
            SingularPronoun.Add("you", new SimplePluralForm("you"));  // y'all
            SingularPronoun.Add("thou", new SimplePluralForm("you"));
            SingularPronoun.Add("she", new SimplePluralForm("they"));
            SingularPronoun.Add("he", new SimplePluralForm("they"));
            SingularPronoun.Add("it", new SimplePluralForm("they"));
            SingularPronoun.Add("they", new SimplePluralForm("they"));
            SingularPronoun.Add("me", new SimplePluralForm("us"));
            SingularPronoun.Add("thee", new SimplePluralForm("you"));
            SingularPronoun.Add("her", new SimplePluralForm("them"));
            SingularPronoun.Add("him", new SimplePluralForm("them"));
            //SingularPronoun.Add("it", new SimplePluralForm("them"));  // duplicate key "it" -> "they"
            SingularPronoun.Add("them", new SimplePluralForm("them"));
            SingularPronoun.Add("myself", new SimplePluralForm("ourselves"));
            SingularPronoun.Add("yourself", new SimplePluralForm("yourself"));
            SingularPronoun.Add("thyself", new SimplePluralForm("yourself"));
            SingularPronoun.Add("herself", new SimplePluralForm("themselves"));
            SingularPronoun.Add("himself", new SimplePluralForm("themselves"));
            SingularPronoun.Add("itself", new SimplePluralForm("themselves"));
            SingularPronoun.Add("themself", new SimplePluralForm("themselves"));
            SingularPronoun.Add("oneself", new SimplePluralForm("oneselves"));

            #endregion

            #region inflectionless words
			
            InflectionlessSuffixes.Add("aircraft");
            InflectionlessSuffixes.Add("bison");
			InflectionlessSuffixes.Add("bream");
			InflectionlessSuffixes.Add("breeches");
			InflectionlessSuffixes.Add("britches");
			InflectionlessSuffixes.Add("carp");
			InflectionlessSuffixes.Add("chassis");
			InflectionlessSuffixes.Add("clippers");
			InflectionlessSuffixes.Add("cod");
			InflectionlessSuffixes.Add("contretemps");
			InflectionlessSuffixes.Add("corps");
			InflectionlessSuffixes.Add("debris");
			InflectionlessSuffixes.Add("deer");
			InflectionlessSuffixes.Add("diabetes");
			InflectionlessSuffixes.Add("djinn");
			InflectionlessSuffixes.Add("eland");
            InflectionlessSuffixes.Add("elk");
            InflectionlessSuffixes.Add("equipment");
            InflectionlessSuffixes.Add("fish");
            InflectionlessSuffixes.Add("flounder");
			InflectionlessSuffixes.Add("gallows");
			InflectionlessSuffixes.Add("graffiti");
			InflectionlessSuffixes.Add("headquarters");
			InflectionlessSuffixes.Add("herpes");
			InflectionlessSuffixes.Add("high-jinks");
			InflectionlessSuffixes.Add("homework");
            InflectionlessSuffixes.Add("information");
            InflectionlessSuffixes.Add("innings");
			InflectionlessSuffixes.Add("jackanapes");
			InflectionlessSuffixes.Add("mackerel");
			InflectionlessSuffixes.Add("measles");
			InflectionlessSuffixes.Add("mews");
			InflectionlessSuffixes.Add("mumps");
            InflectionlessSuffixes.Add("news");
            InflectionlessSuffixes.Add("offspring");
			InflectionlessSuffixes.Add("pincers");
			InflectionlessSuffixes.Add("pliers");
			InflectionlessSuffixes.Add("proceedings");
            InflectionlessSuffixes.Add("rabies");
            InflectionlessSuffixes.Add("rice");
			InflectionlessSuffixes.Add("salmon");
			InflectionlessSuffixes.Add("scissors");
			InflectionlessSuffixes.Add("sea-bass");
			InflectionlessSuffixes.Add("series");
			InflectionlessSuffixes.Add("shears");
			InflectionlessSuffixes.Add("sheep");
			InflectionlessSuffixes.Add("species");
			InflectionlessSuffixes.Add("swine");
			InflectionlessSuffixes.Add("travois");
			InflectionlessSuffixes.Add("trout");
			InflectionlessSuffixes.Add("tuna");
			InflectionlessSuffixes.Add("whiting");
			InflectionlessSuffixes.Add("wildebeest");

            InflectionlessSuffixes.Add("deaf"); // otherwise, rule for "leaf" -> "leaves" will take over

			InflectionlessSuffixes.Add("ois");
			InflectionlessSuffixes.Add("pox");
			InflectionlessSuffixes.Add("itis");
            InflectionlessSuffixes.Add("ese");

			#endregion

            #region irregular plurals
            
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("beef", "beefs"),
                new ExactInflection("beef", "beeves")));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("brother", "brothers"),
                new ExactInflection("brother", "brethren"),
                PluralFormPreference.Anglicized));
            SingularIrregular.Add(
                new ExactInflection("child", "children"));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("cow", "cows"),
                new ExactInflection("cow", "kine"),
                PluralFormPreference.Anglicized));
            SingularIrregular.Add(
                new ExactInflection("ephermeris", "ephemerides"));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("genie", "genies"),
                new ExactInflection("genie", "genii")));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("hoof", "hoofs"),
                new ExactInflection("hoof", "hooves")));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("money", "moneys"),
                new ExactInflection("money", "monies")));
            SingularIrregular.Add(
                new ExactInflection("mongoose", "mongooses"));
            SingularIrregular.Add(
                new ExactInflection("mythos", "mythoi"));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("octopus", "octopuses"),
                new ExactInflection("octopus", "octopodes")));
            SingularIrregular.Add(new RegexInflection("[bcd]ox", "es"));
            SingularIrregular.Add(
                new ExactInflection("ox", "oxen"));
            SingularIrregular.Add(
                new ExactInflection("soliloquy", "soliloquies"));
            SingularIrregular.Add(
                new ExactInflection("trilby", "trilbys"));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("person", "persons"),
                new ExactInflection("person", "people")));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("appendix", "appendixes"),
                new ExactInflection("appendix", "appendices")));
            SingularIrregular.Add(CreateEnglishComposite(
                new ExactInflection("vacuum", "vacuums"),
                new ExactInflection("vacuum", "vacua"),
                PluralFormPreference.Anglicized));
            SingularIrregular.Add(
                new ExactInflection("corpus", "corpora"));
            SingularIrregular.Add(
                new ExactInflection("genus", "genera"));
            SingularIrregular.Add(
                new ExactInflection("cheese", "cheeses"));

            #endregion

            #region irregular suffixes
            
            SingularSuffix.Add(new SuffixReplaceInflection("man", "men"));
            SingularSuffix.Add(new SuffixReplaceInflection("louse", "lice"));
            SingularSuffix.Add(new SuffixReplaceInflection("mouse", "mice"));
            SingularSuffix.Add(new SuffixReplaceInflection("goose", "geese"));
            SingularSuffix.Add(new SuffixReplaceInflection("foot", "feet"));
            SingularSuffix.Add(new SuffixReplaceInflection("zoon", "zoa"));
            SingularSuffix.Add(new SuffixReplaceInflection("zoan", "zoa"));
            SingularSuffix.Add(new SuffixReplaceInflection("cis", "is", "es"));
            SingularSuffix.Add(new SuffixReplaceInflection("sis", "is", "es"));
            SingularSuffix.Add(new SuffixReplaceInflection("xis", "is", "es"));
            SingularSuffix.Add(new SuffixAppendInflection("bus", "es"));
            SingularSuffix.Add(new SuffixReplaceInflection("tooth", "teeth"));
            SingularSuffix.Add(new SuffixReplaceInflection("thief", "thieves"));

            #endregion

            #region assimilated classical inflections

            SingularSuffix.Add(CreateEnglishComposite(
                new RegexInflection("[td]ex", "es"),
                new RegexInflection("[td](ex)", "ices")));
            SingularSuffix.Add(new SuffixAppendInflection("ex", "es"));
            SingularSuffix.Add(new SuffixReplaceInflection("um", "a"));
            SingularSuffix.Add(new SuffixReplaceInflection("on", "a"));
            SingularSuffix.Add(new SuffixReplaceInflection("a", "ae"));

            #endregion

            #region classical variants of modern inflections

            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("trix", "trixes"),
                new SuffixReplaceInflection("trix", "trices")));
            SingularSuffix.Add(new SuffixReplaceInflection("eau", "eaux"));
            SingularSuffix.Add(new SuffixReplaceInflection("ieu", "ieux"));
            // nx -> nges in author's algorithm, but it just doesn't sound right...
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("inx", "nx", "nxes"),
                new SuffixReplaceInflection("inx", "nx", "nges"),
                PluralFormPreference.Anglicized));
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("anx", "nx", "nxes"),
                new SuffixReplaceInflection("anx", "nx", "nges"),  // spanx -> spanges?
                PluralFormPreference.Anglicized));
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("ynx", "nx", "nxes"),
                new SuffixReplaceInflection("ynx", "nx", "nges"),
                PluralFormPreference.Anglicized));
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("en", "ens"),
                new SuffixReplaceInflection("en", "ina")));
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("a", "as"),
                new SuffixReplaceInflection("a", "ata")));
            SingularSuffix.Add(new SuffixReplaceInflection("is", "es"));
            /*SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("is", "ises"),
                new SuffixReplaceInflection("is", "ides")));*/
            AddSuffixCategory("us", "uses", "i", new []
            {
                "campus", "bonus", "virus", 
            }, PluralFormPreference.Anglicized);
            SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("us", "uses"),
                new SuffixReplaceInflection("us", "i")));
            /*SingularSuffix.Add(CreateEnglishComposite(
                new SuffixReplaceInflection("us", "uses"),  // nexuses
                new SuffixReplaceInflection("us", "us")));*/  // Conflicting...

            #endregion

            #region singular suffix
            
            SingularSuffix.Add(new SuffixAppendInflection("ch", "es"));
            SingularSuffix.Add(new SuffixAppendInflection("sh", "es"));
            SingularSuffix.Add(new SuffixAppendInflection("ss", "es"));
            SingularSuffix.Add(new SuffixAppendInflection("tz", "es"));
            SingularSuffix.Add(new RegexInflection("[nlw]i(fe)", "ves"));
            SingularSuffix.Add(new RegexInflection("[aeo]l(f)", "ves"));
            SingularSuffix.Add(new SuffixReplaceInflection("eaf", "f", "ves"));
            SingularSuffix.Add(new SuffixReplaceInflection("loaf", "f", "ves"));
            SingularSuffix.Add(new RegexInflection("[aou]r(f)", "ves"));
            SingularSuffix.Add(new RegexInflection("[aeiou](y)", "ys"));
            SingularSuffix.Add(new RegexInflection("[^aeiou](y)", "ies"));
            SingularSuffix.Add(new RegexInflection("[A-Z].*?(y)", "ys"));
            SingularSuffix.Add(new SuffixReplaceInflection("ay", "y", "ies"));

            // lassos, solos
            SingularSuffix.Add(new RegexInflection("[aeiou](o)", "os"));
            SingularSuffix.Add(new SuffixReplaceInflection("o", "oes"));
            // potatoes, dominoes

            // quiz -> quizzes, biz -> bizzes
            SingularSuffix.Add(new SuffixAppendInflection("iz", "zes"));
            SingularSuffix.Add(new SuffixAppendInflection("zz", "es"));

            SingularSuffix.Add(new RegexInflection("[io]x", "es"));

            #endregion
		}

		private void AddSuffixCategory (string suffix, string anglicizedEnding,
                                        string classicalEnding, string[] words, 
                                        PluralFormPreference pref = PluralFormPreference.Classical)
		{
			foreach (var word in words)
			{
				if(!word.EndsWith(suffix))
				{
					throw new ArgumentException(String.Format(
						"Word {0} must end with the suffix {1}. " +
						"Are you adding it to the wrong category?",
						word, suffix));
				}
                var substr = word.Substring(0, word.Length - suffix.Length);
                var angl = !String.IsNullOrEmpty(anglicizedEnding)
                    ? substr + anglicizedEnding
                    : null;
                var clas = !String.IsNullOrEmpty(classicalEnding)
                    ? substr + classicalEnding 
                    : null;

				SingularCategorySuffix.Add(word, new EnglishPluralForm(
                    angl, clas, pref, 
                    String.Format("{0} -> " + 
                        angl != null && clas != null 
                            ? "{1}/{2}"
                            : angl != null
                                ? "{1}"
                                : "{2}"  // classical must not be null
                              , word, angl, clas)));
			}	
		}

        private IInflection CreateEnglishComposite(IInflection anglicized, 
            IInflection classical, string description = null)
        {
            return new CompositeInflection(
                pl => new EnglishPluralForm(pl[0].Value, pl[1].Value, description: description),
                anglicized, classical);
        }

        private IInflection CreateEnglishComposite(IInflection anglicized, 
            IInflection classical, PluralFormPreference pref, string description = null)
        {
            return new CompositeInflection(
                pl => new EnglishPluralForm(pl[0].Value, pl[1].Value, pref, description),
                anglicized, classical);
        }
	}
}

