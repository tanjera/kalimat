using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Kalimat
{
    class Data
    {
        public Stack StackFromXML(string incXML)
        {
            Stack incStack = new Stack();
            string buf = "";

            if (incXML == String.Empty)
                return null;

            using (XmlReader xr = XmlReader.Create(new StringReader(incXML)))
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

                                case "uid":
                                    incStack.UID = xr.ReadElementContentAsString();
                                    break;

                                case "title":
                                    incStack.Title = xr.ReadElementContentAsString();
                                    break;

                                case "description":
                                    incStack.Description = xr.ReadElementContentAsString();
                                    break;

                                case "source_description":
                                    incStack.SourceDescription = xr.ReadElementContentAsString();
                                    break;

                                case "language":
                                    incStack.Language = (Languages)Enum.Parse(typeof(Languages), xr.ReadElementContentAsString());
                                    break;

                                case "price_points":
                                    incStack.Price_Points = xr.ReadElementContentAsInt();
                                    break;

                                case "price_dollars":
                                    incStack.Price_Dollars = xr.ReadElementContentAsDouble();
                                    break;

                                case "word_pair":
                                    buf = "";
                                    break;

                                case "word_source":
                                    buf = xr.ReadElementContentAsString();
                                    break;

                                case "word_target":
                                    incStack.WordPairs.Add(new string[] { buf, xr.ReadElementContentAsString() });
                                    break;
                            }
                            break;

                    }
                }
            }

            return incStack;
        }

    }
}
