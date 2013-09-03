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
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        private Microsoft.Practices.Prism.Interactivity.InteractionRequest.Confirmation confirmation;

        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        public ConfirmationWindow(Microsoft.Practices.Prism.Interactivity.InteractionRequest.Confirmation confirmation)
        {
            // TODO: Complete member initialization
            this.confirmation = confirmation;
        }
    }
}
