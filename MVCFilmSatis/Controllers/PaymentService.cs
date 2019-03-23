using MVCFilmSatis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilmSatis.Controllers
{
	public abstract class PaymentService
	{
		public abstract bool MakePayment(IPaymentModel pm);
	}

	public class BankTransferService : PaymentService
	{
		public override bool MakePayment(IPaymentModel pm)
		{
			var info = (BankTransferPayment)pm;
			return true;
		}
	}

	public class CreditCardService : PaymentService
		public override bool MakePayment(IPaymentModel pm)
		{
			var info = (BankTransferPayment)pm;
			return true;
		}
	}
}