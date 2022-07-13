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
    public partial class AddSubjectWindow : Window
    {
        public AddSubjectWindow()
        {
            InitializeComponent();
            for(int i = 0;i<100;++i)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = i;
                listViewItem.Content = i;
                LV.Items.Add(listViewItem);
            }
        }

        private void AddNewSubject_Click(object sender, RoutedEventArgs e)
        {
            NewSubjectSubwindow newSubjectSubwindow = new NewSubjectSubwindow();
            newSubjectSubwindow.Show();
        }
    }
}
