using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractBlogContent
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Heading { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}

