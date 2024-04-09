using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiAppMVVM.Models;
using MauiAppMVVM.controller;
using MauiAppMVVM.Views; // Asegúrate de importar correctamente el espacio de nombres de tu controlador

namespace MauiAppMVVM.ViewModels
{
    public class ListProductsViewModels     : BaseViewModel
    {
        private ObservableCollection<Productos> _products;
        private readonly ControllerFirebase _firebaseController; // Cambia el tipo del campo _productController

        public ObservableCollection<Productos> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }

        private Productos _selectedProduct;

        public Productos SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }

        }

        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand GoEdit { private set; get; }

        public INavigation Navigation { get; set; }

        public ListProductsViewModels(INavigation navigation)
        {
            Navigation = navigation;
            GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));
            GoEdit= new Command<Type>(async (pageType) => await edi(pageType));

            Products = new ObservableCollection<Productos>();

            _firebaseController = new ControllerFirebase(); // Inicializa _firebaseController en lugar de _productController

            LoadProducts();
        }

        public ListProductsViewModels()
        {
        }

        private async Task LoadProducts()
        {
            var productList = await _firebaseController.GetListProductsFromFirebase(); // Llama al método correspondiente de ControllerFirebase
            Products.Clear();
            foreach (var product in productList)
            {
                Products.Add(product);
            }
        }

        async Task GoToDetails(Type pageType)
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

        async Task edi(Type pageType)
        {
            if (SelectedProduct != null)
            {
                var page = (Page)Activator.CreateInstance(typeof(PageEdit));

                page.BindingContext = new EditarView()
                {
                    ID = SelectedProduct.id,
                    Nombre = SelectedProduct.Nombre, // Ajusta la propiedad Nombre según el modelo Productos
                    Precio = SelectedProduct.Precio,
                    Foto = SelectedProduct.Foto,
                };
                await Navigation.PushAsync(page);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No ha seleccionado un elemento", "Ok");
            }
            
        }
    }
}
