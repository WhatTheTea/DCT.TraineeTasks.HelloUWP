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
    public ICommand RemovePerson { get; }

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
        this.SaveStateCommand = new RelayCommand(() => this.SaveState());
        this.LoadStateCommand = new RelayCommand(() => this.LoadState());
        this.RemovePerson = new RelayCommand(() =>
        {
            if (this.SelectedPerson is not null)
            {
                this.People.Remove(this.SelectedPerson);
            }
        });

        this.LoadStateCommand.Execute(null);
    }


    private void LoadState()
    {
        try
        {
            IEnumerable<Person> models = this.peopleFileService
                .Load(nameof(this.People));

            this.People.Clear();
            this.People = new ObservableCollection<Person>(models);
        }
        catch (Exception ex)
            when (ex is JsonException or FileNotFoundException)
        {
            Trace.WriteLine(ex);
        }
    }

    private void SaveState()
    {
        this.peopleFileService.Save(this.People,
            nameof(this.People));
    }
}