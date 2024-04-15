﻿// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private static readonly MainViewModel Instance;
    private readonly IFileService<IEnumerable<Person>> peopleFileService = new JsonFileService<IEnumerable<Person>>();

    private ObservableCollection<PersonViewModel> people = [];

    static MainViewModel()
    {
        Instance = new MainViewModel();
    }

    protected MainViewModel()
    {
        this.AddPersonCommand = new RelayCommand(() => this.AddPerson(new PersonViewModel()));
        // TODO: AsyncCommands
        this.SaveStateCommand = new RelayCommand(async () => await this.SaveState());
        this.LoadStateCommand = new RelayCommand(async () => await this.LoadState());

        this.LoadStateCommand.Execute(null);
    }

    public static MainViewModel GetInstance() => Instance;

    public ObservableCollection<PersonViewModel> People
    {
        get => this.people;
        private set => this.SetAndRaise(ref this.people, value);
    }

    public ICommand AddPersonCommand { get; }
    public ICommand SaveStateCommand { get; }
    public ICommand LoadStateCommand { get; }

    private async Task LoadState()
    {
        try
        {
            IEnumerable<Person>? models = await this.peopleFileService
                .LoadAsync(nameof(this.People));
            IEnumerable<PersonViewModel> viewModels = models.Select(x => new PersonViewModel(x));
            this.People.Clear();
            foreach (PersonViewModel person in viewModels)
            {
                this.AddPerson(person);
            }
        }
        catch (Exception ex)
        when (ex is JsonException || ex is FileNotFoundException)
        {
            Debug.WriteLine(ex);
        }
    }

    private async Task SaveState() =>
        await this.peopleFileService.SaveAsync(
            this.People.Select(x => x.Model),
            nameof(this.People));

    private void AddPerson(PersonViewModel person)
    {
        person.DeleteCommand = new RelayCommand(() => this.People.Remove(person));
        this.People.Add(person);
    }
}
