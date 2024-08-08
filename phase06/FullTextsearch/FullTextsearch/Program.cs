using FullTextsearch.Context;
using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Document.Formater;
using FullTextsearch.Factory.FolderFactory;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.Initialize;
using FullTextsearch.InvertedIndex;
using FullTextsearch.QueryManager.WordFinder;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.ResultList;
using FullTextsearch.SearchManager.SignedSearchManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IExtractor, AdvancedDocumentWordsExtractor>();
builder.Services.AddSingleton<ITextEditor, DocumentTextEditor>();
builder.Services.AddSingleton<ISignedSearchStrategy, SignedSearchStrategy>();
builder.Services.AddSingleton<IResultListMaker, IntersectResultListMaker>();
builder.Services.AddSingleton<IResultListMaker, UnionResultListMaker>();
builder.Services.AddSingleton<IWordFinder, PositiveWordFinder>();
builder.Services.AddSingleton<IWordFinder, NegativeWordFinder>();
builder.Services.AddSingleton<IWordFinder, UnsignedWordFinder>();
builder.Services.AddTransient<IInvertedIndex, InvertedIndexDbController>();
builder.Services.AddSingleton<ISearchStrategyFactory, SearchStrategyFactory>();
builder.Services.AddSingleton<ISearchController, SignedSearchController>();
builder.Services.AddScoped<ISearchInitializer, SearchInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program
{
}