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

builder.Services.AddTransient<IExtractor, AdvancedDocumentWordsExtractor>();
builder.Services.AddTransient<ITextEditor, DocumentTextEditor>();
builder.Services.AddTransient<ISignedSearchStrategy, SignedSearchStrategy>();
builder.Services.AddTransient<IResultListMaker, IntersectResultListMaker>();
builder.Services.AddTransient<IResultListMaker, UnionResultListMaker>();
builder.Services.AddTransient<IWordFinder, PositiveWordFinder>();
builder.Services.AddTransient<IWordFinder, NegativeWordFinder>();
builder.Services.AddTransient<IWordFinder, UnsignedWordFinder>();
builder.Services.AddTransient<IInvertedIndex, InvertedIndexDbController>();
builder.Services.AddTransient<IDataFolderReader, DocumentFolderReader>();
builder.Services.AddTransient<IDataFolderReaderFactory, DataFolderReaderFactory>();
builder.Services.AddTransient<ISearchStrategyFactory, SearchStrategyFactory>();
builder.Services.AddTransient<ISearchController, SignedSearchController>();
builder.Services.AddTransient<ISearchInitializer, SearchInitializer>();

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

public partial class Program { }