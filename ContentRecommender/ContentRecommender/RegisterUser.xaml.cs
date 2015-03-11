using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace ContentRecommender
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        private static String dbConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data"
                          + @" Source=C:\BE project\User1.accdb";
        OleDbConnection dbConn = new OleDbConnection(dbConnStr);
        OleDbCommand dbcmd = null;
        private Login parentWin = null;
        public RegisterUser(Window callingWin)
        {
            parentWin = callingWin as Login;
            InitializeComponent();
            field.Items.Add("Engineering");
            field.Items.Add("Doctor");
            field.Items.Add("Finance and Business");
            field.Items.Add("Agent");
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            /* + dob.SelectedDate.Value.ToShortDateString()*/ 
            dbConn.Open();
            //add the sql queries and the update label data
            //String s = "INSERT INTO Users(UId,UName,FieldOfWork,Specialization,Username,Password) VALUES('110','"+firstname.Text+" "+lastname.Text+"','Engineering','Computer','"+username.Text+"','"+upassword.Password+"');";
            String m = "INSERT INTO Users (UName,FieldOfWork,Specialization,Username,Pass) VALUES ('" + firstname.Text + "','" + field.Text + "','" + specialisation.Text + "','" + username.Text + "','" + upassword.Password + "');";
           dbcmd = new OleDbCommand(m, dbConn);
           //System.IO.File.WriteAllText(@"F:\Dropbox\Dropbox\SmartRead\temp.txt", dbcmd.CommandText);
           /* dbcmd = new OleDbCommand();
            dbcmd.Connection = dbConn;
            dbcmd.CommandText = "INSERT INTO user(firstname,lastname,Users,pass,dob,fieldofwork,Specialisation,readinginterests) VALUES('Rohan','Gada','zxczxczxc','aaaa',09/09/14,'Engineering','zxcxcjhzxcj','zcxzcszcz')";
             */
            dbcmd.ExecuteNonQuery();
            dbConn.Close(); 
            
            this.parentWin.softwareDesc.Text = "You are successfully registered!\n proceed to login..";
            this.parentWin.register.Content = "<";
            this.parentWin.Show();
            Close();
        }

       private void field_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            specialisation.Items.Clear();
            String g = field.SelectedItem.ToString();
            if (g=="Engineering" )
            {
                specialisation.Items.Add("Computer");
                specialisation.Items.Add("Electronics");
                
            }
            else if (g=="Doctor")
            {
                specialisation.Items.Add("Surgeon");
                specialisation.Items.Add("Veterian");
                specialisation.Items.Add("Physician");
            }
            else if (g=="Finance and Business")
            {
                specialisation.Items.Add("Finance");
                specialisation.Items.Add("Business");
            }
            else if (g=="Agent")
            {
                specialisation.Items.Add("Travel Agent");
                specialisation.Items.Add("Estate Agent");
            }
        }
    }
}
