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

    public PersonViewModel() : this(new Person("Sample", "Text"))
    {
    }

    public PersonViewModel(Person person)
    {
        this.person = person;

        this.DeleteCommand = new RelayCommand(() => { });
    }

    public string Name => this.person.Name;

    public ICommand DeleteCommand { get; set; }

    internal Person Model => new(this.person);
}