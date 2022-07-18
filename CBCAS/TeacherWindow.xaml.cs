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
using System.Windows.Navigation;

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary> 
    public class Teacher
    {
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public Teacher()
        {

        }
    }

    public partial class TeacherWindow : Window
    {   
        public TeacherWindow()
        {
            InitializeComponent();
            TeacherNameLabel.Content +="Ms YEETY BOI";
            initializeButtons();
        }

        public TeacherWindow(string TeacherID)
        {
            InitializeComponent();

            Teacher teacher = new Teacher();
            //ADD MYSQL CODE HERE OF GETTING TEACHER INFO
            TeacherNameLabel.Content += teacher.TeacherName;
            initializeButtons();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        
        //Initializing the year buttons
        private void initializeButtons()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT DISTINCT YEAR FROM YEARDEGBRANCHSEM";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                
                
                List<string> yearList = new List<string>();
                while(mySqlDataReader.Read())
                {
                    yearList.Add(mySqlDataReader.GetString(0));
                }


                for(int i = 0;i< yearList.Count;++i)
                {
                    Button button = new Button()
                    { 
                        Content = yearList[i],
                        Tag = yearList[i],
                        /*
                         Background = Brushes.Transparent,
                         Margin  = new Thickness(10,10,10,10),
                         Height  = 70,
                         Width = 150.5,
                         BorderThickness = new Thickness(0.5),
                         FontSize = 25,*/
                        Style = FindResource("myButtonStyle") as Style
                    };
                    button.Click += new RoutedEventHandler(yearButton_click);
                    wp.Children.Add(button); 
                    
                }
                cmd.Dispose();
                mySqlConnection.Close();

            }
            catch
            {
                MessageBox.Show("Some Error Occured");
            }
        }


        private void yearButton_click(object sender,RoutedEventArgs e)
        {
            //MessageBox.Show((sender as Button).Name);
            //frame.Content = new TeacherBranch();
            
            this.Content = new TeacherDegree((string)(sender as Button).Tag,this);
        }
    }
}
