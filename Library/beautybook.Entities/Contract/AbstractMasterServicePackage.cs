﻿using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractMasterServicePackage
    {
        public long Id { get; set; }
        public long ServicePackageId { get; set; }
        public long ParentId { get; set; }
        public long LookUpServicesId { get; set; }
        public string ServiceName { get; set; }
        public string CategoryName { get; set; }
        public decimal LookUpServicesPrice { get; set; }
        public decimal LookUpServicesDuration { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long PackageCustomPrices { get; set; }
        public string ServicePhoto { get; set; }


        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        
    }
}

