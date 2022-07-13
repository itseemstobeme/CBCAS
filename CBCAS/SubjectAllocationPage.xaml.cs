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
    /// Interaction logic for SubjectAllocationPage.xaml
    /// </summary>
    
    public class Subject
    {
        public string SubjectCode { get; set; }

        public string SubjectName { get; set; }
        public uint Ranking { get; set; }   
    }
    public partial class SubjectAllocationPage : Page
    {
        private string currentYear { get; set; }
        private string currentDegree { get; set; }
        private string currentBranch { get; set; }
        private string currentSem { get; set; }
        private TeacherWindow mainTeacherWindow { get; set; }
        private Button b { get; set; }

        public SubjectAllocationPage(string currentYear,string currentDegree,string currentBranch,string currentSem,TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear= currentYear;
            this.currentDegree = currentDegree;
            this.currentBranch = currentBranch;
            this.currentSem = currentSem;
            this.mainTeacherWindow = mainTeacherWindow;
            fillPage();
            
        }

        private void fillPage()
        {
            string tableName = currentYear + currentDegree + currentBranch + currentSem;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM " + tableName;
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                
                if (!mySqlDataReader.HasRows) //if subjects haven't been floated
                {
                    AllocationStatus.Text = "Subjects not yet floated";
                    Button button = new Button()
                    {
                        Content = "Click here to add subjects",
                        Tag = "AddSubjects",
                        Style = FindResource("myButtonStyle") as Style,
                        Width = 400
                    };
                    button.Click += new RoutedEventHandler(AddSubjectsButton_Click);
                    UfGrid.Children.Add(button);



                }
                else //if subjects have been floated or allotted
                {
                    
                }
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }

        private void AddSubjectsButton_Click(object sender,RoutedEventArgs e)
        {
            AddSubjectWindow addSubjectWindow = new AddSubjectWindow();
            addSubjectWindow.ShowDialog();
        }
    }
}
