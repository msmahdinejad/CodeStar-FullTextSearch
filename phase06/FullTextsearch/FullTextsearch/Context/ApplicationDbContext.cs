using FullTextsearch.Model;
using Microsoft.EntityFrameworkCore;

namespace FullTextsearch.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<InvertedIndexRecord> InvertedIndexMap { get; set; }
}