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
using iTextSharp;

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for winMain.xaml
    /// </summary>
    /// 

    public partial class winMain : Window {
        public winMain() {
            Application.Current.MainWindow = this; // for later reference
            InitializeComponent();
            SetNavButtons(winLogin.loginAdmin);
            SetWindow(winLogin.loginAdmin);
            LoadMainUct(winLogin.loginAdmin);
        }


        #region CORE_VARIABLES
        private uctShowtimes uctShowtimes;
        private uctAdmin uctAdmin;
        private uctStat uctStat;
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
                    wpCurrentContent.Children.Add(uctShowtimes);
                } else if (!wpCurrentContent.Children.Contains(uctAdmin) && winLogin.loginAdmin) {
                    wpCurrentContent.Children.Clear();
                    wpCurrentContent.Children.Add(uctAdmin);
                }
            }
            private void btnMyTickets_Click(object sender, RoutedEventArgs e) {
                wpCurrentContent.Children.Clear();
            }

            private void btnAboutTheatre_Click(object sender, RoutedEventArgs e) {
                wpCurrentContent.Children.Clear();
            }

            private void btnSettings_Click(object sender, RoutedEventArgs e) {
                wpCurrentContent.Children.Clear();
            }
            private void btnStatistics_Click(object sender, RoutedEventArgs e) {
                wpCurrentContent.Children.Clear();
                uctStat = new uctStat();
                if (!wpCurrentContent.Children.Contains(uctStat) && winLogin.loginAdmin) {
                    wpCurrentContent.Children.Clear();
                    wpCurrentContent.Children.Add(uctStat);
                }
            }
            private void btnLogo_Click(object sender, RoutedEventArgs e) {
                if (winLogin.loginAdmin) {
                    winImportXmlToDB winImportXmlToDB = new winImportXmlToDB();
                    winImportXmlToDB.Show();
                }
                if (!winLogin.loginAdmin) {
                    CreatePDF createPDF = new CreatePDF();
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

        #region WINDOW_PREPARATION
        private void SetNavButtons(bool isAdmin) {
            if (isAdmin) {
                spUserNavButtons.Children.Clear();
            } else {
                spAdminNavButtons.Children.Clear();
            }
        }

        private void SetWindow(bool isAdmin) {
            if (isAdmin) {
                this.Height = 700;
                this.Width = 100;
                this.MinHeight = 700;
                this.MinWidth = 1000;
            }
        }
        private void LoadMainUct(bool isAdmin) {
            if (!isAdmin) {
                // Open showtimes on startup
                uctShowtimes = new uctShowtimes();
                wpCurrentContent.Children.Add(uctShowtimes);
            } else {
                uctAdmin = new uctAdmin();
                wpCurrentContent.Children.Add(uctAdmin);
            }
        }

        #endregion

       
    }
}
