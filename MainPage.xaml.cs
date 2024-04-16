using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace Failide_App;

public partial class MainPage : ContentPage
{
	int count = 0;
    List<Product> products = new List<Product>
        {
            new Product {Name= "Name1", Description = "Dis1", Image = "dotnet_Bot.svg"}
        };
    List<Product> products_ru = new List<Product>
        {
            new Product {Name= "имя1", Description = "Дис1", Image = "dotnet_Bot.svg"}
        };
    public MainPage()
	{
		//InitializeComponent();
		CarouselView carouselView = new CarouselView
		{
			VerticalOptions = LayoutOptions.Center,
		};

        OnAppearing();
        carouselView.ItemsSource = products;

        carouselView.ItemTemplate = new DataTemplate(() =>
		{
			Label header = new Label
			{
				FontAttributes = FontAttributes.Bold,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = new Color(0, 0, 0),
				Margin = 10,
            };
			header.SetBinding(Label.TextProperty, "Name");

			Image image = new Image { WidthRequest = 150, HeightRequest = 150 };
			image.SetBinding(Image.SourceProperty, "Image");

			Label description = new Label { HorizontalTextAlignment= TextAlignment.Center, TextColor = new Color(0, 0, 0), Margin=10 };
			description.SetBinding(Label.TextProperty, "Description");

			StackLayout st = new StackLayout() {header, image, description };
			st.WidthRequest = 300;
			st.HeightRequest = 300;
			st.BackgroundColor = Colors.WhiteSmoke;
			Frame frame = new Frame();
			frame.Content= st;
			return frame;


		});

        Content = carouselView;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMauiAsset();
        ((CarouselView)Content).ItemsSource = products;
    }

        private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    private async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("Synad_eesti.txt");
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            string line = await reader.ReadLineAsync();
            string[] parts = line.Split(',');
            if (parts.Length >= 4)
            {
                products.Add(new Product
                {
                    Name = parts[0],
                    Description = parts[1],
                    Image = "dotnet_bot.svg"
                });
                products_ru.Add(new Product
                {
                    Name = parts[2],
                    Description = parts[3],
                    Image = "dotnet_bot.svg"
                });

            }
        }
    }

}

