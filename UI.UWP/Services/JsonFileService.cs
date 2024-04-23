// <copyright file = "JsonFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

public class JsonFileService<T> : IFileService<T>
    where T : class, new()
{
    private static readonly JsonSerializerOptions Options = new() { IncludeFields = true };
    private readonly StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

    public async Task SaveAsync(T data, string path)
    {
        StorageFile storageFile = await this.storageFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting)
            .AsTask().ConfigureAwait(false);
        using var stream = await storageFile.OpenStreamForWriteAsync().ConfigureAwait(false);
        await JsonSerializer.SerializeAsync(stream, data, Options).ConfigureAwait(false);
    }

    public async Task<T> LoadAsync(string path)
    {
        var result = new T();
        if (await this.storageFolder.TryGetItemAsync(path).AsTask().ConfigureAwait(false) is not null)
        {
            StorageFile storageFile = await this.storageFolder.GetFileAsync(path).AsTask().ConfigureAwait(false);
            using var stream = await storageFile.OpenStreamForReadAsync().ConfigureAwait(false);
            result = await JsonSerializer.DeserializeAsync<T>(stream, Options).ConfigureAwait(false)
                   ?? throw new FormatException("Invalid JSON. File may be corrupted");
        }
        else
        {
            Trace.WriteLine("File was not found.");
        }
        return result;
    }

    public void Save(T data, string path) => this.SaveAsync(data, path)
        .ConfigureAwait(false).GetAwaiter().GetResult();

    public T Load(string path) => this.LoadAsync(path)
        .ConfigureAwait(false).GetAwaiter().GetResult();
}
