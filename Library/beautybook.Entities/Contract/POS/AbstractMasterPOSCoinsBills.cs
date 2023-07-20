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
	public abstract class AbstractMasterPOSCoinsBills
	{
		public long Id { get; set; }
		public decimal Amount { get; set; }
		
	}
}

