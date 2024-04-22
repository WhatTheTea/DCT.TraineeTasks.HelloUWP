// <copyright file = "BindableBase.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

public class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!) =>
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void SetAndRaise<T>(ref T original, T value, [CallerMemberName] string propertyName = null!)
    {
        if (original is null || !original.Equals(value))
        {
            original = value;

            this.OnPropertyChanged(propertyName);
        }
    }
}
