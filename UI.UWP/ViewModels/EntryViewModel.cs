// <copyright file = "EntryViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class EntryViewModel(Entry entry) : BindableBase
{
    public ICommand DeleteCommand { get; set; } = new RelayCommand(() => { });

    public string Text
    {
        get => entry.Text;
        set => this.SetAndRaise(value, entry, x => x.Text);
    }

    public EntryViewModel() : this(new Entry())
    {
    }


    internal Entry Model => entry;
}
