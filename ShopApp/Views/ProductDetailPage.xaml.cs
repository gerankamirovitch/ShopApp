using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Views;

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var product = App.SelectedProduct;
        if (product != null)
        {
            // Генерация отзывов, если их нет
            if (product.Reviews == null || product.Reviews.Count == 0)
            {
                product.Reviews = GenerateReviews(45);
                product.AverageRating = product.Reviews.Average(r => r.Rating);
            }

            BindingContext = product;
            carousel.ItemsSource = product.ImageUrls ?? new List<string> { product.ImageUrl };

            // Настройка Entry количества
            if (product.Stock > 0)
            {
                quantityEntry.Text = "1";
                quantityEntry.IsEnabled = true;
            }
            else
            {
                quantityEntry.Text = "0";
                quantityEntry.IsEnabled = false;
            }
        }
    }

    private List<Review> GenerateReviews(int count)
    {
        var rand = new Random();
        var names = new[] { "Алексей", "Мария", "Иван", "Елена", "Дмитрий", "Ольга", "Сергей", "Анна", "Павел", "Наталья" };
        var comments = new[] {
            "Отличный товар!", "Нормально, но дорого", "Брал в подарок, понравилось",
            "Не соответствует описанию", "Супер! Рекомендую", "Доставка быстрая, качество отличное",
            "Есть небольшие минусы", "За такие деньги - хорошо", "Лучший в своем классе",
            "Пришёл битым, вернул", "Всё отлично, работает", "Хорошее соотношение цена/качество",
            "Мощный, доволен", "Немного греется, но в целом норм", "Фото супер"
        };

        var reviews = new List<Review>();
        for (int i = 0; i < count; i++)
        {
            double prob = rand.NextDouble();
            int rating;
            if (prob < 0.86)
                rating = 5;
            else if (prob < 0.97)
                rating = 4;
            else
                rating = 3;

            reviews.Add(new Review
            {
                UserName = names[rand.Next(names.Length)],
                Rating = rating,
                Comment = comments[rand.Next(comments.Length)],
                Date = DateTime.Now.AddDays(-rand.Next(1, 365))
            });
        }
        return reviews;
    }

    // Получение корректного количества из Entry
    private int GetValidQuantity()
    {
        if (BindingContext is Product product && product.Stock > 0)
        {
            if (int.TryParse(quantityEntry.Text, out int quantity) && quantity >= 1 && quantity <= product.Stock)
                return quantity;
        }
        return -1;
    }

    // Валидация ввода в реальном времени
    private void quantityEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is Product product && product.Stock > 0)
        {
            if (int.TryParse(e.NewTextValue, out int quantity))
            {
                if (quantity < 1)
                    quantityEntry.Text = "1";
                else if (quantity > product.Stock)
                    quantityEntry.Text = product.Stock.ToString();
            }
            else
            {
                // Если введено не число, ставим 1
                quantityEntry.Text = "1";
            }
        }
    }

    private void OnAddToCartClicked(object sender, EventArgs e)
    {
        var product = App.SelectedProduct;
        if (product == null) return;

        int quantity = GetValidQuantity();
        if (quantity == -1)
        {
            DisplayAlert("Ошибка", $"Введите число от 1 до {product.Stock}", "OK");
            return;
        }

        App.Cart.Add(new CartItem { Product = product, Quantity = quantity });
        DisplayAlert("Корзина", $"Товар '{product.Name}' в количестве {quantity} добавлен в корзину", "OK");
    }

    private async void OnBuyNowClicked(object sender, EventArgs e)
    {
        var product = App.SelectedProduct;
        if (product == null) return;

        int quantity = GetValidQuantity();
        if (quantity == -1)
        {
            await DisplayAlert("Ошибка", $"Введите число от 1 до {product.Stock}", "OK");
            return;
        }

        await DisplayAlert("Покупка", $"Вы хотите купить '{product.Name}' в количестве {quantity}. Сумма: {product.Price * quantity:C}", "OK");
    }

    private async void OnGoToCartClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CartPage");
    }
}