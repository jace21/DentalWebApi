﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;

  public partial class ProductionDatabaseEntities : DbContext
  {
    public ProductionDatabaseEntities()
        : base("name=ProductionDatabaseEntities")
    {
      Configuration.ProxyCreationEnabled = true;
      Configuration.LazyLoadingEnabled = true;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Dentist> Dentists { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Schedule> Schedules { get; set; }
  }
}
