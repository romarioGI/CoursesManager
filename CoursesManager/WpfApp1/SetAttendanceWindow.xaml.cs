using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoursesManagerLib;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для SetAttendanceWindow.xaml
    /// </summary>
    public partial class SetAttendanceWindow : Window
    {
        private School _school;
        private CheckBox[] _checkBoxs;

        public SetAttendanceWindow(School school)
        {
            InitializeComponent();
            _school = school;

            UpdateGroupIdComboBox();
            InitGrid();
        }

        public void UpdateInfo()
        {
            UpdateGroupIdComboBox();
        }

        private void UpdateGroupIdComboBox()
        {
            GroupIdComboBox.SelectedIndex = -1;
            GroupIdComboBox.ItemsSource = _school.Groups.Select(x => x.Id).ToArray();
        }

        private void InitGrid()
        {
            _checkBoxs = new CheckBox[10];
            for (var i = 0; i < 10; i++)
            {
                _checkBoxs[i] = new CheckBox();
                var rb = _checkBoxs[i];
                rb.HorizontalAlignment = HorizontalAlignment.Center;
                rb.VerticalAlignment = VerticalAlignment.Center;
                rb.Content = "YES";
                rb.FontSize = 20;
                ChangeGridField(AttendanceGrid, i, 1, rb);
                rb.Visibility = Visibility.Hidden;
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Owner.Visibility = Visibility.Visible;
            GroupIdComboBox.SelectedIndex = -1;
        }

        private void GroupIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AttendanceGrid.Children.Clear();
            InitGrid();

            if (GroupIdComboBox.SelectedIndex == -1)
                return;

            var group = _school.Groups[GroupIdComboBox.SelectedIndex];
            var cntClients = group.GetCount();

            for (var i = 0; i < cntClients; i++)
            {
                var l = new Label
                {
                    Content = group[i].Surname + " " + group[i].Name + "\nId: " + group[i].Id, FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center
                };
                ChangeGridField(AttendanceGrid, i, 0, l);

                _checkBoxs[i].Visibility = Visibility.Visible;
            }
        }

        private void ChangeGridField(Grid grid, int i, int j, UIElement content)
        {           
            Grid.SetColumn(content, j);
            Grid.SetRow(content, i);
            grid.Children.Add(content);            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (GroupIdComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No group selected. Select a group and save again.");
                return;
            }

            var selectDate = Date.SelectedDate;

            if (selectDate == null)
            {
                MessageBox.Show("No date selected. Select a date and save again.");
                return;
            }

            var left = new DateTime(2000, 1, 1);
            var right = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            right = right.AddDays(1);


            if (selectDate < left|| selectDate >= right)
            {
                MessageBox.Show(String.Format("the selected date must be between {0} and {1}", left, right));
                return;
            }

            var gr = _school.Groups[GroupIdComboBox.SelectedIndex];
            var date = selectDate.Value;
            var attCl = new List<Client>();

            for(var i=0;i<gr.CountClients;i++)
                if(_checkBoxs[i].IsChecked == true)
                    attCl.Add(gr[i]);

            try
            {
                gr.SetAttendance(attCl, date);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Something went wrong. Most likely, the lesson with this date already been carried out.");
                return;
            }
            MessageBox.Show("Successfully!");
            GroupIdComboBox.SelectedIndex = -1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
                this.Owner.Close();
        }

        public void ChangeSchool(School newSchool)
        {
            _school = newSchool;
            UpdateInfo();
        }
    }
}
