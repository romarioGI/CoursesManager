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
using CoursesManagerLib;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для GuestAccountWindow.xaml
    /// </summary>
    public partial class GuestAccountWindow : Window
    {
        private School _school;
        private PersonalAccountWindow _personalAccountWindow;
        public GuestAccountWindow(School school)
        {
            InitializeComponent();
            _school = school;
            this.Show();
            _personalAccountWindow = new PersonalAccountWindow(school)
            {
                Owner = this
            };
        }
        private void BoxIntensity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectInd = BoxIntensity.SelectedIndex;
            if (LabelDurationAvto != null)
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
            if (BoxIntensity != null)
                switch (caseformat)
                {
                    case Format.Individual:
                        LabelPriceAvto.Content = (800 * 4 * (BoxIntensity.SelectedIndex + 1)).ToString();
                        break;
                    case Format.Group:
                        LabelPriceAvto.Content = (500 * 4 * (BoxIntensity.SelectedIndex + 1)).ToString();
                        break;
                }
        }
        public Course CourseRequest()
        {
            Course course = new Course(BoxLanguage.Text, int.Parse(BoxIntensity.Text), BoxLevel.Text, (Format)BoxFormat.SelectedItem, int.Parse(LabelDurationAvto.Content.ToString()), int.Parse(LabelPriceAvto.Content.ToString()));
            return course;
        }
        private void ButtonAddClaim_Click(object sender, RoutedEventArgs e)
        {
            Client cl = null;
            try
            {
                cl = new Client(TextBoxName.Text, TextBoxSurname.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Name and Surname must not be empty");
                return;
            }
            cl.AddCourseRequest(CourseRequest());
            _school.Clients.Add(cl);

            TextBoxID.Text = cl.Id.ToString();
            MessageBox.Show("Your claim is add, see your ID");
        }
        private void ButtonAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Owner.Visibility = Visibility.Visible;
        }

        private void ButtonPersAccount_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (TextBoxIDInto.Text != "")
            {
                if (int.TryParse(TextBoxIDInto.Text, out id))
                {
                    bool add = false;
                    foreach (Client cl in _school.Clients)
                        if (cl.Id == id)
                        {
                            this.Visibility = Visibility.Hidden;
                            _personalAccountWindow.FillIn(cl);
                            _personalAccountWindow.Visibility = Visibility.Visible;
                            add = true;
                            break;
                        }
                    if (!add) MessageBox.Show("Wrong ID");
                }
                else MessageBox.Show("Wrong insertion of ID");
            }
        }

        private void EngInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("EngInstruction.txt");
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }

        private void RusInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("RusInstruction.txt", Encoding.GetEncoding("windows-1251"));
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }
    }
}
