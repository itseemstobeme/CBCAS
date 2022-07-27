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
using System.Text.RegularExpressions;

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
        private TeacherWindow mainTeacherWindow { get; set; }
        private bool electiveSwitch { get; set; }

        public AllocateSubjects(string currentYear, string currentDegree, string currentBranch, string currentSem, TeacherWindow mainTeacherWindow)
        {
            InitializeComponent();
            this.currentYear = currentYear;
            this.currentDegree = currentDegree;
            this.currentBranch = currentBranch;
            this.currentSem = currentSem;
            this.electiveSwitch = false;
            this.mainTeacherWindow = mainTeacherWindow;
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
                string cmdString = "SELECT * FROM " + tableName + " ORDER BY Ranking ASC,SubjectType ASC";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();


                while (mySqlDataReader.Read())
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
                        electiveSwitch = true;
                        allocationSubject.checkBox.IsChecked = true;
                        allocationSubject.textBox.Text = "0";
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

        //For ensuring that seat count input is always numeric
        private void NumericTextBoxInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Algo for allocating subjects
        private void AllocateSubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            List<AllocationSubject> finalSubjectsList = new List<AllocationSubject>();

            int seatCount = 0;
            foreach (AllocationSubject item in listView.Items)
            {
                //For elective subjects which are selected
                if ((bool)item.checkBox.IsChecked && item.checkBox.IsEnabled)
                {
                    string rep = item.textBox.Text.Replace(" ", "");
                    item.textBox.Text = rep;
                    seatCount += int.Parse(item.textBox.Text);
                }
                finalSubjectsList.Add(item);
            }

            //No elective subjects
            if (electiveSwitch == false)
            {
                DumpCoreSubjectsToDB(finalSubjectsList, true);
            }
            else
            {
                if (!CheckValidityOfElectives(seatCount))
                {
                    return;
                }
                DumpSubjectsToDBWithElectives(finalSubjectsList);
            }

        }

        private void DumpCoreSubjectsToDB(List<AllocationSubject> finalSubjectsList, bool close)
        {
            string tableName = currentYear + currentDegree + currentBranch + currentSem;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                // Creating Table 'FINAL2019BTECHBT1'{FinalYearDegreeBranchSem}
                mySqlConnection.Open();
                string cmdString = "CREATE TABLE FINAL" + tableName + "(SubjectCode varchar(20),"
                    + "StudentID varchar(20),PRIMARY KEY(SubjectCode,StudentID))";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();


                //Getting STUDENTID(s) from particular Year+Degree+Branch
                mySqlConnection.Open();
                List<string> studentID = new List<string>();
                cmdString = "SELECT STUDENTID FROM STUDENT WHERE YEAR =" + currentYear +
                    " AND DEGREE ='" + currentDegree + "' AND BRANCH ='" + currentBranch + "'";
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    studentID.Add(mySqlDataReader.GetString(0));
                }
                cmd.Dispose();
                mySqlConnection.Close();


                //Inserting all combos of (CoreCourseSubjectIDs,StudentIDs) into {FinalYearDegreeBranchSem}
                mySqlConnection.Open();
                cmdString = "INSERT INTO FINAL" + tableName + " VALUES (";
                for (int i = 0; i < finalSubjectsList.Count; ++i)
                {
                    for (int j = 0; j < studentID.Count; ++j)
                        cmdString += "'" + finalSubjectsList[i].SubjectCode + "','" + studentID[j] + "'),(";

                }
                cmdString = cmdString.Remove(cmdString.Length - 2, 2);
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();

                //Marking all allocated subjects' ALLOCATIONYN = 'Y' IN {YearDegreeBranchSem}
                mySqlConnection.Open();
                cmdString = "UPDATE " + tableName + " SET ALLOCATIONYN ='Y'";
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();


                //Close Window and return

                MessageBox.Show("Core courses allocated!\nReturning to main window");
                if (close)
                {
                    TeacherWindow teacherWindow = new TeacherWindow();
                    teacherWindow.Show();
                    mainTeacherWindow.Close();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }


        }

        public class StudentAllocationForElectives
        {
            public string StudentID { get; set; }
            public string Preference { get; set; }
            public float GPA { get; set; }
        }
        private void DumpSubjectsToDBWithElectives(List<AllocationSubject> finalSubjectsList)
        {
            string tableName = currentYear + currentDegree + currentBranch + currentSem;
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                List<AllocationSubject> coreSubjects = new List<AllocationSubject>();

                //electiveSubjects<SubjectCode,Pair<seatCount,allotedToAlteastOneStudent?>>
                Dictionary<string, Pair<int, bool>> electiveSubjects = new Dictionary<string, Pair<int, bool>>();
                for (int i = 0; i < finalSubjectsList.Count; ++i)
                {
                    if (finalSubjectsList[i].SubjectType == "Core Course")
                    {
                        coreSubjects.Add(finalSubjectsList[i]);
                    }
                    else
                    {
                        Pair<int, bool> pair = new
                            Pair<int, bool>(int.Parse(finalSubjectsList[i].textBox.Text.ToString()), false);
                        electiveSubjects[finalSubjectsList[i].SubjectCode] = pair;
                    }

                }

                DumpCoreSubjectsToDB(coreSubjects,false);

                //Allocating Elective Subjects
                //Getting Student Preferences from {Student} table
                //<Pair<StudentId,Preference>>
                List<StudentAllocationForElectives> students = new List<StudentAllocationForElectives>();
                bool GPAExistence = true;   //to keep track if GPA exists or not
                mySqlConnection.Open();
                string cmdString = "SELECT STUDENTID,PREFERENCE,CGPA FROM STUDENT WHERE YEAR =" + currentYear +
                    " AND DEGREE ='" + currentDegree + "' AND BRANCH ='" + currentBranch + "'";
                MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    StudentAllocationForElectives newStudent = new StudentAllocationForElectives();
                    newStudent.StudentID = mySqlDataReader.GetString(0);
                    newStudent.Preference = mySqlDataReader.GetString(1);
                    if (!mySqlDataReader.IsDBNull(2))
                        newStudent.GPA = mySqlDataReader.GetFloat(2);
                    else
                        GPAExistence = false;

                    students.Add(newStudent);
                }
                cmd.Dispose();
                mySqlConnection.Close();

                //Sorting students list according to GPA(desc) if GPA exists in {Student} table
                if (GPAExistence)
                {
                    students = students.OrderByDescending(o => o.GPA).ToList();

                }

                //Inserting allocation of (ElectiveSubjectID,StudentID) into {FinalYearDegreeBranchSem}
                mySqlConnection.Open();
                cmdString = "INSERT INTO FINAL" + tableName + " VALUES (";
                for (int i = 0; i < students.Count; ++i)
                {
                    int j = 0;
                    //students[i].Preference will be like = "Sub1,Sub2,Sub3,Sub4"
                    while (j < students[i].Preference.Length)
                    {
                        string currentSubject = "";
                        int pos = j;
                        while (pos < students[i].Preference.Length)
                        {
                            if (students[i].Preference[pos] == ',')
                                break;
                            currentSubject += students[i].Preference[pos];
                            ++pos;
                        }
                        j = pos + 1;

                        if (electiveSubjects[currentSubject].First > 0)
                        {
                            --electiveSubjects[currentSubject].First;   //seatCount decrease by one
                            electiveSubjects[currentSubject].Second = true; //allocated to atleast one student
                            cmdString += "'" + currentSubject + "','" + students[i].StudentID + "'),(";
                            break;
                        }
                    }
                }
                cmdString = cmdString.Remove(cmdString.Length - 2, 2);
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();

                //Set ALLOCATIONYN='Y' for allocated elective subjects in {YearDegBranchSem}
                mySqlConnection.Open();
                cmdString = "UPDATE " + tableName + " SET ALLOCATIONYN ='Y' WHERE SUBJECTCODE IN(";
                foreach (KeyValuePair<string, Pair<int, bool>> obj in electiveSubjects)
                {
                    //If allocated
                    if (obj.Value.Second)
                    {
                        cmdString += "'" + obj.Key + "',";
                    }
                }
                cmdString = cmdString.Remove(cmdString.Length - 1, 1);
                cmdString += ")";
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();


                //DELETE ALL SUBJECTS IN {YearDegBranchSem} WHOSE ALLOCATION = 'N' ,ie, unallocated subjects
                mySqlConnection.Open();
                cmdString = "DELETE FROM " + tableName + " WHERE ALLOCATIONYN = 'N'";
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();


                //RESET (Preference,preferenceSem) in {Student} table
                mySqlConnection.Open();
                cmdString = "UPDATE STUDENT SET PREFERENCE = NULL,PREFERENCESEM = NULL WHERE YEAR =" +
                    currentYear + " AND DEGREE ='" + currentDegree + "' AND BRANCH ='" +
                    currentBranch + "'";
                cmd = new MySqlCommand(cmdString, mySqlConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mySqlConnection.Close();


                //Close Window and return
                MessageBox.Show("Elective courses allocated!\nReturning to main window");
                TeacherWindow teacherWindow = new TeacherWindow();
                teacherWindow.Show();
                mainTeacherWindow.Close();
                this.Close();
            }
            catch
            {
                MessageBox.Show("SOME ERROR OCCURED");
            }
        }

        //Checking if electives can be allocated based on current state
        private bool CheckValidityOfElectives(int seatCount)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["offlineconnectionString"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            mySqlConnection.Open();
            string cmdString = "SELECT * FROM STUDENT";
            MySqlCommand cmd = new MySqlCommand(cmdString, mySqlConnection);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

            int studentCount = 0;
            while (mySqlDataReader.Read())
            {
                ++studentCount;

                //If any student hasn't filled a preference of elective
                if (mySqlDataReader.IsDBNull(6))
                {
                    MessageBox.Show("Not all students have filled preferences, cannot allocate!");
                    return false;
                }
            }

            //If seats are less than no of students
            if (studentCount > seatCount)
            {
                MessageBox.Show("Seat count is less than student count");
                MessageBox.Show("Please select more electives or allocate more seats");
                return false;
            }

            //Else return true
            return true;
        }

    }
}
