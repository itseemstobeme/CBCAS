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
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Navigation;


namespace CBCAS
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>

    public class Student
    {
        public static string StudentID = "";
        public static string StudentName = "";
        public static float StudentCGPA = 0;
        public static string Year = "";
        public static string Degree = "";
        public static string Branch = "";
        public static string Preference = null;
        public static uint PreferenceSem = 0;
        public static void setStudent(string SID)
        {
            StudentID = SID;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string cmdString = "SELECT * FROM STUDENT WHERE STUDENTID = '" + StudentID + "'";
            MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {

                StudentName = mySqlDataReader.GetString(1);

                if (!mySqlDataReader.IsDBNull(2))
                    StudentCGPA = mySqlDataReader.GetFloat(2);
                Year = mySqlDataReader.GetString(3);
                Degree = mySqlDataReader.GetString(4);
                Branch = mySqlDataReader.GetString(5);

                if (!mySqlDataReader.IsDBNull(6))
                    Preference = mySqlDataReader.GetString(6);
                if (!mySqlDataReader.IsDBNull(7))
                    PreferenceSem = mySqlDataReader.GetUInt32(7);

            }
        }
        public static void resetStudent()
        {
            StudentID = "";
            StudentName = "";
            StudentCGPA = 0;
            Year = "";
            Degree = "";
            Branch = "";
            Preference = null;
            PreferenceSem = 0;
        }
    }
    public partial class StudentWindow : Window
    {
        public StudentWindow(string StudentID)
        {
            InitializeComponent();
            Student.setStudent(StudentID);
            HomeButton.IsEnabled = false;
            HomeButton.Foreground = Brushes.Gray;
            StudentNameLabel.Content += Student.StudentName;
            StudentIDLabel.Content += Student.StudentID;
            fillPage();
        }

        private void fillPage()
        {
            string tableName = Student.Year + Student.Degree + Student.Branch;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM (SELECT TABLE_NAME AS tables FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'testing') AS TABLES WHERE TABLES LIKE '" + tableName + "%'";   //finding tables eg. 2019btechbt* where * = sem(1,2,3..)
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (mySqlDataReader.HasRows)
                {
                    SemesterStatus.Text = "Select Semester :";
                    int semCount = 0;
                    while (mySqlDataReader.Read())
                    {
                        ++semCount;
                    }

                    for (int i = 0; i < semCount; ++i)
                    {
                        Button button = new Button()
                        {
                            Content = RomanNumeral.ToRoman(i + 1),
                            Tag = (i + 1).ToString(),
                            Style = FindResource("myButtonStyle") as Style
                        };
                        button.Click += new RoutedEventHandler(semButton_click);
                        wp.Children.Add(button);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
            }
        }

        //Semester button click
        private void semButton_click(object sender, RoutedEventArgs e)
        {
            StudentSubjectViewFill studentSubjectViewFill = new StudentSubjectViewFill((sender as Button).Tag.ToString(), this);
            this.Content = studentSubjectViewFill;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Student.resetStudent();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
