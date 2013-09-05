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

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window, IPopupDialogWindow
    {
        public ChildWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public new PopupDialogResult DialogResult
        {
            get;
            set;
        }

        public System.Windows.Input.ICommand DialogResultCommand
        {
            get;
            set;
        }
    }
}
