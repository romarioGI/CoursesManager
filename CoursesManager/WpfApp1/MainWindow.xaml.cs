using System;
using System.Text;
using System.Windows;
using CoursesManagerLib;
using System.Windows.Forms;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        School school;

        private GetAttendanceWindow _getAttendanceWindow;
        private SetAttendanceWindow _setAttendanceWindow;
        private GuestAccountWindow _guestAccountWindow;


        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            school = new School();

            _getAttendanceWindow = new GetAttendanceWindow(school)
            {
                Owner = this
            };
            _setAttendanceWindow = new SetAttendanceWindow(school)
            {
                Owner = this
            };
        }

        private void ShowGroups_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            s += school.Groups.Count.ToString() + "   groups\n";
            int i = 0;
            foreach (Group gr in school.Groups)
            {
                i++;
                s += i.ToString() + ")" + gr.ToString() + "\n";
            }
            ShowPanel.Text = s;
        }

        private void ShowClients_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            s += school.Clients.Count.ToString() + "  clients\n";
            int i = 0;
            foreach (Client cl in school.Clients)
            {
                i++;
                s += i.ToString() + ")" + cl.ToString();
            }
            ShowPanel.Text = s;
        }

        private void ShowClaims_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            s += school.Claims.Count.ToString() + "   claims\n";
            foreach (Claim cl in school.Claims)
                s += cl.ToString() + "\n";
            ShowPanel.Text = s;
        }
        private void ButtonAdmission_Click(object sender, RoutedEventArgs e)
        {
            school.Admission();
        }

        private void ButtonClientWindow_Click(object sender, RoutedEventArgs e)
        {
            if(_guestAccountWindow==null)
            {
                _guestAccountWindow = new GuestAccountWindow(school);
                _guestAccountWindow.Owner = this;
            }
            _guestAccountWindow.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
        }

        private void ButtonID_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (TextBoxIDSearch.Text != "")
            {
                if (int.TryParse(TextBoxIDSearch.Text, out id))
                {
                    bool add = false;
                    foreach (Client cl in school.Clients)
                        if (cl.Id == id)
                        {
                            string s = "";
                            s += cl.ToString() + "\n";
                            for (var i = 0; i < cl.CountGroups; i++)
                            {
                                var gr = cl.GetGroup(i);
                                s += gr.ToString() + "\n";
                            }

                            ShowPanel.Text = s;
                            add = true;
                            break;
                        }
                    if (!add) MessageBox.Show("Wrong ID");
                }
                else MessageBox.Show("Wrong insertion of ID");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = @"Binary file|*.bin" };
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                School.Serialize(sfd.FileName, school);
                System.Windows.Forms.MessageBox.Show("Successfully saved to\n" + sfd.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"Binary file|*.bin" };
            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    school = School.Deserialize(ofd.FileName);
                    System.Windows.Forms.MessageBox.Show("Successfully load from\n" + ofd.FileName);
                }

                _getAttendanceWindow.ChangeSchool(school);
                _setAttendanceWindow.ChangeSchool(school);
                _guestAccountWindow?.ChangeSchool(school);
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show("Not successfully load from\n" + ofd.FileName);
            }            
        }

        private void ButtonSetAttendance_Click(object sender, RoutedEventArgs e)
        {
            _setAttendanceWindow.UpdateInfo();
            this.Visibility = Visibility.Hidden;
            _setAttendanceWindow.Visibility = Visibility.Visible;
        }

        private void ButtonGetAttendance_Click(object sender, RoutedEventArgs e)
        {
            _getAttendanceWindow.UpdateInfo();
            this.Visibility = Visibility.Hidden;
            _getAttendanceWindow.Visibility = Visibility.Visible;
        }

        private void AdminEngInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("EngAdminInstruction.txt");
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }
        private void AdminRusInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("RusAdminInstruction.txt",Encoding.GetEncoding("windows-1251"));
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }
    }
}
