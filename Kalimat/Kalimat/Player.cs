using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Kalimat
{
    public class Player
    {
        [Column("username")]
        public string Username { get; set; }
        [Column("points")]
        public int Points { get; set; }

        public Player () { }
        public Player (string incUser)
        {
            Username = incUser;
        }
    }
}
