using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using System.ComponentModel;
//using Windows.Foundation.Metadata;

namespace Failide_App;

public partial class MainPage : ContentPage
{
    Label header_ru, header, description, description_ru;

    bool count = true;
    List<Product> products = new List<Product>
        {
            new Product {Name= "Name1", Description = "Dis1", Image = "dotnet_Bot.svg", Name_ru="Имя1", Description_ru="Дис1"}
        };
    List<Product> products_ru = new List<Product>
        {
            new Product {Name= "имя1", Description = "Дис1", Image = "dotnet_Bot.svg", Name_ru= "Name1", Description_ru = "Dis1"}
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

            //header_ru = new Label
            //{
            //    FontAttributes = FontAttributes.Bold,
            //    HorizontalTextAlignment = TextAlignment.Center,
            //    FontSize = 18,
            //    TextColor = new Color(0, 0, 0),
            //    Margin = 10,
            //    IsVisible= false
            //};

            //header_ru.SetBinding(Label.TextProperty, "Name_ru");


            //description_ru = new Label { HorizontalTextAlignment = TextAlignment.Center, TextColor = new Color(0, 0, 0), Margin = 10, IsVisible = false };
            //description_ru.SetBinding(Label.TextProperty, "Description_ru");

            //StackLayout st = new StackLayout() { header, header_ru, image, description, description_ru, btn };
            StackLayout st = new StackLayout() { header,image, description, btn };
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
        int index = carouselView.Position;

        if (count)
        {
            foreach (var product in products)
            {
                header.Text = product.Name_ru;
                description.Text = product.Description_ru;
            }

            count = !count;
        }
        else
        {
            foreach (var product in products)
            {
                header.Text = product.Name;
                description.Text = product.Description;
            }

            count = true;
        }

        //carouselView.Position = index;

        //CarouselView carouselView = (CarouselView)Content;
        //int index = carouselView.Position;
        //var product = products[index];
        //if (count)
        //{
        //    //carouselView.ItemsSource = products;
        //    header.Text = product.Name_ru;
        //    description.Text = product.Description_ru;

        //    //header.IsVisible = false;
        //    //header_ru.IsVisible = true;
        //    //description.IsVisible = false;
        //    //description_ru.IsVisible = true;


        //    //carouselView.Position = index;

        //    count = !count;
        //}
        //else
        //{
        //    //carouselView.ItemsSource = products;

        //    header.Text = product.Name;
        //    description.Text = product.Description;

        //    //header.IsVisible = true;
        //    //header_ru.IsVisible = false;
        //    //description.IsVisible = true;
        //    //description_ru.IsVisible = false;

        //    //carouselView.Position = index;

        //    count = true;
        //}

        ////CarouselView carouselView = (CarouselView)Content;

        ////if (count)
        ////{
        ////    carouselView.ItemsSource = products_ru; // Переключаемся на русский список продуктов
        ////    count = !count;
        ////}
        ////else
        ////{
        ////    carouselView.ItemsSource = products; // Переключаемся на английский список продуктов
        ////    count = true;
        ////}
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
            string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 4)
            {
                products.Add(new Product
                {
                    Name = parts[0],
                    Description = parts[1],
                    Image = "dotnet_bot.svg",
                    Name_ru = parts[2],
                    Description_ru = parts[3],
                });

                products_ru.Add(new Product
                {
                    Name = parts[2],
                    Description = parts[3],
                    Image = "dotnet_bot.svg",
                    Name_ru = parts[0],
                    Description_ru = parts[1],
                });
               
            }
        }
    }

}

