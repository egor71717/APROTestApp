using APROTestApp.DAL.EF;
using APROTestApp.DAL.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Windows;

namespace APROTestAPP.UI.WPF
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private const String _empty = "--Empty--";
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ApplicationDBContext _dbContext;
        private Int32 _itemsPerPage;
        private Int32 _currentPage;
        private Int32 _pageCount;
        private Product _selectedProduct;
        
        public Int32 ItemsPerPage
        {
            get { return this._itemsPerPage; }
            set
            {
                this._itemsPerPage = value;
                this.OnPropertyChanged("ItemsPerPage");
            }
        }
        public Int32 CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                this._currentPage = value;
                this.OnPropertyChanged("CurrentPage");
                this.OnPropertyChanged("IsNotFirstPage");
                this.OnPropertyChanged("IsNotLastPage");
            }
        }
        public Int32 AllProductsCount
        {
            get
            {
                var baseQuery = this._dbContext.Products.AsQueryable();
                var filtered = FilterByCategory(FilterByProducer(baseQuery));
                return filtered.Count();
            }
        }
        public Int32 PageCount
        {
            get { return this._pageCount; }
            set
            {
                this._pageCount = value;
                this.OnPropertyChanged("PageCount");
            }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public Boolean IsNotFirstPage
        {
            get { return _currentPage != 0; }
        }
        public Boolean IsNotLastPage
        {
            get { return _currentPage != _pageCount - 1 && _pageCount != 0; }
        }

        public String ProducersFilter { get; set; }
        public String CategoriesFilter { get; set; }
        public ObservableCollection<Product> PageProducts { get; set; }
        public ObservableCollection<Wrapper> Producers { get; set; }
        public ObservableCollection<Wrapper> Categories { get; set; }

        public ApplicationViewModel()
        {
            this.CategoriesFilter = _empty;
            this.ProducersFilter = _empty;
            this.CurrentPage = 0;
            this.ItemsPerPage = 4; //hardcoded value
            this._dbContext = ApplicationDBContext.GetInstance("DefaultConnection");
            this.PageProducts = new ObservableCollection<Product>(this.GetData());
            this.PageProducts.CollectionChanged += (obj, sender) => 
            {
                this.SelectedProduct = this.PageProducts.FirstOrDefault();
            };
            this.Producers = new ObservableCollection<Wrapper>(this._dbContext.Producers
                .Select(prod => new Wrapper() { Name = prod.Name, IsChecked = false }));
            this.Producers.Add(new Wrapper() { Name = _empty, IsChecked = true });
            this.Producers.CollectionChanged += OnProducersCollectionChanged;
            this.Categories = new ObservableCollection<Wrapper>(this._dbContext.Categories
                .Select(cat => new Wrapper() { Name = cat.Name, IsChecked = false }));
            this.Categories.Add(new Wrapper() { Name = _empty, IsChecked = true });
            this.Categories.CollectionChanged += OnCategoriesCollectionChanged;
            this.PageCount = CountPages();
            this.SelectedProduct = PageProducts.FirstOrDefault();
        }

        public Int32 CountPages()
        {
            var productscount = this.AllProductsCount;
            Int32 additionalPage = 0;
            if (productscount % this.ItemsPerPage > 0)
                additionalPage = 1;
            return (productscount / this.ItemsPerPage) + additionalPage;
        }

        public IEnumerable<Product> GetData()
        {
            var baseQuery = this._dbContext.Products.AsQueryable();
            var filtered = FilterByCategory(FilterByProducer(baseQuery));
            var pageItems = filtered
                .OrderBy(prod => prod.Id)
                .Skip(this._currentPage * _itemsPerPage)
                .Take(this._itemsPerPage)
                .ToList();
            return pageItems;
        }

        public void OnCategoriesCollectionChanged(Object sender, NotifyCollectionChangedEventArgs args)
        {
            var changedItem = args.NewItems.Cast<Wrapper>()
                                              .Where(item => item.IsChecked)
                                              .FirstOrDefault();
            this.CategoriesFilter = changedItem.Name;
            this.PageCount = CountPages();
            this.CurrentPage = 0;
            this.RefreshPageProducts();
        }

        public void OnProducersCollectionChanged(Object sender, NotifyCollectionChangedEventArgs args)
        {
            var changedItem = args.NewItems.Cast<Wrapper>()
                                              .Where(item => item.IsChecked)
                                              .FirstOrDefault();
            this.ProducersFilter = changedItem.Name;
            this.CurrentPage = 0;
            this.PageCount = CountPages();
            this.RefreshPageProducts();
        }

        public IQueryable<Product> FilterByCategory(IQueryable<Product> query)
        {
            if (this.CategoriesFilter == _empty)
                return query;
            return query.Where(prod => prod.Category.Name == this.CategoriesFilter);
        }

        public IQueryable<Product> FilterByProducer(IQueryable<Product> query)
        {
            if (this.ProducersFilter == _empty)
                return query;
            return query.Where(prod => prod.Pruducer.Name == this.ProducersFilter);
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void MoveToNextPage()
        {
            if (this.CurrentPage < this.PageCount)
            {
                this.CurrentPage += 1;
                this.RefreshPageProducts();
            }
            else
                throw new Exception("unexpexted _currentPage");
        }

        public void MoveToPreviousPage()
        {
            if (this.CurrentPage > 0)
            {
                this.CurrentPage -= 1;
                this.RefreshPageProducts();
            }   
            else throw new Exception("unexpexted _currentPage");
        }

        public void RefreshPageProducts()
        {
            this.PageProducts.Clear();
            foreach (var item in this.GetData())
                this.PageProducts.Add(item);
        }

    }
}
