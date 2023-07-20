using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Entities.V1
{
	public class POSDetails : AbstractPOSDetails { }
	public class POSOrderDetails : AbstractPOSOrderDetails { }
	public class PosOffer : AbstractPosOffer { }
	public class POSOpeningCash : AbstractPOSOpeningCash { }
	public class POSPayment : AbstractPOSPayment { }
	public class MasterPOSPaymentType : AbstractMasterPOSPaymentType { }
	public class MasterPOSCoinsBills : AbstractMasterPOSCoinsBills { }
	public class POSSession : AbstractPOSSession { }
	public class POSAssignEmployee : AbstractPOSAssignEmployee { }
	public class PosInvoice : AbstractPosInvoice { }
	public class PosReturnInvoice : AbstractPosReturnInvoice { }
}