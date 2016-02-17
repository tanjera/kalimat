using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text;
using System.Xml;
using SQLite;

namespace Kalimat
{
    public class Data_Local
    {
        static string dbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "kalimat.db3");

        public Player GetPlayer()
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Player>();

            TableQuery<Player> qryPlayer = db.Table<Player>();

            if (qryPlayer.Count() == 0)
            {
                db.Close();
                return null;
            }
            else
            {
                Player toReturn = qryPlayer.ElementAt(0);
                db.Close();
                return toReturn;
            }
        }

        public bool AddPlayer(Player incPlayer)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Player>();

            TableQuery<Player> qryPlayer = db.Table<Player>();
            foreach (Player eachPlayer in qryPlayer)
                if (eachPlayer == incPlayer)
                {
                    db.Close();
                    return false;
                }

            db.Insert(incPlayer);
            db.Close();
            return true;
        }


        /*  SQLite does not support List<string> or string[][]...
            until wordpairs are stores as formatted strings (XML?), need to hardcode
            this & actLibraryLanguages/actLibraryStacks


        public List<Languages> GetLibrary_LanguageList()
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stack>();
            List<Languages> listLangs = new List<Languages>();

            TableQuery<Stack> qryStacks = db.Table<Stack>();
            foreach (Stack eachStack in qryStacks)
                if (!listLangs.Contains(eachStack.Language))
                    listLangs.Add(eachStack.Language);

            db.Close();
            return listLangs;
        }
        public string[] GetLibrary_LanguageStrings()
        {
            List<Languages> listLangs = GetLibrary_LanguageList();
            List<string> listStrings = new List<string>();
            listLangs.ForEach(obj => listStrings.Add(obj.ToString()));
            return listStrings.ToArray();
        }

        public List<Stack> GetLibrary_StacksOfLanguage(Languages incLang)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stack>();
            List<Stack> listStacks = new List<Stack>();

            TableQuery<Stack> qryStacks = db.Table<Stack>();
            foreach (Stack eachStack in qryStacks)
                if (eachStack.Language == incLang)
                    listStacks.Add(eachStack);

            db.Close();
            return listStacks;
        }
        */
    }
}
