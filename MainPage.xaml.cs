using Microsoft.Maui.Controls;
using System.ComponentModel;
//using Windows.Foundation.Metadata;

namespace Failide_App;

public partial class MainPage : ContentPage
{
    Label header_ru, header, description, description_ru;

    bool count = true;
    List<Product> products = new List<Product>
        {
            //new Product {Name= "Name1", Description = "Dis1", Image = "dotnet_Bot.svg"}
        };
    List<Product> products_ru = new List<Product>
        {
            //new Product {Name= "имя1", Description = "Дис1", Image = "dotnet_Bot.svg"}
        };
    public MainPage()
	{
		//InitializeComponent();
		CarouselView carouselView = new CarouselView
		{
			VerticalOptions = LayoutOptions.Center,
		};

        OnAppearing();

        carouselView.ItemTemplate = new DataTemplate(() =>
		{
            Button btn = new Button
            {
                Text= "Vahesta",
            };

            btn.Clicked += Btn_Clicked;

            header = new Label
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

            description = new Label { HorizontalTextAlignment = TextAlignment.Center, TextColor = new Color(0, 0, 0), Margin = 10 };
            description.SetBinding(Label.TextProperty, "Description");

            header_ru = new Label
            {
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                TextColor = new Color(0, 0, 0),
                Margin = 10,
                IsVisible= false
            };

            header.SetBinding(Label.TextProperty, "Name_ru");


            description_ru = new Label { HorizontalTextAlignment = TextAlignment.Center, TextColor = new Color(0, 0, 0), Margin = 10, IsVisible = false };
            description.SetBinding(Label.TextProperty, "Description_ru");

            StackLayout st = new StackLayout() { header, header_ru, image, description, description_ru, btn };
            st.WidthRequest = 300;
            st.HeightRequest = 300;
            st.BackgroundColor = Colors.WhiteSmoke;
            Frame frame = new Frame();
            frame.Content = st;
            return frame;


        });

        Content = carouselView;
	}

    private void Btn_Clicked(object sender, EventArgs e)
    {
        

        CarouselView carouselView = (CarouselView)Content;
        if (count)
        {
            //carouselView.ItemsSource = products;
            header.IsVisible= false;
            header_ru.IsVisible= true;

            description.IsVisible= false;
            description_ru.IsVisible= true;
            count = !count;
        }
        else
        {
            //carouselView.ItemsSource = products_ru;
            header.IsVisible = true;
            header_ru.IsVisible = false;

            description.IsVisible = true;
            description_ru.IsVisible = false;
            count = true;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMauiAsset();
        CarouselView carouselView = (CarouselView)Content;
        if (carouselView.ItemsSource == null)
        {
            carouselView.ItemsSource = products;
        }
    }

    private async Task LoadMauiAsset()
    {
        products.Clear();
        products_ru.Clear();
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
                    Image = "dotnet_bot.svg",
                    Name_ru= parts[2],
                    Description_ru= parts[3],
                });
                //products_ru.Add(new Product
                //{
                //    Name = parts[2],
                //    Description = parts[3],
                //    Image = "dotnet_bot.svg"
                //});

            }
            Console.WriteLine(products+ ", "+products_ru);
        }
    }

}

