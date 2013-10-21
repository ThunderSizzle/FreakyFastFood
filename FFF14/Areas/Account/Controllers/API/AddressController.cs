using FFF.InputModels;
using FFF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;

namespace FFF.Areas.Account.Controllers.API
{
	[Authorize]
    public class AddressController : AccountObjectController
    {
        // GET api/Address
        public ICollection<Address> Get()
        {
            return Account.Addresses;
        }

		// GET api/Address/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult Get(Guid id)
        {
			Address Address = this.Account.Addresses.FirstOrDefault( c => c.RID == id );
            if (Address == null)
            {
                return NotFound();
            }

            return Ok(Address);
        }

        // PUT api/Address/5
        public async Task<IHttpActionResult> Put(AddressInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			Address Address = Account.Addresses.FirstOrDefault( c => c.RID == model.RID );
			if(Address == null)
			{
				return NotFound();
			}
			
			Address.Nick = model.Nick;
			Address.Line1 = model.Line1;
			Address.Line2 = model.Line2;
			Address.City = model.City;
			Address.State = await db.States.FindAsync(model.StateID);
			Address.ZIP = model.ZIP;

            db.Entry(Address).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( Address );
        }

        // POST api/Address
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Post(AddressInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			Address Address = new Address(model.Nick, model.Line1, model.Line2, model.City, await db.States.FindAsync(model.StateID), model.ZIP);
            Account.Addresses.Add(Address);
			await db.SaveChangesAsync();
            return Ok( Address );
        }

        // DELETE api/Address/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            Address Address = Account.Addresses.FirstOrDefault( c => c.RID == id);
			if ( Address == null )
            {
                return NotFound();
            }

			Account.Addresses.Remove( Address );
            await db.SaveChangesAsync();

			return Ok( Address );
        }
    }
}