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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;

namespace ContentRecommender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
      //  OleDbDataReader dr = null;
        private static String dbConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data"
                          + @" Source=C:\BE project\User1.accdb";
        OleDbConnection dbConn = new OleDbConnection(dbConnStr);
        OleDbCommand dbcmd = null;
        public Login()
        {
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            RegisterUser regNew = new RegisterUser(this);
            this.Hide();
            regNew.Show();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            using (dbConn=new OleDbConnection(dbConnStr))
            {
                try
                {
                    dbConn.Open();
                    using (dbcmd = new OleDbCommand("Select * from Users where Username = @Username and Password = @Password",dbConn))
                    {

                        dbcmd.Parameters.AddWithValue("@Username", username.Text);
                        dbcmd.Parameters.AddWithValue("@Password", password.Password);
                        
                        using (OleDbDataReader r = dbcmd.ExecuteReader())
                        {
                            r.Read();
                            if (r.HasRows)
                            {
                                String ud= r[0].ToString();
                                SelectArticle sr = new SelectArticle(ud);
                                sr.Show();
                                this.Close();
                            }
                            else
                            {
                                username.Text = "";
                                password.Password = "";
                                Label.Content = "Entered username or password is invalid";
                            }
                        }
                    }
                   // this.Hide();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
