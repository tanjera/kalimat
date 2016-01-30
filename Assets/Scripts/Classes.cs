using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kalimat
{
    public class Vocabulary
    {
        string Title;
        string Description;
        string Source;
        string SourceDescription;
        int Difficulty;
        Languages LanguageTarget;
        Languages LanguageSource;
        string Dialect;
        List<string[]> WordPairs;
    }

    public enum Languages
    { // Using ISO 639-2 codes
        Ara,
        Eng,
        Spa,
        ETC
    }

    public class Player {
        string Username;
        string Name;
        string Hashpass;
        int PointsBank;
        int PointsTotal;
    }
}