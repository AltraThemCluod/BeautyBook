using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractBlogSubscribeNewslatter
    {
        public int Id { get; set; }
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}

