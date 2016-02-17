using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;

namespace StackXML
{
    public partial class Main : Form
    {
        enum WordPair
        {
            Target,
            Source
        }

        enum Languages
        {
            Arabic,
            Spanish
        }

        public Main()
        {
            InitializeComponent();
            cmbLanguage.Items.AddRange(Enum.GetNames(typeof(Languages)));
        }

        private void Format(object sender, EventArgs e)
        {
            txtOutput.Text = String.Format(
@"<?xml version='1.0' encoding='utf-8'?>
<stack>
    <uid>{0}</uid>
    <title>{1}</title>
    <description>{2}</description>
    <source>{3}</source>
    <sourcedescription>{4}</sourcedescription>
    <language>{5}</language>
    <price_points>{6}</price_points>
    <price_dollars>{7}</price_dollars>\n",
                txtUID.Text, txtTitle.Text, txtDesc.Text, txtSource.Text, txtSourceDesc.Text, cmbLanguage.Text, numPoints.Value.ToString(), numDollars.Value.ToString());

            string eachLine;
            int i = 0;
            StringReader sr = new StringReader(txtWordPairs.Text);
            while ((eachLine = sr.ReadLine()) != null)
            {
                if (i == 0)   // First word
                {
                    txtOutput.AppendText(String.Format(
@"   <word_pair>
        <word_source>{0}</word_source>{1}", eachLine, Environment.NewLine));
                    i++;
                }
                else if (i == 1)   // Second word
                {
                    txtOutput.AppendText(String.Format(
@"        <word_target>{0}</word_target>
    </word_pair>{1}", eachLine, Environment.NewLine));
                    i = 0;
                }
            }

            txtOutput.AppendText("</stack>");
        }
    }
}
