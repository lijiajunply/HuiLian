﻿using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using HuiLianMedical.Share.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HuiLianMedical.Share;

public sealed class MedicalContext : DbContext
{
    public MedicalContext(DbContextOptions<MedicalContext> options)
        : base(options)
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Users = Set<UserModel>();
            Commodities = Set<CommodityModel>();
            TodoAid = Set<AidModel>();
            Categories = Set<CategoryModel>();
            DoneAid = Set<AidModel>();
        }
    }

    public DbSet<UserModel> Users { get; init; }
    public DbSet<CategoryModel> Categories { get; init; }
    public DbSet<CommodityModel> Commodities { get; init; }
    public DbSet<AidModel> TodoAid { get; init; }
    public DbSet<AidModel> DoneAid { get; init; }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MedicalContext>
{
    public MedicalContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MedicalContext>();
        optionsBuilder.UseSqlite("Data Source=Data.db");
        return new MedicalContext(optionsBuilder.Options);
    }
}

public static class DataTool
{
    public static string StringToHash(string s)
    {
        var data = Encoding.UTF8.GetBytes(s);
        var hash = MD5.HashData(data);
        var hashStringBuilder = new StringBuilder();
        foreach (var t in hash)
            hashStringBuilder.Append(t.ToString("x2"));
        return hashStringBuilder.ToString();
    }

    public static string GetProperties<T>(T t)
    {
        var builder = new StringBuilder();
        if (t == null) return builder.ToString();

        var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        if (properties.Length <= 0) return builder.ToString();

        foreach (var item in properties)
        {
            var name = item.Name;
            var value = item.GetValue(t, null);
            if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
            {
                builder.Append($"{name}:{value ?? "null"},");
            }
        }

        return builder.ToString();
    }
}

public abstract class DataModel
{
    public override string ToString()
        => DataTool.StringToHash($"{GetType()} : {DataTool.GetProperties(this)}");
}