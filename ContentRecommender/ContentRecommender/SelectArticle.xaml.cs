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
using System.Data.OleDb;

namespace ContentRecommender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SelectArticle : Window
    {
        static String opConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data"
                          + @" Source=C:\BE project\User1.accdb";
        OleDbConnection opconn = new OleDbConnection(opConnStr);
        OleDbCommand cmd = null;
        OleDbDataReader dr = null;
        String ud;
        List<string> articleName = new List<string>();
        public SelectArticle(String userDetails)
        {
            InitializeComponent();
            ud = userDetails;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            combobox1.IsEnabled = true;
            opconn.Open();
            cmd = new OleDbCommand("Select AName from Article", opconn);
            dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                
                combobox1.Items.Add(dr[0].ToString());
                //MessageBox.Show(dr[0].ToString());
              
            }
            dr.Close();
            opconn.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("select AId,AName,APath from Article where AName='" + combobox1.Text + "'", opconn);
            dr = cmd.ExecuteReader();
            dr.Read();
           // MessageBox.Show(dr[0].ToString() + " " + dr[2].ToString());//path
            String aid=dr[0].ToString();
            String p = dr[2].ToString();
            //MessageBox.Show("INSERT INTO Read ([UId],[AId],[Rating]) VALUES ('101','" + dr[0].ToString() + "','4');");
            cmd = new OleDbCommand("INSERT INTO ReadBy (UId,AId,Rating) VALUES ('"+ud+"','" + dr[0].ToString() + "','')", opconn);
            cmd.ExecuteNonQuery();
            dr.Close();
            opconn.Close();
            SmartRead sm = new SmartRead(p,ud,aid);
            this.Hide();
        }
    }
}
