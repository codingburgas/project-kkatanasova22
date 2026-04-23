using LibraryManager.Services.Contracts;
using LibraryManager.Services.Implementations;
using LibraryManagers.Core.Contracts;

namespace LibraryManager.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<ILoanService,LoanService>();

            return services;
        }
    }
}
