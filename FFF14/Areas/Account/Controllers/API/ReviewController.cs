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
	public class ReviewController : AccountObjectController
    {
		public ReviewController()
			: base()
		{

		}
		public ReviewController( FFF.Models.Account Account )
			: base (Account)
		{

		}
        // GET api/Address
		public ICollection<Review> Get()
        {
			return Account.Reviews;
        }

		// GET api/Address/5
		[ResponseType( typeof( Review ) )]
        public IHttpActionResult Get(Guid id)
        {
			Review Review = this.Account.ReviewsBy.FirstOrDefault( c => c.RID == id );
			if ( Review == null )
            {
                return NotFound();
            }

			return Ok( Review );
        }

        // PUT api/Address/5
		public async Task<IHttpActionResult> Put( ReviewInput model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			Review Review = Account.ReviewsBy.FirstOrDefault( c => c.RID == model.RID );
			if ( Review == null )
			{
				return NotFound();
			}

			db.Entry( Review ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( Review );
        }

        // POST api/Address
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Post(ReviewInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			Review Review = new Review( );
			Account.ReviewsBy.Add( Review );
			await db.SaveChangesAsync();
			return Ok( Review );
        }

        // DELETE api/Address/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
			Review Review = Account.ReviewsBy.FirstOrDefault( c => c.RID == id );
			if ( Review == null )
            {
                return NotFound();
            }

			Account.ReviewsBy.Remove( Review );
            await db.SaveChangesAsync();

			return Ok( Review );
        }
    }
}