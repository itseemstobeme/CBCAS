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

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for StudentPreference.xaml
    /// </summary>

    //Custon COMBOBOX class with an added previousValue property
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    }

    public class helper
    {
        public string prevVal { get; set; }
        public string SubjectCode { get; set; }
    }
    public class PreferenceSubject : Subject
    {
        public ComboBox comboBox { get; set; }
    }

    public partial class StudentPreference : Window
    {

        private string semester { get; set; }
        private StudentWindow mainStudentWindow { get; set; }

        public StudentPreference(string semester, StudentWindow mainStudentWindow)
        {
            InitializeComponent();
            this.semester = semester;
            this.mainStudentWindow = mainStudentWindow;
            fillListView();
        }
        private void fillListView()
        {
            string tableName = Student.Year + Student.Degree + Student.Branch + semester;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT * FROM " + tableName + " ORDER BY SubjectType,Ranking";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                int prefCount = 0;
                List<string> l = new List<string>();
                l.Add("None");
                while (mySqlDataReader.Read())
                {

                    PreferenceSubject preferenceSubject = new PreferenceSubject();
                    preferenceSubject.SubjectCode = mySqlDataReader.GetString(0);
                    preferenceSubject.SubjectName = mySqlDataReader.GetString(1);
                    preferenceSubject.SubjectType = mySqlDataReader.GetString(2);
                    preferenceSubject.Rank = uint.Parse(mySqlDataReader.GetString(4));
                    preferenceSubject.comboBox = new ComboBox();

                    helper obj = new helper();
                    obj.SubjectCode = preferenceSubject.SubjectCode;
                    if (preferenceSubject.SubjectType == "Elective Course")
                    {
                        preferenceSubject.comboBox.IsEnabled = true;
                        preferenceSubject.comboBox.SelectedValue = (++prefCount).ToString();
                        obj.prevVal = (prefCount).ToString();
                        l.Add(prefCount.ToString());
                    }
                    else
                    {
                        preferenceSubject.comboBox.IsEnabled = false;
                    }
                    preferenceSubject.comboBox.Tag = obj;
                    listView.Items.Add(preferenceSubject);
                    preferenceSubject.comboBox.ItemsSource = l;
                }


            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }

        private void SubmitPreferenceButton_Click(object sender, RoutedEventArgs e)
        {
            string preference = "";
            List<PreferenceSubject> subjects = new List<PreferenceSubject>();
            foreach (PreferenceSubject preferenceSubject in listView.Items)
            {
                if (preferenceSubject.SubjectType == "Elective Course")
                {
                    if (preferenceSubject.comboBox.SelectedValue == "None")
                    {
                        MessageBox.Show("'None' not allowed, fill a valid preference");
                        return;
                    }
                    else
                    {
                        if (preference == "")
                            preference += preferenceSubject.SubjectCode + "+" + preferenceSubject.comboBox.SelectedValue.ToString();
                        else
                            preference += "," + preferenceSubject.SubjectCode + "+" + preferenceSubject.comboBox.SelectedValue.ToString();

                        preferenceSubject.Rank = uint.Parse(preferenceSubject.comboBox.SelectedValue.ToString());
                    }
                }
            }

            dumpPreferenceToDB();

        }

        private void dumpPreferenceToDB()
        {
            string tableName = Student.Year + Student.Degree + Student.Branch + semester;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                string cmdString = "SELECT SUBJECTCODE,RANKING,RANKCOUNT FROM " + tableName;
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                // <SubjectCode,<Ranking,RankCount>>
                Dictionary<string, Pair<uint, uint>> dict = new Dictionary<string, Pair<uint, uint>>();
                while (mySqlDataReader.Read())
                {
                    Pair<uint, uint> pair = new Pair<uint, uint>();
                    pair.First = uint.Parse(mySqlDataReader.GetString(1));
                    pair.Second = uint.Parse(mySqlDataReader.GetString(2));
                    dict[mySqlDataReader.GetString(0)] = pair;
                }
                cmd.Dispose();
                mySqlConnection.Close();


                foreach (PreferenceSubject preferenceSubject in listView.Items)
                {
                    if (preferenceSubject.SubjectType == "Elective Course")
                    {
                        dict[preferenceSubject.SubjectCode].First += preferenceSubject.Rank;
                        dict[preferenceSubject.SubjectCode].Second += 1;
                    }
                }

                foreach(KeyValuePair<string, Pair<uint, uint>> obj in dict)
                {
                    
                    mySqlConnection.Open();
                    cmdString = "UPDATE " + tableName + " SET RANKING = "+
                        obj.Value.First.ToString()+" , RANKCOUNT = " +
                        obj.Value.Second.ToString() + " WHERE SUBJECTCODE = '"+
                        obj.Key+"'";
                   
                    cmd = new MySqlCommand(cmdString, mySqlConnection);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    mySqlConnection.Close();
                }
                MessageBox.Show("DONE!");
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");

            }
        }
        //Ensuring unique values in the combobox
        private void COMBOBOX_SelectionChangedCommitted(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = sender as ComboBox;

            if (currentComboBox.SelectedValue == "None")
            {
                (currentComboBox.Tag as helper).prevVal = "None";
                return;
            }

            foreach (PreferenceSubject preferenceSubject in listView.Items)
            {
                if (preferenceSubject.SubjectType == "Elective Course")
                { //same item, continue
                    if (preferenceSubject.SubjectCode == (currentComboBox.Tag as helper).SubjectCode)
                    {
                        continue;
                    }
                    else
                    if (preferenceSubject.comboBox.SelectedValue == currentComboBox.SelectedValue)
                    {
                        MessageBox.Show("Same preference not allowed! ");

                        currentComboBox.SelectedValue = (currentComboBox.Tag as helper).prevVal;
                        return;
                    }
                }
                else
                    continue;

            }
        //Valid value change allowed
        (currentComboBox.Tag as helper).prevVal = currentComboBox.SelectedValue.ToString();

        }
    }
}
