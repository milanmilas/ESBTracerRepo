using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using System.Windows;
    using System.Windows.Interactivity;

    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.ServiceLocation;
    using System.Windows.Input;
    using PrismDashboard;
    using System.ComponentModel;

    public class OpenPopupWindowAction : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            var popup = new ChildWindow(); //(ChildWindow)ServiceLocator.Current.GetInstance<IPopupDialogWindow>();
            popup.Owner =  PlacementTarget ?? (Window)ServiceLocator.Current.GetInstance<IShell>();

            popup.DialogResultCommand = PopupDailogResultCommand;
            popup.DataContext = SourceItem;
            popup.Show();
        }

        public Window PlacementTarget
        {
            get { return (Window)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register("PlacementTarget", typeof(Window), typeof(OpenPopupWindowAction), new PropertyMetadata(null));


        public ICommand PopupDailogResultCommand
        {
            get { return (ICommand)GetValue(PopupDailogResultCommandProperty); }
            set { SetValue(PopupDailogResultCommandProperty, value); }
        }

        public static readonly DependencyProperty PopupDailogResultCommandProperty =
            DependencyProperty.Register("PopupDailogResultCommand", typeof(ICommand), typeof(OpenPopupWindowAction), new PropertyMetadata(null));

        public static readonly DependencyProperty SourceItemProperty =
            DependencyProperty.Register("SourceItem", typeof(LogViewModel), typeof(OpenPopupWindowAction), new PropertyMetadata(default(LogViewModel)));

        public LogViewModel SourceItem
        {
            get
            {
                return (LogViewModel)GetValue(SourceItemProperty);
            }
            set
            {
                SetValue(SourceItemProperty, value);
            }
        }
    }

}
