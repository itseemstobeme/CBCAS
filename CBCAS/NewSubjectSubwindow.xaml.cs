using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NewSubjectSubwindow.xaml
    /// </summary>
    public partial class NewSubjectSubwindow : Window
    {   
        private Dictionary<string, Subject> map { get; set; }
        private ListView listView { get; set; }
        private List<Subject> subjects { get; set; }
        public NewSubjectSubwindow(ref Dictionary<string, Subject> map,ref ListView listView,ref List<Subject> subjects)
        {
            InitializeComponent();
            this.map = map;
            this.listView = listView;
            this.subjects = subjects;
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            if(SubjectCode.Text.Length == 0 || SubjectName.Text.Length == 0)
            {
                if (SubjectCode.Text.Length == 0)
                    MessageBox.Show("Please enter a valid Subject Code!");
                else
                    MessageBox.Show("Please enter a valid Subject Name!");
            }
            else
            {
                Subject currentSubject = new Subject();
                currentSubject.SubjectCode = SubjectCode.Text;
                currentSubject.SubjectName = SubjectName.Text;
                currentSubject.SubjectType = SubjectType.Text;

                if (map.ContainsKey(SubjectCode.Text))
                {
                    MessageBox.Show("Subject Code already exists, please use a different one");
                }
                else
                {
                    map.Add(SubjectCode.Text, currentSubject);
                    subjects.Add(currentSubject);
                    listView.Items.Add(currentSubject);
                    --OpenWindow.countOpenWindow;
                    this.Close();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            --OpenWindow.countOpenWindow;
            this.Close();
        }
    }
}
