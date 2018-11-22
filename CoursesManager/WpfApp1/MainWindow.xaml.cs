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
            foreach (Group gr in school.Groups)
                s += gr.ToString() + "\n";
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
            foreach (Client cl in school.Clients)
                s += cl.ToString() + "\n";
            ShowPanel.Text = s;
        }

        private void ShowClaims_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            foreach (Claim cl in school.Claims)
                s += cl.ToString() + "\n";
            ShowPanel.Text = s;
        }
        public Course CorseRequest ()
        {
            Course course = new Course(BoxLanguage.Text,int.Parse(BoxIntensity.Text),BoxLevel.Text,(Format)BoxFormat.SelectedItem, int.Parse(LabelDurationAvto.Content.ToString()),int.Parse(LabelPriceAvto.Content.ToString()));
            return course;
        }

        private void ButtonClaim_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (TextBoxID.Text != "")
            {
                if (int.TryParse(TextBoxID.Text, out id))
                {
                    bool add = false;
                    foreach (Client cl in school.Clients)
                        if (cl.Id == id)
                        {
                            cl.AddCourseRequest(CorseRequest());
                            add = true;
                            break;
                        }
                    if (!add) LabelID.Content = "Wrong ID";
                }
                else LabelID.Content = "Wrong insertion of ID";
            }
            else
            {
                Client cl = new Client(TextBoxName.Text,TextBoxSurname.Text);
                cl.AddCourseRequest(CorseRequest());
                school.Clients.Add(cl);
            }
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
                            foreach (Group gr in cl.Groups)
                                s += gr.ToString() + "\n";
                            ShowPanel.Text = s;
                            add = true;
                            break;
                        }
                    if (!add) LabelIDSearch.Content = "Wrong ID";
                }
                else LabelIDSearch.Content = "Wrong insertion of ID";
            }
        }
    }
}
