using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopApp.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int _id; // Добавлено поле Id
        private string _name;
        private decimal _price;
        private string _description;
        private int _stock;
        private string _imageUrl;
        private List<string> _imageUrls;
        private List<Review> _reviews;
        private double _averageRating;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayPrice)); }
        }

        public string DisplayPrice => $"{Price:C}";

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public int Stock
        {
            get => _stock;
            set { _stock = value; OnPropertyChanged(); OnPropertyChanged(nameof(StockStatus)); }
        }

        public string StockStatus => Stock > 0 ? "В наличии" : "Нет в наличии";

        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = value; OnPropertyChanged(); }
        }

        public List<string> ImageUrls
        {
            get => _imageUrls;
            set { _imageUrls = value; OnPropertyChanged(); }
        }

        public List<Review> Reviews
        {
            get => _reviews;
            set { _reviews = value; OnPropertyChanged(); }
        }

        public double AverageRating
        {
            get => _averageRating;
            set { _averageRating = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}