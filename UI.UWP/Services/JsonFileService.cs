// <copyright file = "JsonFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

// TODO: make it disposable to prevent Open/Close spam
public class JsonFileService<T> : IFileService<T>
    where T : class
{
    private static readonly JsonSerializerOptions Options = new() { IncludeFields = true };

    public async Task SaveAsync(T data, string path)
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        StorageFile storageFile = await storageFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting)
            .AsTask().ConfigureAwait(false);
        using var stream = await storageFile.OpenStreamForWriteAsync().ConfigureAwait(false);
        await JsonSerializer.SerializeAsync(stream, data, Options).ConfigureAwait(false);
    }

    public async Task<T> LoadAsync(string path)
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        StorageFile storageFile = await storageFolder.GetFileAsync(path).AsTask().ConfigureAwait(false);
        using var stream = await storageFile.OpenStreamForReadAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, Options).ConfigureAwait(false)
               ?? throw new FormatException("Invalid JSON");
    }

    public void Save(T data, string path) => this.SaveAsync(data, path)
        .ConfigureAwait(false).GetAwaiter().GetResult();

    public T Load(string path) => this.LoadAsync(path)
        .ConfigureAwait(false).GetAwaiter().GetResult();
}
