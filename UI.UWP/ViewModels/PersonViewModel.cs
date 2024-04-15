// <copyright file = "PersonViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class PersonViewModel : BindableBase
{
    private readonly Person person;
    private EntryViewModel? selectedEntry;

    public PersonViewModel() : this(new Person("Sample", "Text"))
    {
    }

    public PersonViewModel(Person person)
    {
        this.person = person;
        this.Entries = new ObservableCollection<EntryViewModel>(person.Entries.Select(x => new EntryViewModel(x)));

        this.DeleteEntryCommand = new RelayCommand<EntryViewModel?>(this.RemoveEntry);
        this.AddEntryCommand = new RelayCommand(this.AddEntry);
        this.DeleteCommand = new RelayCommand(() => { });
    }

    public void AddEntry() => this.Entries.Add(new EntryViewModel { Text = "Sample text" });

    public void RemoveEntry(EntryViewModel? entry)
    {
        if (entry is not null)
        {
            this.Entries.Remove(entry);
        }
    }

    public string Name => this.person.Name;

    public ICommand DeleteCommand { get; set; }
    public ICommand DeleteEntryCommand { get; }
    public ICommand AddEntryCommand { get; }

    internal Person Model
    {
        get
        {
            this.person.Entries = this.Entries.Select(x => x.Model).ToArray();
            return new(this.person);
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

    public ObservableCollection<EntryViewModel> Entries { get; }

    public EntryViewModel? SelectedEntry
    {
        get => this.selectedEntry;
        set => this.SetAndRaise(ref this.selectedEntry, value);
    }
}
