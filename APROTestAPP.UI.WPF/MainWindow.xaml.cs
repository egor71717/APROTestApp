using APROTestApp.DAL.EF;
using APROTestApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
        private readonly ApplicationDBContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            this._view = new ApplicationViewModel();
            this._dbContext = ApplicationDBContext.GetInstance("DefaultConnection");
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems.Cast<Product>().FirstOrDefault();
            _view.SelectedProduct = item;
        }

        private void OnStartEdit(object sender, RoutedEventArgs e)
        {
            EditStackPanel.Visibility = Visibility.Visible;
            DetailStackPanel.Visibility = Visibility.Collapsed;
        }

        private void OnFinishEdit(object sender, RoutedEventArgs e)
        {
            EditStackPanel.Visibility = Visibility.Collapsed;
            DetailStackPanel.Visibility = Visibility.Visible;
        }

        private void OnUpdate(object sender, RoutedEventArgs e)
        {
            Product product = _dbContext.Products.Where(prod => prod.Id == _view.SelectedProduct.Id).FirstOrDefault();
            product.Name = TextBoxName.Text;
            product.Price = Double.Parse(TextBoxPrice.Text, CultureInfo.InvariantCulture);
            product.OnlinerURL = TextBoxUrl.Text;
            product.Description = TextBoxDescription.Text;
            Producer producer = _dbContext.Producers.Where(prod => prod.Name == TextBoxProducer.Text).FirstOrDefault();
            if(producer == null)
            {
                producer = new Producer() { Name = TextBoxProducer.Text };
                _dbContext.Producers.Add(producer);
            }
            product.Pruducer = producer;
            //Category category = _dbContext.Categories.Where(cat => cat.Name == TextBoxCategory.Text).FirstOrDefault();
            //if (category == null)
            //{
            //    category = new Producer() { Name = TextBoxategory.Text };
            //    _dbContext.Categories.Add(category);
            //}
            //product.Category = category;

            _dbContext.Entry<Product>(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
            _view.RefreshPageProducts();
            OnFinishEdit(null,null);
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            _dbContext.Products.Remove(_view.SelectedProduct);
            _dbContext.SaveChanges();
            _view.PageCount = _view.CountPages();
            //move to previous page if the page is empty
            _view.CurrentPage -= _view.PageCount == _view.CurrentPage ? 1 : 0;
            if (_view.CurrentPage < 0)
                _view.CurrentPage = 0;
            _view.RefreshPageProducts();
        }
    }
}
