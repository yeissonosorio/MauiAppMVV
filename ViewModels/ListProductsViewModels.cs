using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiAppMVVM.Models;
using MauiAppMVVM.controller;

namespace MauiAppMVVM.ViewModels
{
    public class ListProductsViewModels : BaseViewModel
    {
        private ObservableCollection<Productsmodel> _products;
        private readonly controladorproducto _productController;

        public ObservableCollection<Productsmodel> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }

        private Productsmodel _selectedProduct;

        public Productsmodel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ICommand GoToDetailsCommand { private set; get; }

        public INavigation Navigation { get; set; }

        public ListProductsViewModels(INavigation navigation)
        {
            Navigation = navigation;
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));

            Products = new ObservableCollection<Productsmodel>();

            _productController = new controladorproducto();

            LoadProducts();
        }

        public ListProductsViewModels()
        {
        }

        private async Task LoadProducts()
        {
            var productList = await _productController.GetListPersons();
            Products.Clear();
            foreach (var product in productList)
            {
                Products.Add(product);
            }
        }

        async Task GoToDetails(Type pageType)
        {
            if (SelectedProduct != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new ProductosViewModels()
                {
                    Nombre = SelectedProduct.nombre,
                    Precio = SelectedProduct.Precio,
                    Foto = SelectedProduct.foto,
                };

                await Navigation.PushAsync(page);
            }
            else
            {
                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = new ProductosViewModels()
                {
                    Nombre = "",
                    Precio = 0,
                    Foto = "",
                };

                await Navigation.PushAsync(page);
            }
        }

    }
}
