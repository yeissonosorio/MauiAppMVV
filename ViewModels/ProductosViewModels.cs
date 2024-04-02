
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppMVVM.ViewModels
{
    public class ProductosViewModels : BaseViewModel
    {
        FileResult photo;
        private String _nombre;
        private double _precio;
        private String _foto;
        private String _base64;

        public String Nombre 
        {
            get { return _nombre; }
            set { _nombre = value;  OnPropertyChanged(); }
        }

        public double Precio
        {
            get { return _precio; }
            set { _precio = value; OnPropertyChanged(); }
        }

        public String Foto
        {
            get { return _foto; }
            set { _foto = value; OnPropertyChanged(); }
        }

        public ProductosViewModels() 
        {
            CleanCommand = new Command(Cleaner);
            CreateCommand = new Command(async () => await CreateData());
            TomarfotoCommand = new Command(async () => await tomarfoto());
        }

        public ICommand CleanCommand {  get; private set; }
        public ICommand CreateCommand {  get; private set; }
        public ICommand ReadCommand {  get; private set; }
        public ICommand UpdateCommand {  get; private set; }
        public ICommand DeleteCommand {  get; private set; }
        public ICommand TomarfotoCommand {  get; private set; }


        void Cleaner()
        {
            Nombre = string.Empty;
            Precio = 0;
            Foto   = string.Empty;
        }


        // CRUD
        async Task CreateData()
        {
            bool ver= false;
            if (Nombre != "" && Precio > 0 && _base64 != null)
            {
                var product = new Models.Productsmodel
                {
                    nombre = Nombre,
                    Precio = Precio,
                    foto = _base64
                };

                // Llamar al controlador para almacenar el producto en la base de datos
                if (await App.pro.StoreSitios(product) > 0)
                {
                    Cleaner();
                    await App.Current.MainPage.Navigation.PushAsync(new Views.PageListProductos());
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error","LLene todos los datos ","Ok");
            }
        }

        async Task tomarfoto()
        {
            photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                string path = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using Stream sourcephoto = await photo.OpenReadAsync();
                using FileStream Stringlocal = File.OpenWrite(path);

                await sourcephoto.CopyToAsync(Stringlocal);
                GetImage64();

            }
        }

        public string GetImage64()
        {
            if (photo != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = photo.OpenReadAsync().Result;
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();
                    String Base64 = Convert.ToBase64String(data);
                    _base64= Base64;
                    return Base64;
                }
            }
            return null;
        }

    }
}
