using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Kalimat
{
    [System.Serializable]
    public class Player {
        public string Username;
        public string Hashpass;
        public string Name;
        public int Points;
    }

    public static class Serialize
    {
        public static void Save(Player incPlayer)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            FileStream fStr = File.Create(Application.persistentDataPath + "/SaveGame.klm");
            binForm.Serialize(fStr, incPlayer);
            fStr.Close();
            Debug.Log("Player file saved.");
        }

        public static Player Load()
        {
            if (File.Exists(Application.persistentDataPath + "/SaveGame.klm"))
            {
                BinaryFormatter binForm = new BinaryFormatter();
                FileStream fStr = File.Open(Application.persistentDataPath + "/SaveGame.klm", FileMode.Open);
                Player outPlayer = (Player)binForm.Deserialize(fStr);
                fStr.Close();
                Debug.Log("Saved game exists- loaded.");
                return outPlayer;
            }
            else
            {
                Debug.Log("No saved game found- creating new player.");
                return new Player();
            }
        }
    }
}