// <copyright file = "MainPage.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    public MainViewModel ViewModel => this.DataContext as MainViewModel
                                      ?? throw new InvalidOperationException($"ViewModel is {this.DataContext.GetType()}. " +
                                                                             $"{nameof(MainViewModel)} expected.");
    /// <summary>
    /// :c
    /// https://github.com/microsoft/XamlBehaviors/issues/112
    /// </summary>
    private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        => await this.AddDialog.ShowAsync();

}
