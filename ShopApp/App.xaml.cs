using ShopApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShopApp;

public partial class App : Application
{
    public static Product SelectedProduct { get; set; }
    public static ObservableCollection<CartItem> Cart { get; set; } = new ObservableCollection<CartItem>();

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}