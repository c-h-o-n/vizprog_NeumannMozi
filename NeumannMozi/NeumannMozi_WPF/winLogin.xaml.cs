using System;
using System.Collections.Generic;
using System.IO;
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
using NeumannMozi_DAL;

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class winLogin : Window {
        public winLogin() {
            SetDataDirectory();
            InitializeComponent();
        }


        #region Button events
        
        #region Click
        private void btnExitApp_Click(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        private void btnForgattenPassword_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Nem mukodom :(");
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e) {

        }
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            var u = (from x in edmNeumannMoziContainer.FelhasznaloSet
                     where x.Nev == txtUsername.Text && x.Jelszo == txtPassword.Password select new { x.Id, x.Nev,x.Jelszo,x.Admin }).FirstOrDefault();
            if (u != null) {
                MessageBox.Show(string.Format("Sikeres bejelentkezés!\nFelhasználónév:{0}\nFelhasználó id: {1}\nAdminisztrátor: {2}",u.Nev,u.Id.ToString(),u.Admin.ToString()));
            }
            else {
                MessageBox.Show("Sikertelen bejelentkezés!\nHibás adatok!\nIde jöhet majd a kivételdobás.");
            }
        }
        #endregion

        #region Hover
        private void WindowButtons_MouseEnter(object sender, MouseEventArgs e) {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(191, 191, 191));
            Button x = sender as Button;
            x.Background = brush;
        }
        private void WindowButtons_MouseLeave(object sender, MouseEventArgs e) {
            Button x = sender as Button;
            x.Background = Brushes.Transparent;
        }
        #endregion

        #endregion

        #region Input events
        private void Input_GotFocus(object sender, RoutedEventArgs e) {
            if (sender is TextBox) {
                TextBox tb = (TextBox)sender;
                tb.Text = string.Empty;
            } else if (sender is PasswordBox) {
                PasswordBox pb = (PasswordBox)sender;
                pb.Password = string.Empty;
            }
            
        }
        #endregion

        #region Database
        // Set DataDirectory path to Project Solution path (....\NeumannMozi\)
        private void SetDataDirectory() {
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory());
            projectDir = Directory.GetParent(projectDir.ToString());
            projectDir = Directory.GetParent(projectDir.ToString());
            //
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDir.ToString());
        }








        #endregion


    }
}
