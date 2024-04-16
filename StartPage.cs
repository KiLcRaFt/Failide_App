using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

namespace Failide_App;

public class StartPage : ContentPage
{
    public StartPage()
    {
        Button synad = new Button
        {
            Text = "Sõnad",
        };
        synad.Clicked += Synad_Clicked;
        VerticalStackLayout vst = new VerticalStackLayout
        {
            Children = { synad }
        };

        Content = vst;
    }

    private async void Synad_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}
