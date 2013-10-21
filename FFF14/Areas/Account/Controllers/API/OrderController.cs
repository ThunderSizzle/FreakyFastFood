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
	public class OrderController : AccountObjectController
    {
		// GET api/Order
		public ICollection<Order> Get()
        {
			return Account.Orders;
        }

		// GET api/Order/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult Get(Guid id)
        {
			Order Order = this.Account.Orders.FirstOrDefault( c => c.RID == id );
			if ( Order == null )
            {
                return NotFound();
            }

			return Ok( Order );
        }

		// PUT api/Order/5
		public async Task<IHttpActionResult> Put( OrderInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			Order Order = Account.Orders.FirstOrDefault( c => c.RID == model.RID );
			if ( Order == null )
			{
				return NotFound();
			}

			db.Entry( Order ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( Order );
        }

		// POST api/Order
		[ResponseType( typeof( Order ) )]
		public async Task<IHttpActionResult> Post( OrderInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			Order Order = new Order( );
			Account.Orders.Add( Order );
			await db.SaveChangesAsync();
			return Ok( Order );
        }

		// DELETE api/Order/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
			Order Order = Account.Orders.FirstOrDefault( c => c.RID == id );
			if ( Order == null )
            {
                return NotFound();
            }

			Account.Orders.Remove( Order );
            await db.SaveChangesAsync();

			return Ok( Order );
        }
    }
}