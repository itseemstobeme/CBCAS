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
using System.Configuration;
using MySql.Data.MySqlClient;


namespace CBCAS
{
    /// <summary>
    /// Interaction logic for TeacherBranch.xaml
    /// </summary>
    public partial class TeacherDegree : Page
    {   
        private string currentYear { get; set; }
        private TeacherWindow teacherMainWindow { get; set; }

        public TeacherDegree(string currentYear,TeacherWindow teacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.teacherMainWindow = teacherWindow;
            initializeButtons(currentYear);
        }

        //Initializing the year buttons
        private void initializeButtons(string currentYear)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT DISTINCT DEGREE FROM YEARDEGBRANCHSEM WHERE YEAR = '" + 
                    currentYear + "'";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                List<string> degreeList = new List<string>();
                while (mySqlDataReader.Read())
                {
                    degreeList.Add(mySqlDataReader.GetString(0));
                }

                for (int i = 0; i < degreeList.Count; ++i)
                {
                    Button button = new Button()
                    {
                        Content = degreeList[i],
                        Tag = degreeList[i],
                        Style = FindResource("myButtonStyle") as Style
                    };
                   button.Click += new RoutedEventHandler(degreeButton_Click);
                    UfGrid.Children.Add(button);
                }
                cmd.Dispose();
                mySqlConnection.Close();
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
            }
        }

        private void degreeButton_Click(object sender, RoutedEventArgs e)
        {
            teacherMainWindow.Content = new TeacherBranch(currentYear,(string)(sender as Button).Tag,teacherMainWindow);
        }
    }
}
