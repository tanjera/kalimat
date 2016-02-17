using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using SQLite;

namespace Kalimat
{
    public enum WordPair
    {
        Target,
        Source
    }

    public class Stack
    {
        [Column("uid")]
        public string UID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("source")]
        public string Source { get; set; }
        [Column("sourcedescription")]
        public string SourceDescription { get; set; }
        [Column("price_points")]
        public int Price_Points { get; set; }
        [Column("price_dollars")]
        public double Price_Dollars { get; set; }
        [Column("language")]
        public string Language { get; set; }
        [Column("xml")]
        public string XML { get; set; }

        public Stack() { }
        public Stack(string incTitle, string incDescription, string incLanguage)
        {
            Title = incTitle;
            Description = incDescription;
            Language = incLanguage;
        }

        public List<string[]> WordPairs()
        {
            List<string[]> outList = new List<string[]>();
            string buf = "";

            if (XML == String.Empty)
                return null;
            try
            {
                using (XmlReader xr = XmlReader.Create(new StringReader(XML)))
                {
                    while (xr.Read())
                    {
                        switch (xr.NodeType)
                        {
                            default: break;

                            case XmlNodeType.Element:
                                switch (xr.Name)
                                {
                                    default: break;

                                    case "word_pair":
                                        buf = "";
                                        break;

                                    case "word_source":
                                        buf = xr.ReadElementContentAsString();
                                        break;

                                    case "word_target":
                                        outList.Add(new string[] { buf, xr.ReadElementContentAsString() });
                                        break;
                                }
                                break;

                        }
                    }
                }
            }
            catch
            { return null; }

            return outList;
        }

        public string[] ListPairs()
        {
            List<string> newPairs = new List<string>();
            foreach (string[] eachPair in WordPairs())
                newPairs.Add(String.Format("{0} : {1}", eachPair[WordPair.Target.GetHashCode()], eachPair[WordPair.Source.GetHashCode()]));

            return newPairs.ToArray();
        }
        public string[] ListTargets()
        {
            List<string> newPairs = new List<string>();
            foreach (string[] eachPair in WordPairs())
                newPairs.Add(eachPair[WordPair.Target.GetHashCode()]);

            return newPairs.ToArray();
        }
    }
}