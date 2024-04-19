// <copyright file = "Person.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Models;

public class Person(string firstName, string lastName) : BindableBase
{
    public Person(Person other) : this(other.FirstName, other.LastName)
    {}

    public Person() : this(string.Empty, string.Empty)
    {
    }

    public string Name => $"{this.FirstName} {this.LastName}";

    public string FirstName
    {
        get => firstName;
        set
        {
            this.SetAndRaise(ref firstName, value);
            this.OnPropertyChanged(nameof(this.Name));
        }
    }

    public string LastName
    {
        get => lastName;
        set
        {
            this.SetAndRaise(ref lastName, value);
            this.OnPropertyChanged(nameof(this.Name));
        }
    }
}
