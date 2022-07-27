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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public static class Teacher
    {
        public static string TeacherID = "";
        public static string TeacherName = "";
        public static void setTeacher(string TID)
        {
            TeacherID = TID;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            string cmdString = "SELECT * FROM TEACHER WHERE TEACHERID = '" + TeacherID + "'";
            MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

            while(mySqlDataReader.Read())
            {
                TeacherName = mySqlDataReader.GetString(1);
            }
        }
    }

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }


        //When valid login credentials are entered, teacher is logged in
        private void TeacherLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM TEACHERLOGIN WHERE TEACHERID = '" + TeacherID.Text +
                    "' AND PASSWORD = '" + TeacherPassword.Password + "'";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.HasRows)
                {
                    Teacher.setTeacher(TeacherID.Text);
                    TeacherWindow teacherWindow = new TeacherWindow();
                    teacherWindow.Show();
                    cmd.Dispose();
                    mySqlConnection.Close();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect login credentials");
                }
            }
            catch
            {
                MessageBox.Show("Unable to connect to the server\nPlease check your internet connection");
            }
        }

        //When valid login credentials are entered, student is logged in
        private void StudentLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM STUDENTLOGIN WHERE STUDENTID = '" + StudentID.Text +
                    "' AND PASSWORD = '" + StudentPassword.Password + "'";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.HasRows)
                {   
                    StudentWindow studentWindow = new StudentWindow(StudentID.Text);
                    studentWindow.Show();
                    cmd.Dispose();
                    mySqlConnection.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect login credentials");
                }
            }
            catch
            {
                MessageBox.Show("Unable to connect to the server\nPlease check your internet connection");
            }
        }

        private void WINDOWEXIT(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //For pressing enter after entering password
        private void TeacherPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
                return;
            TeacherLoginButton_Click(sender, e);
        }

        //For pressing enter after entering password
        private void StudentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
                return;
            StudentLoginButton_Click(sender, e);
        }

        private void tabItem_Selected(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(tabItem.SelectedIndex.ToString());
            TabItem item = tabItem.SelectedItem as TabItem;
            if(item.Name == "TeacherLogin")
            {
                Grad2.Color = Brushes.CornflowerBlue.Color;
                Grad3.Color = Brushes.LightSkyBlue.Color;
                Grad2.Offset = 3;
                Grad3.Offset = 2;

            }
            else
            {
                Grad2.Color = Brushes.CornflowerBlue.Color;
                BrushConverter brushConverter = new BrushConverter();
                Grad3.Color = (Color)ColorConverter.ConvertFromString("#97F8EC");
                Grad2.Offset = 3;
                Grad3.Offset = 2;
            }
        }
    }
}
