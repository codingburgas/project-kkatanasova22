using LibraryManager.Services.Contracts;
using LibraryManager.Services.Implementations;

namespace LibraryManager.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IAuthorService, AuthorsService>();

            services.AddScoped<ILoanService,LoanService>();

            services.AddScoped<IAccountService,AccountService>();

            services.AddScoped<IStatisticsService,StatisticsService>();
            return services;
        }
    }
}
