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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for AllocateSubjects.xaml
    /// </summary>

    public class AllocationSubject : Subject
    {
        public CheckBox checkBox { get; set; }
        public TextBox textBox { get; set; }
    }
    public partial class AllocateSubjects : Window
    {
        private string currentYear { get; set; }
        private string currentDegree { get; set; }
        private string currentBranch { get; set; }
        private string currentSem { get; set; }

        public AllocateSubjects(string currentYear, string currentDegree, string currentBranch, string currentSem, TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.currentDegree = currentDegree;
            this.currentBranch = currentBranch;
            this.currentSem = currentSem;
            fillListView();
        }
        private void fillListView()
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

                int studentCount = 0;
                bool elective = false;
                while(mySqlDataReader.Read())
                {
                    AllocationSubject allocationSubject = new AllocationSubject();
                    allocationSubject.SubjectCode = mySqlDataReader.GetString(0);
                    allocationSubject.SubjectName = mySqlDataReader.GetString(1);
                    allocationSubject.SubjectType = mySqlDataReader.GetString(2);
                    allocationSubject.Rank = uint.Parse(mySqlDataReader.GetString(4));
                    allocationSubject.checkBox = new CheckBox();
                    allocationSubject.textBox = new TextBox();

                    if (allocationSubject.SubjectType == "Elective Course")
                    {
                        elective = true;
                    }
                    else
                    {
                        allocationSubject.checkBox.IsChecked = true;
                        allocationSubject.checkBox.IsEnabled = false;
                        allocationSubject.textBox.IsEnabled = false;
                    }
                    listView.Items.Add(allocationSubject);
                }
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }
    }
}
