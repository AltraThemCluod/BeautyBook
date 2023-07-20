using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicZaanVendor.Entities.Contract
{
    public abstract class AbstractBlogSubscribeNewslatter
    {
        public int Id { get; set; }
        public int BlogId { get; set; }

		[Required(ErrorMessageResourceType = (typeof(Resources.Language)), ErrorMessageResourceName = "Emailisrequired")]
        public string Email { get; set; }
    }
}

