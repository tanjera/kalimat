using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
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
    }
}
