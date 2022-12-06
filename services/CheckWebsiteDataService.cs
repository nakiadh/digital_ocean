namespace services.CheckWebsiteDataService;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using services.CheckWebsite;

public class WebsiteDbContext : DbContext
{
    public WebsiteDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? dirPath = Assembly.GetExecutingAssembly().Location;
        dirPath = Path.GetDirectoryName(dirPath);

        string dbfilename = "ApiDB.sqlite";
        string connectionString = Path.GetFullPath(Path.Combine(dirPath!, dbfilename));

        optionsBuilder.UseSqlite($"DataSource={connectionString}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}