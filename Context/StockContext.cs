using GestionStock.Models;
using Microsoft.EntityFrameworkCore;

public class StockContext : DbContext
{
    // Add DbSet properties for each model to represent the corresponding tables in the database
    public DbSet<Produit> Produits { get; set; }
    public DbSet<Entree> Entrees { get; set; }
    public DbSet<Sortie> Sorties { get; set; }
    public DbSet<Stocker> Stockers { get; set; }
    public DbSet<Destocker> Destockers { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<StatisticsResult> StatisticsResults { get; set; }
    public DbSet<StockerWithDesignation> StockersWithDesignation { get; set; }
    public DbSet<MonthlyRankResult> MonthlyRankResults { get; set; }


    public StockContext(DbContextOptions<StockContext> options) : base(options)
    {
    }

    
    // Optionally override OnModelCreating to configure relationships or constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MonthlyRankResult>().HasNoKey();
        modelBuilder.Entity<StockerWithDesignation>().HasNoKey();
        modelBuilder.Entity<StatisticsResult>().HasNoKey();
        modelBuilder.Entity<Activity>().HasNoKey();
        // Example of configuring a one-to-many relationship between Entree and Stocker
        modelBuilder.Entity<Stocker>()
            .HasOne(s => s.Entree)
            .WithMany(e => e.Stockers)
            .HasForeignKey(s => s.NumBonEntre);

        // Example of configuring a one-to-many relationship between Sortie and Destocker
        modelBuilder.Entity<Destocker>()
            .HasOne(d => d.Sorties)
            .WithMany(s => s.Destockers)
            .HasForeignKey(d => d.NumBonSortie);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=inventory.db"); // Update the connection string as needed
    }
}

