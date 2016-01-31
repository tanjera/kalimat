using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Kalimat
{
    [System.Serializable]
    public class Player {
        string Username;
        string Name;
        string Hashpass;
        int PointsBank;
        int PointsTotal;
    }

    public static class Serialize
    {
        public static void Save(Player incPlayer)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            FileStream fStr = File.Create(Application.persistentDataPath + "/SaveGame.klm");
            binForm.Serialize(fStr, incPlayer);
            fStr.Close();
        }

        public static Player Load()
        {
            if (File.Exists(Application.persistentDataPath + "/SaveGame.klm"))
            {
                BinaryFormatter binForm = new BinaryFormatter();
                FileStream fStr = File.Open(Application.persistentDataPath + "/SaveGame.klm", FileMode.Open);
                Player outPlayer = (Player)binForm.Deserialize(fStr);
                fStr.Close();
                return outPlayer;
            }
            else
                return null;
        }
    }
}