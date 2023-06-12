using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class CommentsDbRepository : ICommentsRepository
    {
        private DatabaseContext databaseContext;
        private readonly UserManager<User> userManager;
        public CommentsDbRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }
        
        public async Task SaveAsync(Comment comment, Guid productId, Guid userId)
        {
            var product = await databaseContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var user = await userManager.FindByIdAsync(userId.ToString());
            comment.UserId = userId;
            comment.Product = product;
            databaseContext.Comments.Add(comment); 
            await databaseContext.SaveChangesAsync();
        }
    }
}
