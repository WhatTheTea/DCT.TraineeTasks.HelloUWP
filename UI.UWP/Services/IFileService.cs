// <copyright file = "IFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

internal interface IFileService<T>
    where T : class
{
    public Task SaveAsync(T data, string path);
    public Task<T> LoadAsync(string path);
    public void Save(T data, string path);
    public T Load(string path);
}
