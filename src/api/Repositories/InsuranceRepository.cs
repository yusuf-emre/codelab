using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class InsuranceRepository : IRepository<Insurance>, IDisposable
{
    private InsuranceDbContext context;

    public InsuranceRepository(InsuranceDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Insurance> GetAll()
    {
        return context.Insurances.ToList();
    }

    public Insurance? GetById(int insuranceId)
    {
        return context.Insurances.Find(insuranceId);
    }

    public void Insert(Insurance insurance)
    {
        context.Insurances.Add(insurance);
    }

    public void Delete(int insuranceId)
    {
        Insurance? insurance = context.Insurances.Find(insuranceId);
        if (insurance != null)
            context.Insurances.Remove(insurance);
    }

    public void Update(Insurance insurance)
    {
        context.Entry(insurance).State = EntityState.Modified;
    }

    public void Save()
    {
        context.SaveChanges();
    }

    // public int [] Top(int maxCount, int maxDepth)
    //     {
    //         var insuranceList = GetAll();
    //         var allCombinedValues = new int[insuranceList.Count()];
    //         var i = 0;

    //         foreach(var insurance in insuranceList)
    //         {
    //             insurance.Depth = 0;
    //             var sum = 0;
    //             sum += insurance.Value;

    //             if(insurance.Children.Count() == 0 || insurance.Depth == maxDepth)
    //             {
    //                 allCombinedValues[i] = sum;
    //                 i++;
    //                 continue;
    //             }

    //             LoopChildren(insurance, sum, allCombinedValues, i, maxDepth);

    //             i++;
    //         }

    //         var topValues = allCombinedValues.OrderByDescending(x => x).Take(maxCount).ToArray();
    //         return topValues;
    //     }

    public void LoopChildren(Insurance insurance, int sum, int[] allCombinedValues, int i, int maxDepth)
    {

        foreach (var child in insurance.Children)
        {
            child.Depth = insurance.Depth + 1;
            sum += child.Value;
            allCombinedValues[i] = sum;

            if (child.Children.Count() == 0 || child.Depth == maxDepth) continue;

            LoopChildren(child, sum, allCombinedValues, i, maxDepth);
        }
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}