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
                    TeacherWindow teacherWindow = new TeacherWindow();
                    teacherWindow.Show();
                    cmd.Dispose();
                    mySqlConnection.Close();

                    this.Close();
                }    
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
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
                    StudentWindow studentWindow = new StudentWindow();
                    studentWindow.Show();
                    cmd.Dispose();
                    mySqlConnection.Close();

                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Some Error Occured");
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
    }
}
