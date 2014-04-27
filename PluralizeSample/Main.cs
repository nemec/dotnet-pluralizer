using System;
using System.Linq;

namespace PluralizeSample
{
	class MainClass
	{
        public static EnglishPluralizer Pluralizer { get; set; }

        public static int FailCount = 0;

        public static void Assert(string toPluralize, string expected)
        {
            var plural = Pluralizer.GetPluralForm(toPluralize);
            if(expected != plural.Value)
            {
                var oldBg = Console.BackgroundColor;
                var oldFg = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} -> {1}, expected {2}", 
                                  toPluralize, plural.Value, expected);
                Console.WriteLine(String.Join("\n", 
                    plural.Description.Split('\n').Select(d => "\t" + d)));
                Console.BackgroundColor = oldBg;
                Console.ForegroundColor = oldFg;
                FailCount++;
            }
        }
		public static void Main (string[] args)
		{
            Pluralizer = new EnglishPluralizer();

            Assert("cheese", "cheeses");
            Assert("chinese", "chinese");
            Assert("codex", "codices");
            Assert("stigma", "stigmata");
            Assert("church", "churches");
            Assert("class", "classes");
            Assert("life", "lives");
            Assert("wolf", "wolves");
            Assert("knife", "knives");
            Assert("scarf", "scarves");
            Assert("leaf", "leaves");
            Assert("deaf", "deaf");
            Assert("roof", "roofs");
            Assert("safe", "safes");
            Assert("protozoon", "protozoa");
            Assert("protozoan", "protozoa");
            Assert("inferno", "infernos");
            Assert("sex", "sexes");
            Assert("cortex", "cortices");
            Assert("schema", "schemata");
            Assert("canto", "cantos");
            Assert("solo", "solos");
            Assert("phenomenon", "phenomena");
            Assert("memorandum", "memoranda");
            Assert("ovum", "ova");
            Assert("radius", "radii");
            Assert("impetus", "impetuses");
            Assert("cherub", "cherubim");
            Assert("sphinx", "sphinxes");
            Assert("buzz", "buzzes");
            Assert("campus", "campuses");
            Assert("werewolf", "werewolves");
            Assert("cow", "cows");
            Assert("suffix", "suffixes");
            Assert("bucktooth", "buckteeth");
            Assert("flora", "florae");  // floras
            Assert("vacuum", "vacuums");  // vacua
            Assert("corf", "corves");
            Assert("greave", "greaves");
            Assert("oaf", "oafs");
            Assert("meatloaf", "meatloaves");
            Assert("rebus", "rebuses");
            Assert("paradox", "paradoxes");
            Assert("cox", "coxes");
            Assert("coxswain", "coxswains");
            Assert("goof", "goofs");
            Assert("proof", "proofs");
            Assert("reef", "reefs");
            Assert("neckerchief", "neckerchiefs");
            Assert("magma", "magmas");
            Assert("drama", "dramas");  // dramata
            Assert("ranch", "ranches");

            // From humanizer
            Assert("atlas", "atlases");
            Assert("cod", "cod");
            Assert("domino", "dominoes");
            Assert("echo", "echoes");
            Assert("hero", "heroes");
            Assert("hoof", "hooves");
            Assert("iris", "irises");
            Assert("leaf", "leaves");
            Assert("loaf", "loaves");
            Assert("motto", "mottoes");
            Assert("reflex", "reflexes");
            Assert("sheaf", "sheaves");
            Assert("syllabus", "syllabi");
            Assert("thief", "thieves");
            Assert("waltz", "waltzes");
            Assert("gas", "gases");
            Assert("focus", "foci"); // /focuses
            Assert("nucleus", "nuclei");
            Assert("radius", "radii");
            Assert("stimulus", "stimuli");
            Assert("appendix", "appendices");
            Assert("beau", "beaux");
            Assert("corpus", "corpora");
            Assert("criterion", "criteria");
            Assert("curriculum", "curricula");
            Assert("genus", "genera");
            Assert("memorandum", "memoranda");
            Assert("offspring", "offspring");
            Assert("foot", "feet");
            Assert("tooth", "teeth");
            Assert("nebula", "nebulae");
            Assert("vertebra", "vertebrae");
            Assert("search", "searches");
            Assert("switch", "switches");
            Assert("fix", "fixes");
            Assert("box", "boxes");
            Assert("process", "processes");
            Assert("address", "addresses");
            Assert("case", "cases");
            Assert("stack", "stacks");
            Assert("wish", "wishes");
            Assert("fish", "fish");
            Assert("category", "categories");
            Assert("query", "queries");
            Assert("ability", "abilities");
            Assert("agency", "agencies");
            Assert("movie", "movies");
            Assert("archive", "archives");
            Assert("index", "indices");
            Assert("wife", "wives");
            Assert("half", "halves");
            Assert("move", "moves");
            Assert("salesperson", "salespeople");
            Assert("person", "people");
            Assert("spokesman", "spokesmen");
            Assert("man", "men");
            Assert("woman", "women");
            Assert("basis", "bases");
            Assert("diagnosis", "diagnoses");
            Assert("datum", "data");
            Assert("medium", "media");
            Assert("analysis", "analyses");
            Assert("node_child", "node_children");
            Assert("child", "children");
            Assert("experience", "experiences");
            Assert("day", "days");
            Assert("comment", "comments");
            Assert("foobar", "foobars");
            Assert("newsletter", "newsletters");
            Assert("old_news", "old_news");
            Assert("news", "news");
            Assert("series", "series");
            Assert("species", "species");
            Assert("quiz", "quizzes");
            Assert("perspective", "perspectives");
            Assert("ox", "oxen");
            Assert("photo", "photos");
            Assert("buffalo", "buffaloes");
            Assert("tomato", "tomatoes");
            Assert("dwarf", "dwarves");
            Assert("elf", "elves");
            Assert("information", "information");
            Assert("equipment", "equipment");
            Assert("bus", "buses");
            Assert("status", "statuses");
            Assert("status_code", "status_codes");
            Assert("mouse", "mice");
            Assert("louse", "lice");
            Assert("house", "houses");
            Assert("octopus", "octopodes");
            Assert("alias", "aliases");
            Assert("portfolio", "portfolios");
            Assert("vertex", "vertices");
            Assert("matrix", "matrices");
            Assert("axis", "axes");
            Assert("testis", "testes");
            Assert("crisis", "crises");
            Assert("rice", "rice");
            Assert("shoe", "shoes");
            Assert("horse", "horses");
            Assert("prize", "prizes");
            Assert("edge", "edges");
            Assert("goose", "geese");
            Assert("deer", "deer");
            Assert("sheep", "sheep");
            Assert("wolf", "wolves");
            Assert("volcano", "volcanoes");
            Assert("aircraft", "aircraft");
            Assert("alumna", "alumnae");
            Assert("alumnus", "alumni");
            Assert("fungus", "fungi");

            
            Assert("virus", "viruses");
            Assert("cactus", "cacti");
            //Assert("safe", "saves");

            Console.WriteLine(FailCount + " failed.");
		}
	}
}
