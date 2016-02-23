using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Kalimat
{
    class Data_Server
    {
        static string ScriptURL_Login = "http://www.tanjera.com/kalimat_scripts/login.php";
        static string ScriptURL_Query = "http://www.tanjera.com/kalimat_scripts/query.php";
        static string ScriptURL_Transact = "http://www.tanjera.com/kalimat_scripts/transact.php";

        int Generic_PHPRequest(string reqURL)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            int Result = int.Parse(sr.ReadToEnd());

            sr.Close();
            myResponse.Close();

            return Result;
        }

        public bool Player_Login(string Username, string Password)
        {
            string reqURL = String.Format("{0}?User={1}&Pass={2}&Act=Login", ScriptURL_Login, Username, Password);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string reqResponse = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            switch (reqResponse)
            {
                default:
                case "invalid":
                    return false;

                case "verified":
                    return true;
            }
        }

        public bool Player_Register(string Username, string Password)
        {
            string reqURL = String.Format("{0}?User={1}&Pass={2}&Act=Register", ScriptURL_Login, Username, Password);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string reqResponse = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            switch (reqResponse)
            {
                default:
                case "username_taken":
                    return false;

                case "registration_complete":
                    return true;
            }
        }

        public List<string> Languages_Get()
        {
            string reqURL = String.Format("{0}?Query=Languages", ScriptURL_Query);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            List<string> outList = new List<string>();
            string eachLine;
            while (!sr.EndOfStream)
            {
                eachLine = sr.ReadLine();
                if (eachLine.Trim() != String.Empty)
                    if (!outList.Contains(eachLine))
                        outList.Add(eachLine);
            }

            sr.Close();
            myResponse.Close();

            return outList;
        }

        public List<Stack> Stack_GetList(string incLanguage)
        {
            string reqURL = String.Format("{0}?Query=Stacklist&Language={1}", ScriptURL_Query, incLanguage);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            List<Stack> outList = new List<Stack>();
            while (!sr.EndOfStream)
            {
                Stack eachStack = new Stack();
                eachStack.UID = sr.ReadLine();
                eachStack.Title = sr.ReadLine();
                eachStack.Description = sr.ReadLine();
                eachStack.Price_Points = int.Parse(sr.ReadLine());
                eachStack.Price_Dollars = double.Parse(sr.ReadLine());
                outList.Add(eachStack);
            }

            sr.Close();
            myResponse.Close();

            return outList;
        }

        public Stack Stack_Get(string incUID)
        {
            string reqURL = String.Format("{0}?Query=Stack&UID={1}", ScriptURL_Query, incUID);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            Data instData = new Data();
            Stack outStack = instData.StackFromXML(sr.ReadToEnd());

            sr.Close();
            myResponse.Close();

            return outStack;
        }

        public Player Player_Get(string incUser)
        {
            Player outPlayer = new Player();

            string reqURL = String.Format("{0}?Query=Player&Username={1}", ScriptURL_Query, incUser);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(reqURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

            outPlayer.Username = sr.ReadLine().Trim();
            outPlayer.Points = int.Parse(sr.ReadLine().Trim());
            outPlayer.Timestamp = DateTime.FromBinary(Int64.Parse(sr.ReadLine().Trim()));

            sr.Close();
            myResponse.Close();

            return outPlayer;
        }
        public bool Player_Update(Player incPlayer)
        {
            string reqURL = String.Format("{0}?Query=Player_Update&Username={1}&Points={2}&Timestamp={3}", ScriptURL_Query, incPlayer.Username, incPlayer.Points, incPlayer.Timestamp.ToBinary());
            return Generic_PHPRequest(reqURL) > 0;
        }
        public int Player_Points_Get(string incUser)
        {
            string reqURL = String.Format("{0}?Query=Player_Points&Username={1}", ScriptURL_Query, incUser);
            return Generic_PHPRequest(reqURL);
        }

        public bool Player_Deposit(string incUser, int incPoints)
        {
            string reqURL = String.Format("{0}?Username={1}&Transaction=Deposit&Points={2}&Timestamp={3}", ScriptURL_Transact, incUser, incPoints, DateTime.Now.ToBinary());
            return Generic_PHPRequest(reqURL) > 0;
        }

        public bool Player_Deposit_Quiz(string incUser, int incPoints, string incStackUID)
        {
            string reqURL = String.Format("{0}?Username={1}&Transaction=Deposit_Quiz&Points={2}&Item={3}&Timestamp={4}", ScriptURL_Transact, incUser, incPoints, incStackUID, DateTime.Now.ToBinary());
            return Generic_PHPRequest(reqURL) > 0;
        }

        public bool Player_Purchase_Points(string incUser, int incPoints, string incItem)
        {
            string reqURL = String.Format("{0}?Username={1}&Transaction=Purchase_Points&Points={2}&Item={3}&Timestamp={4}", ScriptURL_Transact, incUser, incPoints, incItem, DateTime.Now.ToBinary());
            return Generic_PHPRequest(reqURL) > 0;
        }
    }
}
