using FFF.InputModels;
using FFF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;

namespace FFF.Controllers.AccountObject
{
	[AllowAnonymous]
	public class ShoppingCartController : AccountObjectController
	{
		public ShoppingCartController()
			: base()
		{

		}
		public ShoppingCartController( FFF.Models.Account Account )
			: base( Account )
		{

		}
		// GET api/Address
		public ShoppingCart Get()
		{
			return Account.ShoppingCart;
		}

		public async Task<IHttpActionResult> AddProduct( ProductInput model )
		{
			Account.ShoppingCart.Products.Add(new Product(await db.Items.FindAsync(model.ItemRID), model));
			await db.SaveChangesAsync();
			return Ok( );
		}
		public async Task<IHttpActionResult> RemoveProduct( ProductInput model )
		{
			Account.ShoppingCart.Products.Remove(await db.Products.FindAsync(model.RID));
			await db.SaveChangesAsync();
			return Ok( );
		}
		public async Task<IHttpActionResult> EditProduct( ProductInput model )
		{
			Product Product = this.Account.ShoppingCart.Products.FirstOrDefault( c => c.RID == model.RID );
			if ( Product == null )
			{
				return NotFound();
			}
			Product.SelectedOptions = model.SelectedOptions;
			db.Entry( Product ).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok( );
		}
		public async Task<IHttpActionResult> SetAddress( Guid AddressID )
		{
			this.Account.ShoppingCart.DeliveryAddress = await db.Addresses.FindAsync(AddressID);
			await db.SaveChangesAsync();
			return Ok( );

		}
	}
}