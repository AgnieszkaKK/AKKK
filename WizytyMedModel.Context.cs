﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AKKK
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SzpitalMedDBEntities : DbContext
    {
        public SzpitalMedDBEntities()
            : base("name=SzpitalMedDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Lekarz> Lekarzs { get; set; }
        public virtual DbSet<Pacjent> Pacjents { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }
        public virtual DbSet<Wizyta> Wizytas { get; set; }
        public virtual DbSet<Widok_Wizyty> Widok_Wizyty { get; set; }
    }
}