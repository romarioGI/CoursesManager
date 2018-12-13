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
using CoursesManagerLib;
using System.Windows.Forms;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        School school;
        public MainWindow()
        {
            InitializeComponent();
            school = new School();
            BoxLanguage.SelectedIndex = 1;
        }

        private void ShowGroups_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            s += school.Groups.Count.ToString()+"   groups\n";
            int i = 0;
            foreach (Group gr in school.Groups)
            {
                i++;
                s += i.ToString()+")"+gr.ToString() + "\n";
            }
            ShowPanel.Text = s;
        }

        private void BoxIntensity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectInd = BoxIntensity.SelectedIndex;
            if(LabelDurationAvto!=null)
            switch (SelectInd)
            {
                    case 0:
                        LabelDurationAvto.Content = "10";
                        BoxFormat_SelectionChanged(sender, e);
                        break;
                    case 1:
                        LabelDurationAvto.Content = "5";
                        BoxFormat_SelectionChanged(sender, e);
                        break;
                    case 2:
                        LabelDurationAvto.Content = "3";
                        BoxFormat_SelectionChanged(sender, e);
                        break;
                }
        }

        private void BoxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Format caseformat = (Format)BoxFormat.SelectedValue;
            if(BoxIntensity!=null)
            switch (caseformat)
            {
                case Format.Individual:
                    LabelPriceAvto.Content = (800*4*(BoxIntensity.SelectedIndex+1)).ToString();
                    break;
                case Format.Group:
                    LabelPriceAvto.Content = (500 * 4 * (BoxIntensity.SelectedIndex + 1)).ToString();
                    break;
            }
        }

        private void ShowClients_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            s += school.Clients.Count.ToString() + "  clients\n";
            int i = 0;
            foreach (Client cl in school.Clients)
            {
                i++;
                s +=i.ToString()+")"+ cl.ToString();
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

        public Course CourseRequest ()
        {
            Course course = new Course(BoxLanguage.Text,int.Parse(BoxIntensity.Text),BoxLevel.Text,(Format)BoxFormat.SelectedItem, int.Parse(LabelDurationAvto.Content.ToString()),int.Parse(LabelPriceAvto.Content.ToString()));
            return course;
        }

        private void ButtonAddClaim_Click(object sender, RoutedEventArgs e)
        {
            LabelIsAddClaim.Content = "";
                Client cl = new Client(TextBoxName.Text,TextBoxSurname.Text);
                cl.AddCourseRequest(CourseRequest());
                school.Clients.Add(cl);
                TextBoxID.Text = cl.Id.ToString();
            LabelIsAddClaim.Content = "Your claim is add, see your ID";
        }

        private void ButtonAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            GridClient.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Visible;
        }

        private void ButtonAdmission_Click(object sender, RoutedEventArgs e)
        {
            school.Admission();
        }

        private void ButtonClientWindow_Click(object sender, RoutedEventArgs e)
        {
            GridClient.Visibility = Visibility.Visible;
            GridAdmin.Visibility = Visibility.Hidden;
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
                            //foreach (Group gr in cl.Groups)
                            for (var i = 0; i < cl.CountGroups; i++)
                            {
                                var gr = cl.GetGroup(i);
                                s += gr.ToString() + "\n";
                            }

                            ShowPanel.Text = s;
                            add = true;
                            break;
                        }
                    if (!add) LabelIDSearch.Content = "Wrong ID";
                }
                else LabelIDSearch.Content = "Wrong insertion of ID";
            }
        }

        private void ButtonPersClientWindow_Click(object sender, RoutedEventArgs e)
        {
            GridPersonalClient.Visibility = Visibility.Hidden;
            GridClient.Visibility = Visibility.Visible;
        }

        private void ButtonPersAddClaim_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            LabelPersIsAddClaim.Content = "";
            if (TextBoxPersID.Text != "")
            {
                if (int.TryParse(TextBoxPersID.Text, out id))
                {
                    var add = false;
                    foreach (var cl in school.Clients)
                        if (cl.Id == id)
                        {
                            //foreach (var gr in cl.Groups)
                            for (var i = 0; i < cl.CountGroups; i++)
                            {
                                var gr = cl.GetGroup(i);
                                if (gr.Course == CourseRequest())
                                {
                                    LabelPersIsAddClaim.Content = "You have group with this course";
                                    add = true;
                                    break;
                                }
                            }

                            //foreach (Course  cs in cl.CourseRequests)
                            for (var i = 0; i < cl.CountCourseRequests; i++)
                            {
                                var cs = cl.GetCourseRequest(i);
                                if (cs == CourseRequest())
                                {
                                    LabelPersIsAddClaim.Content = "You have Request with this course";
                                    add = true;
                                    break;
                                }
                            }

                            if (add)
                                break;
                            cl.AddCourseRequest(CourseRequest());
                            add = true;
                            LabelPersIsAddClaim.Content = "Your claim is add";
                            break;
                        }
                    if (!add) LabelPersIsAddClaim.Content = "Wrong ID";
                }
                else LabelPersIsAddClaim.Content = "Wrong insertion of ID";
            }
        }

        private void BoxPersFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Format caseformat = (Format)BoxPersFormat.SelectedValue;
            if (BoxPersIntensity != null)
                switch (caseformat)
                {
                    case Format.Individual:
                        LabelPersPriceAvto.Content = (800 * 4 * (BoxPersIntensity.SelectedIndex + 1)).ToString();
                        break;
                    case Format.Group:
                        LabelPersPriceAvto.Content = (500 * 4 * (BoxPersIntensity.SelectedIndex + 1)).ToString();
                        break;
                }
        }

        private void BoxPersIntensity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectInd = BoxPersIntensity.SelectedIndex;
            if (LabelPersDurationAvto != null)
                switch (SelectInd)
                {
                    case 0:
                        LabelPersDurationAvto.Content = "10";
                        BoxPersFormat_SelectionChanged(sender, e);
                        break;
                    case 1:
                        LabelPersDurationAvto.Content = "5";
                        BoxPersFormat_SelectionChanged(sender, e);
                        break;
                    case 2:
                        LabelPersDurationAvto.Content = "3";
                        BoxPersFormat_SelectionChanged(sender, e);
                        break;
                }
        }

        private void ButtonPersAccount_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            LabelIsAddClaim.Content = "";
            if (TextBoxIDInto.Text != "")
            {
                if (int.TryParse(TextBoxIDInto.Text, out id))
                {
                    bool add = false;
                    foreach (Client cl in school.Clients)
                        if (cl.Id == id)
                        {
                            GridClient.Visibility = Visibility.Hidden;
                            GridPersonalClient.Visibility = Visibility.Visible;
                            TextBoxPersID.Text = cl.Id.ToString();
                            TextBoxPersName.Text = cl.Name;
                            TextBoxPersSurname.Text = cl.Surname;
                            add = true;
                            break;
                        }
                    if (!add) LabelIsAddClaim.Content = "Wrong ID";
                }
                else LabelIsAddClaim.Content = "Wrong insertion of ID";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog {Filter = @"Binary file|*.bin"};
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                School.Serialize(sfd.FileName, school);
                System.Windows.Forms.MessageBox.Show("Successfully saved to\n" + sfd.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog {Filter = @"Binary file|*.bin"};
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                school = School.Deserialize(ofd.FileName);
                System.Windows.Forms.MessageBox.Show("Successfully load from\n" + ofd.FileName);
            }
        }
    }
}
