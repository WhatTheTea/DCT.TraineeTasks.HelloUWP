﻿// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Services;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class MainViewModel : BindableBase
{
    //public static MainViewModel Instance { get; } = new();
    private readonly IFileService<ObservableCollection<Person>> peopleFileService = new JsonFileService<ObservableCollection<Person>>();

    public ObservableCollection<Person> People
    {
        get => this.people;
        private set => this.SetAndRaise(ref this.people, value);
    }


    public ICommand SaveStateCommand { get; }
    public ICommand LoadStateCommand { get; }
    public ICommand RemovePersonCommand { get; }
    public ICommand CreatePersonCommand { get; }

    private ObservableCollection<Person> people = [];

    public MainViewModel()
    {
        // TODO: AsyncCommands
        this.SaveStateCommand = new RelayCommand(this.SaveState);
        this.LoadStateCommand = new RelayCommand(this.LoadState);
        this.RemovePersonCommand = new RelayCommand<Person>(p =>
        {
            if (p is not null)
            {
                this.People.Remove(p);
            }
        });
        this.CreatePersonCommand = new RelayCommand<Person>(x => this.People.Add(new Person(x)));

        this.LoadStateCommand.Execute(null);
    }


    private void LoadState()
    {
        this.People = this.peopleFileService
            .Load(nameof(this.People));
    }

    private void SaveState()
    {
        this.peopleFileService.Save(this.People,
            nameof(this.People));
    }
}