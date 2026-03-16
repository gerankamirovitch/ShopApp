using System;

namespace ShopApp.Views;

public partial class ReverseNumberPage : ContentPage
{
    public ReverseNumberPage()
    {
        InitializeComponent();
    }

    private void OnReverseClicked(object sender, EventArgs e)
    {
        if (int.TryParse(NumberEntry.Text, out int number))
        {
            int reversed = ReverseNumber(number);
            ResultLabel.Text = $"Результат: {reversed}";
        }
        else
        {
            ResultLabel.Text = "Ошибка ввода!";
        }
    }

    private int ReverseNumber(int num)
    {
        int sign = num < 0 ? -1 : 1;
        int absNum = Math.Abs(num);
        int reversed = 0;
        while (absNum > 0)
        {
            reversed = reversed * 10 + absNum % 10;
            absNum /= 10;
        }
        return sign * reversed;
    }
}