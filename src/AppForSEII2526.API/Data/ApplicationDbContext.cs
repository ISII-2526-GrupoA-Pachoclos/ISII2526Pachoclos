﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Herramienta> Herramienta { get; set; }
    public DbSet<fabricante> fabricante { get; set; }
    public DbSet<ComprarItem> ComprarItem { get; set; }
    public DbSet<OfertaItem> OfertaItem { get; set; }
    public DbSet<Compra> Compra { get; set; }
    public DbSet<Oferta> Oferta { get; set; }
    public DbSet<ReparacionItem> ReparacionItem { get; set; }
    public DbSet<Reparacion> Reparacion { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

}