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

        #region Database
        private void SetDataDirectory() {
            var dir = Directory.GetParent(Directory.GetCurrentDirectory());
            dir = Directory.GetParent(dir.ToString());
            dir = Directory.GetParent(dir.ToString());
            //
            AppDomain.CurrentDomain.SetData("DataDirectory", dir.ToString());
        }
        #endregion
    }
}
