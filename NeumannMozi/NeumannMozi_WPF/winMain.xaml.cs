﻿using System;
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
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        private void btnSizeState_Click(object sender, RoutedEventArgs e) {
            if (this.WindowState == WindowState.Normal) {
                this.WindowState = WindowState.Maximized;
            } else {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }


    }
}