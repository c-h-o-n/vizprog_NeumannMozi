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
        private void btnLogin_Click(object sender, RoutedEventArgs e) {

        }
        private void btnRegister_Click(object sender, RoutedEventArgs e) {

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
