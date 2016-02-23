using System;
using System.IO;
using System.Collections.Generic;
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

        public Player Player_Get(string incUser)
        {   // Returns the newest copy of the player
            Data_Server dServ = new Data_Server();

            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Player>();

            TableQuery<Player> qryPlayer = db.Table<Player>();

            Player remPlayer = dServ.Player_Get(incUser);
            Player locPlayer = null;
            
            foreach (Player eachPlayer in qryPlayer)
                if (eachPlayer.Username == incUser)
                    locPlayer = eachPlayer;

            if (DateTime.Compare(locPlayer.Timestamp, remPlayer.Timestamp) < 0)
            {
                bool existsLocally = locPlayer != null;
                locPlayer = remPlayer;
                if (existsLocally)
                    db.InsertOrReplace(locPlayer);
                db.Close();
                return remPlayer;
            }
            else
            {
                dServ.Player_Update(locPlayer);
                return locPlayer;
            }
        }

        public Player Player_Default()
        {   // Query to see if any player exists locally

            Data_Server dServ = new Data_Server();

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
                return Player_Get(toReturn.Username);
            }
        }
        public bool Player_Exists(Player incPlayer)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Player>();

            TableQuery<Player> qryPlayer = db.Table<Player>();
            foreach (Player eachPlayer in qryPlayer)
                if (eachPlayer == incPlayer)
                {
                    db.Close();
                    return true;    // Return player exists!
                }

            db.Close();
            return false;   // Return player does not exist
        }
        public bool Player_Add(Player incPlayer)
        {
            if (Player_Exists(incPlayer))
                return false;

            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Player>();
            db.Insert(incPlayer);
            db.Close();
            return true;
        }
        public bool Player_Deposit_Quiz(string incUser, string incUID, int incPoints)
        {
            Data_Server dServ = new Data_Server();

            Player incPlayer = Player_Get(incUser);
            // Update the player's bank locally
            incPlayer.Points += incPoints;
            incPlayer.Timestamp = DateTime.Now;
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.InsertOrReplace(incPlayer);
            db.Close();
            // And update the player's bank on the server
            dServ.Player_Deposit_Quiz(incUser, incPoints, incUID);

            return true;
        }

        public Stack Stack_Get(string incUID)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stack>();

            TableQuery<Stack> qryStack = db.Table<Stack>();
            foreach (Stack eachStack in qryStack)
                if (eachStack.UID == incUID)
                {
                    db.Close();
                    return eachStack;
                }

            db.Close();
            return null;
        }
        public bool Stack_Exists(string incUID)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stack>();

            TableQuery<Stack> qryStack = db.Table<Stack>();
            foreach (Stack eachStack in qryStack)
                if (eachStack.UID == incUID)
                {
                    db.Close();
                    return true;
                }

            db.Close();
            return false;
        }
        public bool Stack_Purchase(string incUsername, string incUID)
        {
            if (Stack_Exists(incUID))
                return false;   // Purchase failed, stack already exists locally

            SQLiteConnection db = new SQLiteConnection(dbPath);
            Data_Server dServ = new Data_Server();

            Stack incStack = dServ.Stack_Get(incUID);

            db.CreateTable<Stack>();
            db.Insert(incStack);
            db.Close();
            return true;
        }
        public bool Stack_Purchase_Points(string incUser, string incUID)
        {
            Data_Server dServ = new Data_Server();
            Stack incStack = dServ.Stack_Get(incUID);
            int i;
            if ((i = dServ.Player_Points_Get(incUser)) < incStack.Price_Points
                || !Stack_Purchase(incUser, incUID))
                return false;
            else
            {
                Player incPlayer = Player_Get(incUser);
                // Update the player's bank locally
                incPlayer.Points -= incStack.Price_Points;
                incPlayer.Timestamp = DateTime.Now;
                SQLiteConnection db = new SQLiteConnection(dbPath);
                db.InsertOrReplace(incPlayer);
                db.Close();
                // And update the player's bank on the server
                dServ.Player_Purchase_Points(incUser, incStack.Price_Points, incStack.UID);

                return true;
            }
        }

        public List<string> List_Languages()
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<Stack>();
            List<string> listLangs = new List<string>();

            TableQuery<Stack> qryStacks = db.Table<Stack>();
            foreach (Stack eachStack in qryStacks)
                if (!listLangs.Contains(eachStack.Language))
                    listLangs.Add(eachStack.Language);

            db.Close();
            return listLangs;
        }
        public List<Stack> List_Stacks_ByLanguage(string incLang)
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
    }
}
