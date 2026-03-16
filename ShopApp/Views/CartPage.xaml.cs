using ShopApp.Models;
using System.Linq;

namespace ShopApp.Views;

public partial class CartPage : ContentPage
{
    public CartPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        cartCollection.ItemsSource = App.Cart;
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        var total = App.Cart.Sum(item => item.Total);
        totalLabel.Text = $"Общая сумма: {total:C}";
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        if (App.Cart.Count == 0)
        {
            await DisplayAlert("Корзина пуста", "Добавьте товары в корзину", "OK");
            return;
        }
        await DisplayAlert("Заказ оформлен", "Спасибо за покупку!", "OK");
        App.Cart.Clear();
        UpdateTotal();
        cartCollection.ItemsSource = null;
    }
}