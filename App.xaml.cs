namespace MauiAppMVVM
{
    public partial class App : Application
    {
        static controller.controladorproducto controller;

        // Create the database connection as a singleton.
        public static controller.controladorproducto pro
        {
            get
            {
                if (controller == null)
                {
                    controller = new controller.controladorproducto();
                }
                return controller;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PageListProductos());
        }
    }
}
