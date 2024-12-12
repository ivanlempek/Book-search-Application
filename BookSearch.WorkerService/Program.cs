using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using BookSearch.Jobs;
using BookSearch.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BookSearch.Application.Interfaces;
using BookSearch.Application.Services;
using BookSearch.Infrastructure.Repository;
using BookSearch.WorkerService;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<BookContext>(options => options.UseSqlServer("Server=localhost,1433;Database=BookDb;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;"));
        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<BookUpsertJob>();
        services.AddHangfire(config => config.UseSqlServerStorage("Server=localhost,1433;Database=BookDb;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;"));
        services.AddHangfireServer();
        services.AddHostedService<Worker>();
    })
    .Build()
    .Run();
