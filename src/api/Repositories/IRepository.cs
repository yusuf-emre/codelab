using System;
using System.Collections.Generic;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories;

public interface IRepository<T> : IDisposable
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Insert(T item);
    void Delete(int id);
    void Update(T item);
    void Save();
    void LoopChildren(Insurance insurance, int sum, int[] allCombinedValues, int i, int maxDepth);
    // int [] Top(int maxCount, int maxDepth);
}