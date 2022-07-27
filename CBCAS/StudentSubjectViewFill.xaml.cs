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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for StudentSubjectViewFill.xaml
    /// </summary>
    public partial class StudentSubjectViewFill : Page
    {
        private string semester { get; set; }
        private StudentWindow mainStudentWindow { get; set; }
        private List<Subject> subjects { get; set; }
        private Dictionary<string, Subject> subjectMap { get; set; }
        public StudentSubjectViewFill(string semester, StudentWindow mainStudentWindow)
        {
            InitializeComponent();
            this.semester = semester;
            this.mainStudentWindow = mainStudentWindow;
            StudentNameLabel.Content += Student.StudentName;
            StudentIDLabel.Content += Student.StudentID;
            subjects = new List<Subject>();
            subjectMap = new Dictionary<string, Subject>();
            fillPage();
        }

        private void fillPage()
        {
            if (Student.PreferenceSem.ToString() == semester)
            {
                AllocationStatus.Text = "Preference filled, please wait for allocation by teacher";
                AllocationStatus.FontSize -= 4;
                return;
            }

            string tableName = Student.Year + Student.Degree + Student.Branch + semester;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM " + tableName;
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                bool electiveSwitch = false;
                if (mySqlDataReader.HasRows)
                {
                    string YN = "";
                    int rankCount = 0;
                    while (mySqlDataReader.Read())
                    {
                        Subject subject = new Subject();
                        subject.SubjectCode = mySqlDataReader.GetString(0);
                        subject.SubjectName = mySqlDataReader.GetString(1);
                        subject.SubjectType = mySqlDataReader.GetString(2);
                        if (subject.SubjectType == "Elective Course")
                            electiveSwitch = true;
                        YN = mySqlDataReader.GetString(3);
                        subject.Rank = uint.Parse(mySqlDataReader.GetString(4));
                        rankCount = int.Parse(mySqlDataReader.GetString(5));
                        subjects.Add(subject);
                        subjectMap.Add(subject.SubjectCode, subject);
                    }

                    if (YN == "Y")
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
                    else
                    {
                        if (electiveSwitch == false)
                        {
                            AllocationStatus.Text = "Only core subjects have been floated, please wait for allocation";
                            AllocationStatus.FontSize -= 7;
                            return;
                        }
                        else
                        {
                            AllocationStatus.Text = "Please fill your preference for SEM " + RomanNumeral.ToRoman(int.Parse(semester));
                            Button button = new Button()
                            {
                                Content = "Click here to fill your preference",
                                Tag = "Subjects",
                                Style = FindResource("myButtonStyle") as Style,
                                Width = 450
                            };
                            button.Click += new RoutedEventHandler(FillPreference_Click);
                            UfGrid.Children.Add(button);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
            }
        }


        private void ShowAllocatedSubjects_Click(object sender, RoutedEventArgs e)
        {
            List<Subject> allocatedSubjects = new List<Subject>();

            string tableName = Student.Year + Student.Degree + Student.Branch + semester;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT SubjectCode FROM Final" + tableName +
                    " WHERE StudentID = '" + Student.StudentID + "'";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    allocatedSubjects.Add(subjectMap[mySqlDataReader.GetString(0)]);
                }

                AddSubjectWindow addSubjectWindow = new AddSubjectWindow(ref allocatedSubjects);
                addSubjectWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
            }
        }

        private void FillPreference_Click(object sender, RoutedEventArgs e)
        {
            StudentPreference studentPreference = new StudentPreference(semester, mainStudentWindow);
            studentPreference.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Student.resetStudent();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            mainStudentWindow.Close();
        }
    }
}
