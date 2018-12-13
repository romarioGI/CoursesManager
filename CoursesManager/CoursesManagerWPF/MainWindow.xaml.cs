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

namespace CoursesManagerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private School _school;
        private event SchoolUpdate _schoolUpdate;

        private Window _getAttendanceWindow;
        private Window _setAttendanceWindow;

        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            _school = new School();
            _getAttendanceWindow = new GetAttendanceWindow(_school, _schoolUpdate);
            _getAttendanceWindow.Owner = this;
            _setAttendanceWindow = new SetAttendanceWindow(_school, _schoolUpdate);
            _setAttendanceWindow.Owner = this;
        }

        private void GetAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            _getAttendanceWindow.Visibility = Visibility.Visible;
        }

        private void SetAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            _setAttendanceWindow.Visibility = Visibility.Visible;
        }
    }
}
