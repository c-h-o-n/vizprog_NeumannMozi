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

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for winMain.xaml
    /// </summary>
    public partial class winMain : Window {
        public winMain() {
            /*
             * ADMIN LOGIN:
             * winLogin.loginAdmin = true;
             * ================================
             * USER LOGIN:
             * winLogin.loginAdmin = false;
             */
            winLogin.loginAdmin = true;
            winLogin.SetDataDirectory();
            InitializeComponent();
            SetNavButtons(winLogin.loginAdmin);
            if (!winLogin.loginAdmin) {
                // Open showtimes on startup
                uctShowtimes = new uctShowtimes();
                wpCurrentContent.Children.Add(uctShowtimes);
            } else {
                uctAdmin = new uctAdmin();
                wpCurrentContent.Children.Add(uctAdmin);
            }
        }


        #region CORE_VARIABLES
        private uctShowtimes uctShowtimes;
        private uctAdmin uctAdmin;
        #endregion
        #region BUTTON_CLICK_EVENTS

            #region WINDOW
            private void btnExit_Click(object sender, RoutedEventArgs e) {
                App.Current.Shutdown();
            }
            private void btnSizeState_Click(object sender, RoutedEventArgs e) {
                if (this.WindowState == WindowState.Normal) {
                    this.WindowState = WindowState.Maximized;
                } else {
                    this.WindowState = WindowState.Normal;
                }
            }
            private void btnMinimize_Click(object sender, RoutedEventArgs e) {
                WindowState = WindowState.Minimized;
            }
            #endregion

            #region NAV_MENU
            private void btnShowTimes_Click(object sender, RoutedEventArgs e) {
            // TODO: && !winLogin.loginAdmin protection should define in xaml
            if (!wpCurrentContent.Children.Contains(uctShowtimes) && !winLogin.loginAdmin) {
                wpCurrentContent.Children.Clear();
                uctShowtimes = new uctShowtimes();
                wpCurrentContent.Children.Add(uctShowtimes);
            } else {
                wpCurrentContent.Children.Clear();
                uctAdmin = new uctAdmin();
                wpCurrentContent.Children.Add(uctAdmin);
            }
            }
            #endregion

        #endregion

        #region BUG_FIXING
        // Dirty little code to fix the issue when in maximized state the window overlap the taskbar
        private void Window_StateChanged(object sender, EventArgs e) {
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowStyle = WindowStyle.None;
        }

        #endregion

        private void btnMyTickets_Click(object sender, RoutedEventArgs e) {

        }

        private void btnAboutTheatre_Click(object sender, RoutedEventArgs e) {

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e) {

        }

        private void SetNavButtons(bool isAdmin) {
            if (isAdmin) {
                spUserNavButtons.Children.Clear();
            } else {
                spAdminNavButtons.Children.Clear();
            }
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e) {

        }
    }
}
