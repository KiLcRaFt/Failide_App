﻿namespace Failide_App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
        //MainPage = new NavigationPage(new StartPage());
    }
}
