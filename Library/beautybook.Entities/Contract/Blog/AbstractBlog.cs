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
    public abstract class AbstractBlog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public string ThumbnailDataUrl { get;set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public int Like { get; set; }
        public int InstagramShare { get; set; }
        public int TwitterShare { get; set; }
        public int linkedinShare { get; set; }
        public int EmailShare { get; set; }
        public int WhatsAppShare { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string CreatedUserEmail { get; set; }
        public string CreatedUserContactNumber { get; set; }
        public string CreatedUserProfileUrl { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public string BlogContentDetailsData { get; set; }
        public List<BlogContentRoot> BlogContent { get; set; }
        public string BlogContentStr { get; set; }
        public string UserTypeName { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string ArticleCreatedDate => CreatedDate != null ? CreatedDate.ToString("MMM dd, yyyy") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        //[NotMapped]
        //public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }

    public class BlogContentRoot
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Heading { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }

    public abstract class AbstractBlogImages
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}

