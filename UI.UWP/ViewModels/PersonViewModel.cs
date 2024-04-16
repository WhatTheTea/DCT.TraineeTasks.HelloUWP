// <copyright file = "PersonViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Wrappers;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class PersonViewModel : BindableBase
{
    private readonly Person person;
    private EntryViewModel? selectedEntry;
    private EntryViewModel EntryPlaceholder
    {
        get
        {
            var entry = new EntryViewModel(new Entry { Text = "[Add new]" });
            entry.DeleteCommand = new RelayCommand(() => this.Entries.Remove(entry));
            return entry;
        }
    }

    public string FirstName
    {
        get => this.person.FirstName;
        set => this.SetAndRaise(value, this.person, x => x.FirstName,
            propertyNames: nameof(this.Name));
    }

    public string LastName
    {
        get => this.person.LastName;
        set => this.SetAndRaise(value, this.person, x => x.LastName,
            propertyNames: nameof(this.Name));
    }

    public PlaceholderObservableCollectionWrapper<EntryViewModel> Entries { get; }

    public EntryViewModel? SelectedEntry
    {
        get => this.selectedEntry;
        set => this.SetAndRaise(ref this.selectedEntry, value);
    }

    public PersonViewModel() : this(new Person("Sample", "Text"))
    {
    }

    public PersonViewModel(Person person)
    {
        this.person = person;
        this.Entries = new PlaceholderObservableCollectionWrapper<EntryViewModel>(() => this.EntryPlaceholder);
        this.Entries.AddMany(person.Entries.Select(x =>
        {
            var entry = new EntryViewModel(x);
            entry.DeleteCommand = new RelayCommand(() => this.Entries.Remove(entry));
            return entry;
        }));

        this.DeleteCommand = new RelayCommand(() => { });
    }

    public string Name => this.person.Name;

    public ICommand DeleteCommand { get; set; }

    internal Person Model
    {
        get
        {
            this.person.Entries = this.Entries.Select(x => x.Model).ToArray();
            return new(this.person);
        }
    }
}