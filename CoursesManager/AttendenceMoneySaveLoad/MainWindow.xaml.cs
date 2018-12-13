using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
using CoursesManagerLib;
using System.Windows.Forms;

namespace AttendenceMoneySaveLoad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private School school;

        public MainWindow()
        {
            InitializeComponent();

           
            school = new School();
            school.Clients.Add(new Client("q", "q"));
            school.Clients.Add(new Client("@@@@@@@@", "q"));

            school.Groups.Add(new Group(new Course(" 0", 1, "1",Format.Individual, 1 ,1)));
            school.Groups[0].AddClient(new Client("123","qwerty"));

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox.ItemsSource = school.Groups[0].GetAttendance();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = @"Binary file|*.bin";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                School.Serialize(sfd.FileName, school);
                System.Windows.Forms.MessageBox.Show("Successfully saved to\n" + sfd.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = @"Binary file|*.bin";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                school = School.Deserialize(ofd.FileName);
                System.Windows.Forms.MessageBox.Show("Successfully load from\n" + ofd.FileName);
            }
        }

        private void ChangeAccountSum_Click(object sender, RoutedEventArgs e)
        {
            var curClient = new Client("Как-то нужно достать клиента текущего", "вот");
            curClient.ChangeAccountSum(int.Parse(ChangeAccountSumTextBox.Text));
        }
    }
}
