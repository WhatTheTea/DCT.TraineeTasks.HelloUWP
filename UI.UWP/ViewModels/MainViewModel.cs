// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Services;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class MainViewModel : BindableBase
{
    public static MainViewModel Instance { get; } = new();
    private readonly IFileService<IEnumerable<Person>> peopleFileService = new JsonFileService<IEnumerable<Person>>();

    public ObservableCollection<Person> People
    {
        get => this.people;
        private set => this.SetAndRaise(ref this.people, value);
    }


    public ICommand SaveStateCommand { get; }
    public ICommand LoadStateCommand { get; }
    public ICommand RemovePersonCommand { get; }
    public ICommand AddPlaceholderCommand { get; }

    public Person? SelectedPerson
    {
        get => this.selectedPerson;
        set => this.SetAndRaise(ref this.selectedPerson, value);
    }

    private ObservableCollection<Person> people = [];
    private Person? selectedPerson;

    private MainViewModel()
    {
        // TODO: AsyncCommands
        this.SaveStateCommand = new RelayCommand(async () => await this.SaveState());
        this.LoadStateCommand = new RelayCommand(async () => await this.LoadState());
        this.RemovePersonCommand = new RelayCommand(() =>
        {
            if (this.SelectedPerson is not null)
            {
                this.People.Remove(this.SelectedPerson);
            }
        });

        this.AddPlaceholderCommand = new RelayCommand(() =>
        {
            this.People.Add(new("Sample", "Text"));
        });

        this.LoadStateCommand.Execute(null);
    }


    private async Task LoadState()
    {
        try
        {
            IEnumerable<Person> models = await this.peopleFileService
                .LoadAsync(nameof(this.People));

            this.People.Clear();
            this.People = new ObservableCollection<Person>(models);
        }
        catch (Exception ex)
            when (ex is JsonException or FileNotFoundException)
        {
            Trace.WriteLine(ex);
        }
    }

    private async Task SaveState()
    {
        await this.peopleFileService.SaveAsync(this.People,
            nameof(this.People));
    }
}