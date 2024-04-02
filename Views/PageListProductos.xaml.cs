namespace MauiAppMVVM.Views;

public partial class PageListProductos : ContentPage
{
	public PageListProductos()
	{
		InitializeComponent();

        BindingContext = new ViewModels.ListProductsViewModels(Navigation);
    }
}