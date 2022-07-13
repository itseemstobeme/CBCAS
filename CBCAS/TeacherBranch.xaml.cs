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
    public partial class TeacherBranch : Page
    {   
        private string currentYear { get; set; }
        private string currentDegree { get; set; }
        private TeacherWindow mainTeacherWindow { get; set; }

        public TeacherBranch(string currentYear,string currentDegree, TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.currentDegree = currentDegree;
            this.mainTeacherWindow = mainTeacherWindow;
            initializeButtons(currentYear, currentDegree);
        }

        //Initializing the year buttons
        private void initializeButtons(string currentYear,string currentDegree)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT DISTINCT BRANCH FROM YEARDEGBRANCHSEM WHERE YEAR = '" +
                    currentYear + "' AND DEGREE = '" + currentDegree +"'" ;
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                List<string> branchList = new List<string>();
                while (mySqlDataReader.Read())
                {
                    branchList.Add(mySqlDataReader.GetString(0));
                }

                for (int i = 0; i < branchList.Count; ++i)
                {
                    Button button = new Button()
                    {
                        Content = branchList[i],
                        Tag = branchList[i],
                        Style = FindResource("myButtonStyle") as Style
                    };
                    button.Click += new RoutedEventHandler(branchButton_Click);
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

        private void branchButton_Click(object sender,RoutedEventArgs e)
        {
            mainTeacherWindow.Content = new TeacherSemester(currentYear,currentDegree,(string)(sender as Button).Tag,mainTeacherWindow);
        }
    }
}
