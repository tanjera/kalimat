using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace StackXML
{
    public partial class Main : Form
    {
        enum Languages
        {
            Arabic,
            French,
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

        private void Insert(object sender, EventArgs e)
        {
            string dbHost = "kalimat.tanjera.com";
            string dbBase = "tanjera_kalimat";
            string dbTable = "stacks";

             MySqlConnection mysqlConnect= new MySqlConnection();
            mysqlConnect.ConnectionString = String.Format(
                "Server={0};Database={1};Uid={2};Pwd={3};SslMode=Preferred;CharSet=utf8;",
                dbHost, dbBase, txtUser.Text, txtPassword.Text);

            MySqlCommand mysqlCommand = new MySqlCommand();
            mysqlCommand.Connection = mysqlConnect;
            mysqlCommand.CommandType = CommandType.Text;
            mysqlCommand.CommandText =
            String.Format(@"INSERT into {0} (uid, title, description, price_points, price_dollars, language, xml)
                VALUES (@uid, @title, @description, @price_points, @price_dollars, @language, @xml)", dbTable);
            mysqlCommand.Parameters.AddWithValue("@uid", txtUID.Text);
            mysqlCommand.Parameters.AddWithValue("@title", txtTitle.Text);
            mysqlCommand.Parameters.AddWithValue("@description", txtDesc.Text);
            mysqlCommand.Parameters.AddWithValue("@price_points", numPoints.Value);
            mysqlCommand.Parameters.AddWithValue("@price_dollars", numPoints.Value);
            mysqlCommand.Parameters.AddWithValue("@language", cmbLanguage.Text);
            mysqlCommand.Parameters.AddWithValue("@xml", txtOutput.Text);

            try
            {
                mysqlConnect.Open();
                int recordsAdded = mysqlCommand.ExecuteNonQuery();
                MessageBox.Show(String.Format("Connection successful. {0} records added!", recordsAdded));
            }
            catch (MySqlException except)
            {
                MessageBox.Show(except.Message);
            }
            finally
            {
                mysqlConnect.Close();
            }
        }

        private void LowerCase(object sender, EventArgs e)
        {
            txtWordPairs.Text = txtWordPairs.Text.ToLower();
        }
    }
}
