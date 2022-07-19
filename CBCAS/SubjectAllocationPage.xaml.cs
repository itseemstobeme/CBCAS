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
        public string SubjectType { get; set; }
        public uint Rank { get; set; }
    }

    public partial class SubjectAllocationPage : Page
    {
        private string currentYear { get; set; }
        private string currentDegree { get; set; }
        private string currentBranch { get; set; }
        private string currentSem { get; set; }
        private List<Subject> subjects { get; set; }
        private Dictionary<string, Subject> map { get; set; }
        private TeacherWindow mainTeacherWindow { get; set; }
        private Button floatButton { get; set; }

        public SubjectAllocationPage(string currentYear, string currentDegree, string currentBranch, string currentSem, TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.currentDegree = currentDegree;
            this.currentBranch = currentBranch;
            this.currentSem = currentSem;
            this.mainTeacherWindow = mainTeacherWindow;
            floatButton = null;
            subjects = new List<Subject>();
            map = new Dictionary<string, Subject>();
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
                string cmdString = "SELECT DISTINCT ALLOCATIONYN FROM " + tableName;
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (!mySqlDataReader.HasRows) //if subjects haven't been floated
                {
                    AllocationStatus.Text = "Subjects not added yet";
                    Button button = new Button()
                    {
                        Content = "Click here to add a few subjects",
                        Tag = "AddSubjects",
                        Style = FindResource("myButtonStyle") as Style,
                        Width = 400
                    };
                    button.Click += new RoutedEventHandler(AddNewSubjectsButton_Click);
                    UfGrid.Children.Add(button);
                }
                else //if subjects have been floated or allocated
                {
                    string YN = "";
                    while (mySqlDataReader.Read())
                    {
                        YN = mySqlDataReader.GetString(0);
                    }

                    if (YN == "Y")   //If subjects have been allocated
                    {
                        AllocationStatus.Text = "Subjects have been allocated for this semester";
                        Button button = new Button()
                        {
                            Content = "Click here to view allocated subjects",
                            Tag = "ViewSubjects",
                            Style = FindResource("myButtonStyle") as Style,
                            Width = 450
                        };
                        button.Click += new RoutedEventHandler(ShowAllocatedSubjects_Click);
                        UfGrid.Children.Add(button);
                    }
                    else             //If subjects have been floated but not allocated
                    {
                        AllocationStatus.Text = "Subjects floated but not allocated";
                        Button button = new Button()
                        {
                            Content = "Click here to allocate subjects",
                            Tag = "AllocateSubjects",
                            Style = FindResource("myButtonStyle") as Style,
                            Width = 450
                        };
                        button.Click += new RoutedEventHandler(AllocateSubjects_Click);
                        UfGrid.Children.Add(button);
                    }
                }
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }

        //View or View + Add new subjects
        private void AddNewSubjectsButton_Click(object sender, RoutedEventArgs e)
        {

            List<Subject> list = subjects;
            Dictionary<string, Subject> subjectsDictionary = map;
            AddSubjectWindow addSubjectWindow = new AddSubjectWindow(ref list, ref subjectsDictionary);
            addSubjectWindow.ShowDialog();
            subjects = addSubjectWindow.commitedSubjects;

            //If subjects have been added
            if (subjects.Count > 0)
            {
                AllocationStatus.Text = "Some subjects have been added";
                (sender as Button).Content = "Click here to view/add more subjects";
                (sender as Button).Width = 450;

                if (floatButton == null)
                {
                    Button button = new Button()
                    {
                        Content = "Click here to float the added subjects",
                        Tag = "FloatSubjects",
                        Style = FindResource("myButtonStyle") as Style,
                        Width = 450
                    };
                    button.Click += new RoutedEventHandler(FloatAddedSubjectsButton_Click);
                    UfGrid.Children.Add(button);
                    floatButton = button;
                }
            }
            else //If no subjects have been added OR all subjects have been deleted
            {
                AllocationStatus.Text = "Subjects not added yet";
                (sender as Button).Content = "Click here to add a few subjects";
                (sender as Button).Width = 400;
                UfGrid.Children.Remove(floatButton);
                floatButton = null;
            }
        }

        //To show allocated subjects
        private void ShowAllocatedSubjects_Click(object sender, RoutedEventArgs e)
        {
            string tableName = currentYear + currentDegree + currentBranch + currentSem;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT *  FROM " + tableName + " ORDER BY SubjectType ";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    Subject subject = new Subject();
                    subject.SubjectCode = mySqlDataReader.GetString(0);
                    subject.SubjectName = mySqlDataReader.GetString(1);
                    subject.SubjectType = mySqlDataReader.GetString(2);
                    subjects.Add(subject);
                }

                cmd.Dispose();
                mySqlConnection.Close();
                List<Subject> list = subjects;
                AddSubjectWindow addSubjectWindow = new AddSubjectWindow(ref list);
                addSubjectWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }

        //Allocate subjects
        private void AllocateSubjects_Click(object sender,RoutedEventArgs e)
        {
            AllocateSubjects allocateSubjects = new AllocateSubjects(currentYear, currentDegree, currentBranch, currentSem, mainTeacherWindow);
            allocateSubjects.ShowDialog();
        }

        //Float subjects
        private void FloatAddedSubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            string tableName = currentYear + currentDegree + currentBranch + currentSem;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {

                for (int i = 0; i < subjects.Count; ++i)
                {
                    mySqlConnection.Open();
                    string cmdString = "INSERT INTO " + tableName + " VALUES(@SubjectCode,@SubjectName,@SubjectType,@YN,@R,@RankCount)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);

                    cmd.Parameters.AddWithValue("@SubjectCode", subjects[i].SubjectCode);
                    cmd.Parameters.AddWithValue("@SubjectName", subjects[i].SubjectName);
                    cmd.Parameters.AddWithValue("@SubjectType", subjects[i].SubjectType);
                    cmd.Parameters.AddWithValue("@YN", 'N');
                    cmd.Parameters.AddWithValue("@R", 0);
                    cmd.Parameters.AddWithValue("@RankCount", 0);

                    cmd.Prepare();
                    //MessageBox.Show(cmd.CommandText.ToString());

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    mySqlConnection.Close();
                }
                MessageBox.Show("Subjects floated\nReturning to main window");
                TeacherWindow newTeacherWindow = new TeacherWindow();
                newTeacherWindow.ShowDialog();
                mainTeacherWindow.Close();

            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }
    }
}
