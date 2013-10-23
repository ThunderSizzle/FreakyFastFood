using FFF.InputModels;
using FFF.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FFF.Areas.Account.Controllers.API
{
	[Authorize]
	public class ProfileController : AccountObjectController
    {
		public ProfileController()
			: base()
		{

		}
		public ProfileController( FFF.Models.Account Account )
			: base (Account)
		{

		}
		// GET api/Address
        public FFF.Models.Account Get()
        {
			return this.Account;
        }

        // PUT api/Address/5
		public async Task<IHttpActionResult> Put( ProfileInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var Profile = this.Account;
			if ( Profile == null )
			{
				return NotFound();
			}

			db.Entry( Profile ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( Profile );
        }

        // POST api/Address
		[ResponseType( typeof( Profile ) )]
		public async Task<IHttpActionResult> Post( ProfileInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			var Profile = new FFF.Models.Account();
			db.Accounts.Add(Profile);
			await db.SaveChangesAsync();
			return Ok( Profile );
        }

        // DELETE api/Address/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
			var Profile = this.Account;
			if ( Profile == null )
            {
                return NotFound();
            }
			db.Accounts.Remove( Profile );
            await db.SaveChangesAsync();
			return Ok( Profile );
        }
    }
}