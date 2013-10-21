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
	public class SettingController : AccountObjectController
    {
        // GET api/Address
        public ICollection<Setting> Get()
        {
			return Account.Settings;
        }

		// GET api/Address/5
		[ResponseType( typeof( Setting ) )]
        public IHttpActionResult Get(Guid id)
        {
			Setting Setting = this.Account.Settings.FirstOrDefault( c => c.RID == id );
			if ( Setting == null )
            {
                return NotFound();
            }

			return Ok( Setting );
        }

        // PUT api/Address/5
		public async Task<IHttpActionResult> Put( SettingInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			Setting Setting = Account.Settings.FirstOrDefault( c => c.RID == model.RID );
			if ( Setting == null )
			{
				return NotFound();
			}

			db.Entry( Setting ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( Setting );
        }

        // POST api/Address
		[ResponseType( typeof( Setting ) )]
		public async Task<IHttpActionResult> Post( SettingInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			Setting Setting = new Setting();
			Account.Settings.Add( Setting );
			await db.SaveChangesAsync();
			return Ok( Setting );
        }

        // DELETE api/Address/5
		[ResponseType( typeof( Setting ) )]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
			Setting Setting = Account.Settings.FirstOrDefault( c => c.RID == id );
			if ( Setting == null )
            {
                return NotFound();
            }

			Account.Settings.Remove( Setting );
            await db.SaveChangesAsync();

			return Ok( Setting );
        }
    }
}