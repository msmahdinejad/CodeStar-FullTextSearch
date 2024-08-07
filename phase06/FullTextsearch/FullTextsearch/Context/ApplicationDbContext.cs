using FullTextsearch.Model;
using Microsoft.EntityFrameworkCore;

namespace FullTextsearch.Context;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=FullTextSearch;Username=postgres;Password=pass");
    }

    public DbSet<InvertedIndexRecord> InvertedIndexMap { get; set; }
}