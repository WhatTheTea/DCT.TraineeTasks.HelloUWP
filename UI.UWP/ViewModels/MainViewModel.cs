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
using DCT.TraineeTasks.HelloUWP.UI.UWP.Wrappers;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class MainViewModel : BindableBase
{
    public static MainViewModel Instance { get; } = new();
    private readonly IFileService<IEnumerable<Person>> peopleFileService = new JsonFileService<IEnumerable<Person>>();

    private PlaceholderObservableCollectionWrapper<PersonViewModel> people;

    private MainViewModel()
    {
        this.people = new PlaceholderObservableCollectionWrapper<PersonViewModel>(() =>
        {
            var person = new PersonViewModel(new Person("[Add new]", string.Empty));
            person.DeleteCommand = new RelayCommand(() => this.People.Remove(person));
            return person;
        });
        // TODO: AsyncCommands
        this.SaveStateCommand = new RelayCommand(async () => await this.SaveState());
        this.LoadStateCommand = new RelayCommand(async () => await this.LoadState());

        this.LoadStateCommand.Execute(null);
    }

    public PlaceholderObservableCollectionWrapper<PersonViewModel> People
    {
        get => this.people;
        private set => this.SetAndRaise(ref this.people, value);
    }
    public ICommand SaveStateCommand { get; }
    public ICommand LoadStateCommand { get; }

    private async Task LoadState()
    {
        try
        {
            IEnumerable<Person> models = await this.peopleFileService
                .LoadAsync(nameof(this.People));
            IEnumerable<PersonViewModel> viewModels = models.Select(x =>
            {
                var person = new PersonViewModel(x);
                person.DeleteCommand = new RelayCommand(() => this.People.Remove(person));
                return person;
            });

            this.People.Clear();
            this.People.AddMany(viewModels);
        }
        catch (Exception ex)
            when (ex is JsonException or FileNotFoundException)
        {
            Trace.WriteLine(ex);
        }
    }

    private async Task SaveState()
    {
        IEnumerable<Person> data = this.People
            .GetReal()
            .Select(x => x.Model);

        await this.peopleFileService.SaveAsync(data,
            nameof(this.People));
    }
}