using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace NewsInCSharp.Accessories
{
    public static class Utils
    {
        private static readonly RandomizerTextNaughtyStrings _randomizerTextNaughtyStrings = new RandomizerTextNaughtyStrings(new FieldOptionsTextNaughtyStrings());
        private static readonly RandomizerTextWords _randomizerTextWords = new RandomizerTextWords(new FieldOptionsTextWords());
        private static readonly RandomizerNumber<double> _doubleRandomizer = new RandomizerNumber<double>(new FieldOptionsDouble());

        public static double GetRandomPrice()
        {
            return _doubleRandomizer.Generate().GetValueOrDefault();
        }

        public static string GetNaughtyString(bool upperCase = false)
        {
            return _randomizerTextNaughtyStrings.Generate(upperCase)!;
        }

        public static string GetRandomWords(bool upperCase = false)
        {
            return _randomizerTextWords.Generate(upperCase)!;
        }
    }
}
