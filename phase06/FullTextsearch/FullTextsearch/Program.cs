using FullTextsearch.Context;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Document.Extractor.Abstraction;
using FullTextsearch.Document.Formater;
using FullTextsearch.Document.Formater.Abstraction;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.Factory.SearchFactory.Abstraction;
using FullTextsearch.Initialize;
using FullTextsearch.Initialize.Abstarction;
using FullTextsearch.InvertedIndex;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.SearchManager.Abstraction;
using FullTextsearch.SearchManager.ResultList;
using FullTextsearch.SearchManager.ResultList.Abstraction;
using FullTextsearch.SearchManager.SignedSearchManager;
using FullTextsearch.Service;
using FullTextsearch.Service.Abstraction;
using FullTextsearch.WordFinder;
using FullTextsearch.WordFinder.Abstraction;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddDb(builder);

AddDi(builder);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

void AddDb(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
}

void AddDi(WebApplicationBuilder builder)
{
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
    builder.Services.AddScoped<IApiService, ApiService>();
}