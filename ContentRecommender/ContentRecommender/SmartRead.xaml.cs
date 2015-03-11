using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SmartRead.xaml
    /// </summary>
    public partial class SmartRead : Window
    {
        static String opConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data"
                         + @" Source= C:\BE project\User1.accdb";
        OleDbConnection opconn = new OleDbConnection(opConnStr);
        OleDbCommand cmd = null;
        OleDbDataReader dr = null;
        OleDbDataReader r = null;
        OleDbDataReader n = null;

        //to give pdfViewer filepath
        PDFViewerUserControl uc = null;

        Int32[] plot_x = new Int32[100000];
        Int32[] plot_y = new Int32[100000];
        String ud;
        String ad;

        public SmartRead(String path,String userDetails,String articleId)
        {
            InitializeComponent();
            ud = userDetails;
            this.Show();
            PDFViewer(path);
            ad = articleId;
        }
        public void PDFViewer(String s)
        {
            uc = new PDFViewerUserControl(@s);
            this.pdfViewWindow.Child = uc;
            // this.Show();
           // MessageBox.Show("reader");

            int milliseconds = 2000;
            Thread.Sleep(milliseconds);

            recomend();
        }

        public void recomend()
        {
            EyeDetect ed = new EyeDetect();
            //MessageBox.Show("recommender");
            //StreamReader file = new StreamReader(@"C:\Users\Zeal\Desktop\B.E. Project\plot.txt");
          //  string line = "";
           Int16 i = 0;
            String s,s1;
             Int16 mind = 0;
             while (true)
             {
                 i++;
                 s = ed.Eye();
                 //MessageBox.Show(s1);
                
                 //s = s1.Substring(1, s1.Length);
                 //MessageBox.Show(s);
              
              
              string[] a = s.Split(' ');
              int x = Convert.ToInt32(a[0].Substring(3));
              //MessageBox.Show(x.ToString());
              //MessageBox.Show(a[1].Substring(2, a[1].LastIndexOf(')'));
             // MessageBox.Show(a[1]);
              string[] b = a[1].Split(')');
                 int y = Convert.ToInt32(b[0].Substring(2));
                
              //MessageBox.Show(x + " " + y);
             
             if (x!=0 && y!=0)
                {
                    plot_x[i] = x;
                    plot_y[i] = y;
                }
                // MessageBox.Show(x[i] + " " + x[i - 1]);
                if (plot_x[i - 1] - plot_x[i] > 100 || plot_y[i - 1] - plot_y[i] > 100)
                {
                    mind++;
                    // MessageBox.Show("mind wandering for" + i);
                }

                if (mind > 2)
                {
                    MessageBoxResult result1 = MessageBox.Show("Your mind seems to be wndering; would you like alternate reading content?",
        "Important Question",
        MessageBoxButton.YesNo);
                    if (result1 == MessageBoxResult.Yes)
                        recommendation();
                    else
                        MessageBox.Show("No");
                    break;
                } 

                // MessageBox.Show("break not executed");
            }


        }

        public void recommendation()
        {
            opconn.Open();
            cmd=new OleDbCommand("SELECT Specialization FROM Users WHERE UId= "+ud+";",opconn);
             dr = cmd.ExecuteReader();
             dr.Read();
            String sp = dr[0].ToString();
            MessageBox.Show(sp);

            cmd=new OleDbCommand("SELECT * FROM Users WHERE Specialization='"+sp+"'AND UId <>"+ud+";",opconn);
            dr = cmd.ExecuteReader();
            //MessageBox.Show(dr.ToString());
            //dr.Read();
            String display="";        

           while(dr.Read())
           {
               cmd = new OleDbCommand("SELECT AId FROM ReadBy WHERE Rating='5' AND UID='" + dr[0].ToString() + "';", opconn);
               r = cmd.ExecuteReader();
               while (r.Read())
               {
                 //  display = display +"\t"+ r[0].ToString();
                   cmd = new OleDbCommand("SELECT AName from Article WHERE AId='" + r[0].ToString() + "';", opconn);
                   n = cmd.ExecuteReader();
                   n.Read();
                   display = display + "\t" + n[0].ToString();
               }
           }
          // MessageBox.Show(display);
        
        }

       
        
        
        
        
        private void R4_Click(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("UPDATE ReadBy SET Rating ='4' WHERE UId='"+ud+"' AND AId='"+ad+"';", opconn);
            cmd.ExecuteNonQuery();
            opconn.Close();
        }

        private void R1_Click(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("UPDATE ReadBy SET Rating ='1' WHERE UId='" + ud + "' AND AId='" + ad + "';", opconn);
            cmd.ExecuteNonQuery();
            opconn.Close();
        }

        private void R2_Click(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("UPDATE ReadBy SET Rating ='2' WHERE UId='" + ud + "' AND AId='" + ad + "';", opconn);
            cmd.ExecuteNonQuery();
            opconn.Close();
        }

        private void R3_Click(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("UPDATE ReadBy SET Rating ='3' WHERE UId='" + ud + "' AND AId='" + ad + "';", opconn);
            cmd.ExecuteNonQuery();
            opconn.Close();
        }

        private void R5_Click(object sender, RoutedEventArgs e)
        {
            opconn.Open();
            cmd = new OleDbCommand("UPDATE ReadBy SET Rating ='5' WHERE UId='" + ud + "' AND AId='" + ad + "';", opconn);
            cmd.ExecuteNonQuery();
            opconn.Close();
        }

      }
}
