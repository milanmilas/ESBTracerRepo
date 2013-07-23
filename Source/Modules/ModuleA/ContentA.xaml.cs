﻿using ESBInfrastructureLibrary;
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
    /// <summary>
    /// Interaction logic for ContentView.xaml
    /// </summary>
    public partial class ContentA : UserControl, IContentAView
    {
        public ContentA()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return (IContentAViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
