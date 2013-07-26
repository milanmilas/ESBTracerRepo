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

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    /// <summary>
    /// Interaction logic for ToolbarView.xaml
    /// </summary>
    public partial class ToolbarA : UserControl, IToolbarAView
    {
        public ToolbarA()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return (IToolbarAViewViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
