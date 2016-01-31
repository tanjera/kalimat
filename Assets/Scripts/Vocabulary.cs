using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;

namespace Kalimat
{
    public static class Text
    {
        public static string LTRRTL(string incText, Kalimat.Vocab.Languages incLanguage)
        {   // Fix RTL script since Unity doesn't support it...
            if (incLanguage == Vocab.Languages.Arabic)
                return ArabicFixer.Fix(incText);
            else
                return incText;
        }
    }
}

namespace Kalimat.Vocab
{
    public class Stack
    {
        public string Title;
        public string Description;
        public string Source;
        public string SourceDescription;
        public Difficulties Difficulty;
        public Languages LanguageTarget;
        public Languages LanguageSource;
        public string Dialect;
        public List<string[]> WordPairs;

        public Stack(string incTitle, string incDescription, Languages incTarget, Languages incSource, Difficulties incDifficulty)
        {
            Title = incTitle;
            Description = incDescription;
            LanguageTarget = incTarget;
            LanguageSource = incSource;
            Difficulty = incDifficulty;
        }
    }

    public enum WordPair
    {
        Target,
        Source
    }

    public enum Languages
    { // Using ISO 639-2 codes
        Arabic,
        English,
        Spanish,
        ETC
    }

    public enum Difficulties
    {
        Elementary,
        Junior,
        Senior,
        Technical
    }

    public class Test_Spanish : Stack
    {
        public Test_Spanish() : base("Test Stack: Spanish", "Testing, testing...", Languages.Spanish, Languages.English, Difficulties.Elementary)
        {
            WordPairs = new List<string[]>(new List<string[]>() {
                new string[] { "Cero", "Zero" },
                new string[] { "Uno", "One" },
                new string[] { "Dos", "Two" },
                new string[] { "Tres", "Three" },
                new string[] { "Cuatro", "Four" },
                new string[] { "Cinco", "Five" },
                new string[] { "Seis", "Six" },
                new string[] { "Siete", "Seven" },
                new string[] { "Ocho", "Eight" },
                new string[] { "Nueve", "Nine" },
                new string[] { "Diez", "Ten" }
                });
        }
    }

    public class Test_Arabic : Stack
    {
        public Test_Arabic() : base("Test Stack: Arabic", "Testing, testing...", Languages.Arabic, Languages.English, Difficulties.Elementary)
        {
            WordPairs = new List<string[]>(new List<string[]>() {
                new string[] { "صفر", "Zero" },
                new string[] { "واحد", "One" },
                new string[] { "اثنان", "Two" },
                new string[] { "ثلاثة", "Three" },
                new string[] { "أربعة", "Four" },
                new string[] { "خمسة", "Five" },
                new string[] { "ستة", "Six" },
                new string[] { "سبعة", "Seven" },
                new string[] { "ثمانية", "Eight" },
                new string[] { "تسعة", "Nine" },
                new string[] { "عشرة", "Ten" }
                });
        }
    }
}