using CommentManagement.Application;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Context;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application.Contracts.Comment;

namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            #region Comment
            
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            #endregion

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
        
    }
}