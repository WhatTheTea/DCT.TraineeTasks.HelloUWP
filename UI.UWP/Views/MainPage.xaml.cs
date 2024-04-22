// <copyright file = "MainPage.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        Application.Current.Suspending += (_, _) => this.ViewModel.SaveStateCommand.Execute(null);
    }

    public readonly MainViewModel ViewModel = MainViewModel.Instance;

    private async void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.AddDialog.DataContext is Person person)
        {
            person.FirstName = string.Empty;
            person.LastName = string.Empty;
            await this.AddDialog.ShowAsync();
        }
    }

    private void FlyoutRemovePersonButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.FlyoutRemovePerson.Hide();
    }
}
