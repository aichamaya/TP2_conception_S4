using GestionCryo.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace GestionCryo.View
{
    public partial class ConnectView : UserControl
    {
        public ConnectView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ConnectUserViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
