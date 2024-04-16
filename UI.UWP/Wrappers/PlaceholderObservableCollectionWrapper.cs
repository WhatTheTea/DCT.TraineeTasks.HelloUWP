// <copyright file = "PlaceholderObservableCollectionWrapper.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Wrappers;

/// <summary>
/// Behaved mostly like ObservableCollection, but last element is a placeholder for a new one
/// </summary>
/// <typeparam name="T"><inheritdoc/></typeparam>
public class PlaceholderObservableCollectionWrapper<T> : ObservableCollection<T>
    where T : BindableBase
{
    private readonly Func<T> placeholderFactory;

    /// <summary>
    /// Behaved mostly like ObservableCollection, but last element is a placeholder for a new one
    /// </summary>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    /// <param name="placeholderFactory">Factory method to return a new placeholder</param>
    public PlaceholderObservableCollectionWrapper(Func<T> placeholderFactory)
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

    /// <summary>
    /// Adds new item before placeholder
    /// </summary>
    /// <param name="item">Item to add</param>
    public new void Add(T item)
    {
        base.Add(this.Last());
        base[this.Count - 2] = item;
    }

    public void AddMany(IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            this.Add(item);
        }
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
