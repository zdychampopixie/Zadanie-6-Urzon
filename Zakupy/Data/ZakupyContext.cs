using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zakupy;
using Zakupy.Models;

namespace Zakupy.Data;

public partial class ZakupyContext : DbContext
{
    public ZakupyContext()
    {
    }

    public ZakupyContext(DbContextOptions<ZakupyContext> options)
        : base(options)
    {
    }

    public DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
