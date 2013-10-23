using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace FFF.Areas.Account.Controllers.API
{
	[Authorize]
	public class PaymentMethodController : AccountObjectController
    {
		public PaymentMethodController()
			: base()
		{

		}
		public PaymentMethodController( FFF.Models.Account Account )
			: base (Account)
		{

		}
        // GET api/Address
		public ICollection<PaymentMethod> Get()
        {
			return Account.PaymentMethods;
        }

		// GET api/Address/5
		[ResponseType( typeof( PaymentMethod ) )]
        public IHttpActionResult Get(Guid id)
        {
			PaymentMethod PaymentMethod = this.Account.PaymentMethods.FirstOrDefault( c => c.RID == id );
			if ( PaymentMethod == null )
            {
                return NotFound();
            }

			return Ok( PaymentMethod );
        }

        // PUT api/Address/5
		public async Task<IHttpActionResult> Put( PaymentMethodInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			PaymentMethod PaymentMethod = Account.PaymentMethods.FirstOrDefault( c => c.RID == model.RID );
			if ( PaymentMethod == null )
			{
				return NotFound();
			}

			db.Entry( PaymentMethod ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( PaymentMethod );
        }

        // POST api/Address
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Post(PaymentMethodInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			PaymentMethod PaymentMethod = new PaymentMethod();
			Account.PaymentMethods.Add( PaymentMethod );
			await db.SaveChangesAsync();
			return Ok( PaymentMethod );
        }

        // DELETE api/Address/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
			PaymentMethod PaymentMethod = Account.PaymentMethods.FirstOrDefault( c => c.RID == id );
			if ( PaymentMethod == null )
            {
                return NotFound();
            }

			Account.PaymentMethods.Remove( PaymentMethod );
            await db.SaveChangesAsync();
			return Ok( PaymentMethod );
        }
    }
}