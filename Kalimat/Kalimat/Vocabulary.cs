using System;
using System.Collections;
using System.Collections.Generic;

namespace Kalimat.Vocabulary
{
    public class Stack
    {
        public string Title;
        public string Description;
        public string Source;
        public string SourceDescription;
        public Languages Language;
        public List<string[]> WordPairs;

        public Stack(string incTitle, string incDescription, Languages incLanguage)
        {
            Title = incTitle;
            Description = incDescription;
            Language = incLanguage;
        }
        
        public string[] ListPairs()
        {
            List<string> newPairs = new List<string>();
            foreach (string[] eachPair in WordPairs)
                newPairs.Add(String.Format("{0} : {1}", eachPair[WordPair.Target.GetHashCode()], eachPair[WordPair.Source.GetHashCode()]));

            return newPairs.ToArray();
        }
        public string[] ListTargets()
        {
            List<string> newPairs = new List<string>();
            foreach (string[] eachPair in WordPairs)
                newPairs.Add(eachPair[WordPair.Target.GetHashCode()]);

            return newPairs.ToArray();
        }
    }

    public enum WordPair
    {
        Target,
        Source
    }

    public enum Languages
    {
        Arabic,
        Spanish
    }

    public class Stacks
    {
        public List<Stack> Listing = new List<Stack>
        {
            new Spanish_Numbers(),
            new Arabic_Numbers(),
            new Arabic_BeautyAndTheBeast()
        };

        public Stack GetStack(string Name)
        {
            for (int i = 0; i < Listing.Count; i++)
                if (Listing[i].ToString() == Name)
                    return Listing[i];

            return null;
        }
    }

    public class Spanish_Numbers : Stack
    {
        public Spanish_Numbers() : base("Numbers", "One to ten.", Languages.Spanish)
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

    public class Arabic_Numbers : Stack
    {
        public Arabic_Numbers() : base("Numbers", "One to ten.", Languages.Arabic)
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

    public class Arabic_BeautyAndTheBeast : Stack
    {
        public Arabic_BeautyAndTheBeast() : base("Beauty and the Beast", "Vocabulary from the short story.", Languages.Arabic)
        {
            WordPairs = new List<string[]>(new List<string[]>() {
                new string[] { "وحش", "beast" },
                new string[] { "مَرَت", "passed, go by" },
                new string[] { "قَرْية", "village" },
                new string[] { "غَيْر", "but" },
                new string[] { "قَصْرِ", "palace" },
                new string[] { "عيس", "to live" },
                new string[] { "فَتاةٌ", "girl" },
                new string[] { "دعى / تُدْعى", "called, is called..." },
                new string[] { "عشق / تَعْشَقُ", "to love / passion" },
                new string[] { "القراءة", "reading n" },
                new string[] { "أصْبَحَ", "become" },
                new string[] { "عادَة", "habit" },
                new string[] { "كُلَّما", "whenever" },
                new string[] { "غارقَةٌ", "overwhelmed, engrossed" },
                new string[] { "أهْلُ", "people" },
                new string[] { "مازح", "to joke" },
                new string[] { "لأحِظ", "to notice" },
                new string[] { "وُجودَ", "presence" },
                new string[] { "حتى", "until, even" },
                new string[] { "مَخْرورِ", "cocky, arrogant" },
                new string[] { "الَّذي", "which, that" },
                new string[] { "تباهى", "to boast" },
                new string[] { "أنْحاءِ", "around" },
                new string[] { "سَتُصْبِح", "will become" },
                new string[] { "يومًا ما", "one day" }
                });
        }
    }
}