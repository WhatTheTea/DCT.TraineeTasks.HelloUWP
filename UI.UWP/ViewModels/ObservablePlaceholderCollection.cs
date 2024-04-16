// <copyright file = "ObservablePlaceholderCollection.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
/// <summary>
/// Behaved mostly like ObservableCollection, but last element is a placeholderFactory for a new one
/// </summary>
/// <typeparam name="T"><inheritdoc/></typeparam>
/// <param name="placeholderFactory">Factory method to return a new placeholder</param>
internal class ObservablePlaceholderCollection<T>(Func<T> placeholderFactory) : ObservableCollection<T>
    where T : BindableBase
{
    /// <inheritdoc cref="Collection{T}.Remove" />
    public new bool Remove(T item)
    {
        return this.Last()!.Equals(item) && base.Remove(item);
    }

    /// <inheritdoc cref="Collection{T}.Clear" />
    public new void Clear()
    {
        base.Clear();
        base.Add(placeholderFactory());
    }

    /// <inheritdoc cref="Collection{T}.Add" />
    public new void Add(T item)
    {
        base.Add(item);
        T last = this.Last();
        last.PropertyChanged += this.OnPlaceholderEdit;
    }

    private void OnPlaceholderEdit(object sender, PropertyChangedEventArgs _)
    {
        PersonViewModel person = sender as PersonViewModel ??
                                 throw new ArgumentException($"sender is not {typeof(T)}", nameof(sender));
        person.PropertyChanged -= this.OnPlaceholderEdit;
        this.Add(placeholderFactory());
    }

    
}
