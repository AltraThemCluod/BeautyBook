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
    public abstract class AbstractDashboardData
    {
        public AbstractDashboardData()
        {
            this.UsersSalonOwner = new List<Users>();
            this.UsersSallers = new List<Users>();
            this.SalonBranch = new List<Salons>();
        }
        public long TotalSalonOwner { get; set; }
        public long TotalSeller { get; set; }
        public long TotalSalon { get; set; }
        public long LookUpState { get; set; }
        public long LookUpCountry { get; set; }
        public List<Users> UsersSalonOwner { get; set; }
        public List<Users> UsersSallers { get; set; }
        public List<Salons> SalonBranch { get; set; }
    }
}

