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
    public abstract class AbstractEmailMarketingTemplates
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string HtmlTemplatesText { get; set; }
        public DateTime CreatedDate { get; set; }
      
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

