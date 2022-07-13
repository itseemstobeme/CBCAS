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
    /// Interaction logic for TeacherSemester.xaml
    /// </summary>
    public class RomanNumeral
    {   
        //Optimize function by DP later on
        public string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
    public partial class TeacherSemester : Page
    {
        private string currentYear { get; set; }
        private string currentDegree { get; set; }
        private string currentBranch { get; set; }
        private TeacherWindow mainTeacherWindow { get; set; }
        public TeacherSemester(string currentYear,string currentDegree,string currentBranch,TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.currentDegree = currentDegree;
            this.currentBranch = currentBranch;
            this.mainTeacherWindow = mainTeacherWindow;
            initializeButtons(currentYear, currentDegree,currentBranch);
        }

        //Initializing the year buttons
        private void initializeButtons(string currentYear, string currentDegree,string currentBranch)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT SEM FROM YEARDEGBRANCHSEM WHERE YEAR = '" +
                    currentYear + "' AND DEGREE = '" + currentDegree + "' AND BRANCH ='" +
                    currentBranch +"' ORDER BY SEM";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                List<string> semList = new List<string>();
                while (mySqlDataReader.Read())
                {
                    semList.Add(mySqlDataReader.GetString(0));
                }

                RomanNumeral romanNumeral = new RomanNumeral();
                for (int i = 0; i < semList.Count; ++i)
                {
                    Button button = new Button()
                    {
                        Content = "SEM " + romanNumeral.ToRoman(int.Parse(semList[i])),
                        Tag = semList[i],
                        Style = FindResource("myButtonStyle") as Style
                    };
                    button.Click += new RoutedEventHandler(semesterButton_Click);
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

        private void semesterButton_Click(object sender,RoutedEventArgs e)
        {
            mainTeacherWindow.Content = new SubjectAllocationPage(currentYear, currentDegree, currentBranch, (string)(sender as Button).Tag,mainTeacherWindow);
        }
    }
}
