using ShopApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Views;

public partial class MainShopPage : ContentPage
{
    private List<Product> _products;

    public MainShopPage()
    {
        InitializeComponent();
        LoadProducts();
        ProductsCollection.ItemsSource = _products;
    }

    private void LoadProducts()
    {
        _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Смартфон Obeme Pro",
                Price = 999.99m,
                Description = "6.7-дюймовый экран, 256 ГБ, A16",
                ImageUrl = "phone_front.png",
                ImageUrls = new List<string> { "phone_front.png", "phone_back.png", "phone_side.png" },
                Stock = 15
            },
            new Product
            {
                Id = 2,
                Name = "Планшет QWE Book",
                Price = 1499.99m,
                Description = "16 ГБ RAM, 512 ГБ SSD, M2",
                ImageUrl = "laptop.png",
                ImageUrls = new List<string> { "laptop.png", "laptop_open.png", "laptop_side.png" },
                Stock = 7
            },
            new Product
            {
                Id = 3,
                Name = "Ноутбук ABC Pad",
                Price = 449.99m,
                Description = "10.9-дюймовый, 64 ГБ",
                ImageUrl = "tablet.png",
                ImageUrls = new List<string> { "tablet.png", "tablet_back.png", "tablet_stand.png" },
                Stock = 20
            },
            new Product
            {
                Id = 4,
                Name = "Умные часы Watch Fit",
                Price = 299.99m,
                Description = "GPS, пульсометр, 7 дней работы",
                ImageUrl = "watch.png",
                ImageUrls = new List<string> { "watch.png", "watch_band.png", "watch_face.png" },
                Stock = 30
            },
            new Product
            {
                Id = 5,
                Name = "Наушники AirSound",
                Price = 199.99m,
                Description = "Беспроводные, шумоподавление",
                ImageUrl = "headphones.png",
                ImageUrls = new List<string> { "headphones.png", "headphones_case.png", "headphones_side.png" },
                Stock = 25
            }
        };
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var filtered = _products.Where(p => p.Name.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
        ProductsCollection.ItemsSource = filtered;
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
        {
            App.SelectedProduct = selectedProduct;
            await Shell.Current.GoToAsync("//ProductDetailPage");
            ProductsCollection.SelectedItem = null;
        }
    }

    private void OnAddToCartInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var product = swipeItem.BindingContext as Product;
        App.Cart.Add(new CartItem { Product = product, Quantity = 1 });
        DisplayAlert("Корзина", $"Товар '{product.Name}' добавлен в корзину", "OK");
    }
}