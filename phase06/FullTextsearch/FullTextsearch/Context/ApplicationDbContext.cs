using FullTextsearch.Model;
using Microsoft.EntityFrameworkCore;

namespace FullTextsearch.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=localhost;Database=FullTextSearch;Username=postgres;Password=Saleh@1283");
    // }

    public DbSet<InvertedIndexRecord> InvertedIndexMap { get; set; }
}