// <copyright file = "MainPage.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Views.Pages;

/// <summary>
///     An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        Application.Current.Suspending += (_, _) => this.ViewModel.SaveStateCommand.Execute(null);
    }

    public readonly MainViewModel ViewModel = MainViewModel.GetInstance();
}
