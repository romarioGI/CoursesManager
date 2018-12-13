using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoursesManagerLib;

namespace CoursesManagerWPF
{
    /// <summary>
    /// Логика взаимодействия для GetAttendanceWindow.xaml
    /// </summary>
    public partial class GetAttendanceWindow : Window
    {
        private School _school;

        public GetAttendanceWindow( School school, SchoolUpdate schoolUpdateEvent)
        {
            InitializeComponent();

            _school = school;

            schoolUpdateEvent += UpdateGroupIdComboBox;

            var g = new Group(new Course(" ", 1, " ", Format.Group, 3, 3));

           /* _school = new School();

            _school.Groups.Add(g);
            g.AddClient(new Client("TEST", "teat"));
            g.AddClient(new Client("Romka", "Pomka"));
            g.AddClient(new Client("Ne", "ZHozhy"));

            g.SetAttendance(new List<Client>() {g[0]}, DateTime.Today);
            g.SetAttendance(new List<Client>(), DateTime.MaxValue);
            g.SetAttendance(new List<Client>() {g[0], g[1]}, DateTime.Now.Add(TimeSpan.FromDays(25)));
            g.SetAttendance(new List<Client>() {g[0], g[1]}, DateTime.Now.Add(TimeSpan.FromDays(26)));
            g.SetAttendance(new List<Client>() {g[0], g[1]}, DateTime.Now.Add(TimeSpan.FromDays(27)));
            g.AddClient(new Client("A", "A"));
            g.SetAttendance(new List<Client>() {g[0], g[1]}, DateTime.Now.Add(TimeSpan.FromDays(28)));
            g.SetAttendance(new List<Client>() {g[0], g[1]}, DateTime.Now.Add(TimeSpan.FromDays(29)));*/

            UpdateGroupIdComboBox();
            InitGrid(0);
        }

        private void UpdateGroupIdComboBox()
        {
            GroupIdComboBox.SelectedIndex = -1;
            GroupIdComboBox.ItemsSource = _school.Groups.Select(x => x.Id).ToArray();
        }

        private void InitGrid(int countColumn)
        {
            AttendanceGrid.ColumnDefinitions.Clear();
            for (var i = 0; i < countColumn; i++)
                AttendanceGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void ChangeGridField(Grid grid, int i, int j, UIElement content)
        {
            Grid.SetColumn(content, j);
            Grid.SetRow(content, i);
            grid.Children.Add(content);
        }

        private void GroupIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupIdComboBox.SelectedIndex == -1)
            {
                AttendanceGrid.Children.Clear();
                InitGrid(0);
                return;
            }

            var group = _school.Groups[GroupIdComboBox.SelectedIndex];
            var cntClients = group.GetCount();
            var attendance = group.GetAttendance();
            List<DateTime> dates = null;
            var cntColumn = 1;
            if (attendance.Values.Count != 0)
            {
                dates = attendance.Values[0].Keys.ToList();
                cntColumn += dates.Count;
            }

            AttendanceGrid.Children.Clear();
            InitGrid(cntColumn);

            for (var i = 0; i < cntClients; i++)
            {
                var l = new Label
                {
                    Content = group[i].Surname + " " + group[i].Name + "\nId: " + group[i].Id,
                    FontSize = 17,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };
                ChangeGridField(AttendanceGrid, i + 1, 0, l);
            }

            for (var j = 0; j < cntColumn - 1; j++)
            {
                var l = new Label
                {
                    Content = dates[j].Date.ToString("d"),
                    FontSize = 17,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };
                ChangeGridField(AttendanceGrid, 0, j + 1, l);
            }

            for (var i = 1; i <= cntClients; i++)
            for (var j = 1; j < cntColumn; j++)
            {
                var l = new Label();
                var res = attendance[group[i - 1]][dates[j - 1]];
                if (res == null)
                    l.Background = Brushes.Gray;
                else
                {
                    if (res.Value == true)
                        l.Background = Brushes.Green;
                    else
                        l.Background = Brushes.Red;
                }

                ChangeGridField(AttendanceGrid, i, j, l);
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Owner.Visibility = Visibility.Visible;
            GroupIdComboBox.SelectedIndex = -1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
                this.Owner.Close();
        }
    }
}
