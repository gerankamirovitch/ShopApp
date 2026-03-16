namespace ShopApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Регистрируем маршрут для страницы корзины
        Routing.RegisterRoute("CartPage", typeof(Views.CartPage));
    }
}