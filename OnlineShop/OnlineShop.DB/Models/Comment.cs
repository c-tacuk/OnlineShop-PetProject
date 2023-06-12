using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Guid Id { get; set; }
        public int Evaluation { get; set; }
        public string Text { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
    }
}
