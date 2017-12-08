using APROTestApp.DAL.EF;
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

namespace APROTestAPP.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationViewModel _view;

        public MainWindow()
        {
            InitializeComponent();
            this._view = new ApplicationViewModel();
            DataContext = this._view;

        }

        private void OnNextClicked(object sender, RoutedEventArgs e)
        {
            this._view.MoveToNextPage();
        }

        private void OnPreviousClicked(object sender, RoutedEventArgs e)
        {
            this._view.MoveToPreviousPage();
        }

        private void RadioButtonCategories_Checked(object sender, RoutedEventArgs e)
        {
            //does not invoke collectionChanged bug
            var name = ((Wrapper)((RadioButton)sender).DataContext).Name;
            _view.Categories.Where(cat => cat.IsChecked == false).FirstOrDefault().IsChecked = false;
            _view.Categories.Where(cat => cat.Name == name).FirstOrDefault().IsChecked = true;
        }

        private void RadioButtonProducers_Checked(object sender, RoutedEventArgs e)
        {
            //does not invoke collectionChanged bug
            var name = ((Wrapper)((RadioButton)sender).DataContext).Name;
            _view.Producers.Where(cat => cat.IsChecked == false).FirstOrDefault().IsChecked = false;
            _view.Producers.Where(cat => cat.Name == name).FirstOrDefault().IsChecked = true;
        }
    }
}
