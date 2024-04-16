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
/// Behaved mostly like ObservableCollection, but last element is a placeholder for a new one
/// </summary>
/// <typeparam name="T"><inheritdoc/></typeparam>
public class ObservablePlaceholderCollection<T> : ObservableCollection<T>
    where T : BindableBase
{
    private readonly Func<T> placeholderFactory;

    /// <summary>
    /// Behaved mostly like ObservableCollection, but last element is a placeholder for a new one
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    /// <param name="placeholderFactory">Factory method to return a new placeholder</param>
    public ObservablePlaceholderCollection(Func<T> placeholderFactory)
    {
        this.placeholderFactory = placeholderFactory;
        this.AddNewPlaceholder();
    }

    /// <inheritdoc cref="Collection{T}.Remove" />
    public new bool Remove(T item)
    {
        return !this.Last().Equals(item) && base.Remove(item);
    }

    /// <inheritdoc cref="Collection{T}.Clear" />
    public new void Clear()
    {
        base.Clear();
        this.AddNewPlaceholder();
    }

    /// <inheritdoc cref="Collection{T}.Add" />
    public new void Add(T item)
    {
        base.Add(this.Last());
        base[this.Count - 2] = item;
    }

    private void OnPlaceholderEdit(object sender, PropertyChangedEventArgs _)
    {
        T item = sender as T ?? throw new ArgumentException($"sender is not {typeof(T)}", nameof(sender));
        item.PropertyChanged -= this.OnPlaceholderEdit;
        this.AddNewPlaceholder();
    }

    private void AddNewPlaceholder()
    {
        T newPlaceholder = this.placeholderFactory();
        base.Add(newPlaceholder);
        newPlaceholder.PropertyChanged += this.OnPlaceholderEdit;
    }
}
