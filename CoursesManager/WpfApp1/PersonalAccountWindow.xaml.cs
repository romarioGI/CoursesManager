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
using System.IO;
using CoursesManagerLib;
using System.Numerics;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountWindow.xaml
    /// </summary>
    public partial class PersonalAccountWindow : Window
    {
        private School _school;

        public PersonalAccountWindow(School school)
        {
            InitializeComponent();
            this._school = school;
        }

        private void ButtonPersClientWindow_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Owner.Visibility = Visibility.Visible;
        }
        public Course PersCourseRequest()
        {
            Course course = new Course(BoxPersLanguage.Text, int.Parse(BoxPersIntensity.Text), BoxPersLevel.Text, (Format)BoxPersFormat.SelectedItem, int.Parse(LabelPersDurationAvto.Content.ToString()), int.Parse(LabelPersPriceAvto.Content.ToString()));
            return course;
        }

        public void FillIn(Client cl)
        {
            TextBoxPersID.Text = cl.Id.ToString();
            TextBoxPersName.Text = cl.Name;
            TextBoxPersSurname.Text = cl.Surname;
            textBoxGetMoney.Text = cl.Account.ToString();
        }
        private void ButtonPersAddClaim_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            if (TextBoxPersID.Text != "")
            {
                if (int.TryParse(TextBoxPersID.Text, out id))
                {
                    var add = false;
                    foreach (var cl in _school.Clients)
                        if (cl.Id == id)
                        {
                            for (var i = 0; i < cl.CountGroups; i++)
                            {
                                var gr = cl.GetGroup(i);
                                if (gr.Course == PersCourseRequest())
                                {
                                    MessageBox.Show("You have group with this course");
                                    add = true;
                                    break;
                                }
                            }

                            for (var i = 0; i < cl.CountCourseRequests; i++)
                            {
                                var cs = cl.GetCourseRequest(i);
                                if (cs == PersCourseRequest())
                                {
                                    MessageBox.Show("You have Request with this course");
                                    add = true;
                                    break;
                                }
                            }

                            if (add)
                                break;
                            try
                            {
                                cl.AddCourseRequest(PersCourseRequest());
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("This claim already exists");
                                return;
                            }
                            add = true;
                            MessageBox.Show("Your claim is add");
                            break;
                        }
                    if (!add) MessageBox.Show("Wrong ID");
                }
                else MessageBox.Show("Wrong insertion of ID");
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

        private void ButtonChangeMoney_Click(object sender, RoutedEventArgs e)
        {
            var s = TextBoxMoney.Text;
            var money = new BigInteger(0);
            if (!BigInteger.TryParse(s, out money))
            {
                MessageBox.Show("Uncorrect count of money.");
                return;
            }

            Client client = null;
            var Id = int.Parse(TextBoxPersID.Text);
            foreach (var c in _school.Clients)
            {
                if (c.Id == Id)
                    client = c;
            }
            client.ChangeAccountSum(money);
            textBoxGetMoney.Text = client.Account.ToString();
        }
        

        private void EngPersInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("PersEngInstruction.txt");
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }

        private void RusPersInstruction_Click(object sender, RoutedEventArgs e)
        {
            StreamReader f = new StreamReader("PersRusInstruction.txt", Encoding.GetEncoding("windows-1251"));
            string s = f.ReadToEnd();
            f.Close();
            MessageBox.Show(s);
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            if (this.Visibility == Visibility.Visible)
                this.Owner.Close();
        }

        public void ChangeSchool(School newSchool)
        {
            _school = newSchool;
        }
    }
}
