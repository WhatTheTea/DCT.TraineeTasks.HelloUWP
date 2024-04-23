﻿// <copyright file = "MainPage.xaml.cs" company = "Digital Cloud Technologies">
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

    public MainViewModel ViewModel => this.DataContext as MainViewModel
                                      ?? throw new InvalidOperationException($"ViewModel is {this.DataContext.GetType()}. " +
                                                                             $"{nameof(MainViewModel)} expected.");

    private async void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.AddDialog.DataContext is Person person)
        {
            person.FirstName = string.Empty;
            person.LastName = string.Empty;
            await this.AddDialog.ShowAsync();
        }
    }
}
