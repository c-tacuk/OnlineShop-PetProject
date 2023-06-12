using System;

namespace OnlineShopWebApp.Models
{
    public class CommentViewModel
    {
        public int Evaluation { get; set; }
        public string Text { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid UserId { get; set; }
    }
}
