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

namespace CBCAS
{
    /// <summary>
    /// Interaction logic for AddSubjectWindow.xaml
    /// </summary>
    
    //To keep count of open new add subject subwindows as 1
    public static class OpenWindow
    {
        public static int countOpenWindow = 0;
    }

    public partial class AddSubjectWindow : Window
    {
        private Dictionary<string, Subject> map { get; set; }  //for <SubjectCode,Bool>
        public List<Subject> subjects { get; set; }
        public List<Subject> commitedSubjects { get; set; }
        
        //For adding new subjects
        public AddSubjectWindow(ref List<Subject> commitedSubjects,ref Dictionary<string,Subject> map)
        {
            InitializeComponent();
            this.map = map;
            subjects = new List<Subject>();
            this.commitedSubjects = commitedSubjects;
            for (int i = 0; i < commitedSubjects.Count; ++i)
            {
                subjects.Add(commitedSubjects[i]);
                listView.Items.Add(commitedSubjects[i]);
            }
        }
        
        //For viewing allocated subjects 
        public AddSubjectWindow(ref List<Subject> commitedSubjects)
        {
            InitializeComponent();
            AddNewSubject.IsEnabled = false;
            SaveSubjects.IsEnabled = false;
            AddNewSubjectLabel.Background = Brushes.LightGray;
            AddNewSubjectLabel.Foreground = Brushes.DarkGray;
            HeaderLabel.Content = "Allocated subjects are :";
            DeleteMenu.Visibility = Visibility.Hidden;
            this.Title = "View Allocated Subjects";
            for (int i = 0; i < commitedSubjects.Count; ++i)
            {
                listView.Items.Add(commitedSubjects[i]);
            }
        }
        private void AddNewSubject_Click(object sender, RoutedEventArgs e)
        {
            if (OpenWindow.countOpenWindow == 1)
                return;

            ++OpenWindow.countOpenWindow;
            Dictionary<string, Subject> m = map;
            List<Subject> list = subjects;
            NewSubjectSubwindow newSubjectSubwindow = new NewSubjectSubwindow(ref m,ref listView,ref list);
            newSubjectSubwindow.ShowDialog();
            //Auto resize the columns
            gridView.Columns[0].Width = double.NaN;
            gridView.Columns[1].Width = double.NaN;
            gridView.Columns[2].Width = double.NaN;
        }

        //Right click and delete Context Menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Array selectedItems = listView.SelectedItems.Cast<Object>().ToArray();
            foreach (Subject item in selectedItems)
            {
                listView.Items.Remove(item);
                map.Remove(item.SubjectCode);
                subjects.Remove(item);
                if (item.SubjectType == "Elective Course")
                    --ElectiveCount.electiveCount;
            }
            
        }

        //Save added list of subjects and return
        private void SaveSubjects_Click(object sender, RoutedEventArgs e)
        {
            commitedSubjects = subjects;
            this.Close();
        }
    }
}
