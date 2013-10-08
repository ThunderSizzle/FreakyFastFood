using FFF.Models.FoodProviderSystem;
using FFF.Models.ItemSystem;
using FFF.Models.LocationSystem;
using FFF.Models.OrderSystem;
using FFF.Models.ProfileSystem;
using FFF.Models.ReviewSystem;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using FFF.Models.UserSystem;
using FFF.Models.PaymentSystem;
using System.Collections.ObjectModel;
using System.Collections;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using FFF.Models.ContactSystem;
using System.Security.Cryptography;
using FFF.Models.ImagesSystem;

namespace FFF.Models
{
	//todo Comment Class
	public abstract class DatabaseObject
	{
		[Key]
		public Guid RID { get; set; }


		public DatabaseObject()
		{
			this.RID = Guid.NewGuid();
		}
	}

    public class DatabaseContext : IdentityDbContext<FFFUser, UserClaim, UserSecret, UserLogin, Role, UserRole, Token, UserManagement>
    {
		public DatabaseContext() : base("DefaultConnection")
		{

		}
		public DbSet<CardType> CardTypes { get; set; }
		public DbSet<ImagePath> ImagePaths { get; set; }
		public DbSet<Carrier> Carriers { get; set; }
		public DbSet<Gender> Genders { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Phone> Phones { get; set; }
		public DbSet<Chain> Chains { get; set; }
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Reviewable> Reviewables { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Comestible> Comestibles { get; set; }
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Allergen> Allergens { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Choice> Choices { get; set; }
		public DbSet<Option> Option { get; set; }
		public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DatabaseObject>().ToTable( "Objects" ).
				Map<CardType>( m =>
				{
					m.ToTable( "CardTypes" );
				} ).
				Map<Gender>( m =>
				{
					m.ToTable( "Genders" );
				} ).
				Map<ImagePath>( m =>
				{
					m.ToTable( "ImagePaths" );
				} ).
				Map<Carrier>( m =>
				{
					m.ToTable( "Carriers" );
				} ).
				Map<Phone>( m =>
				{
					m.ToTable( "Phones" );
				} ).
				Map<Email>( m =>
				{
					m.ToTable( "Emails" );
				} ).
				Map<State> (m =>
				{
					m.ToTable( "States" );
				}).
				Map<Product>( m=>
				{
					m.ToTable( "Products" );
				}).
				Map<Category>( m => m.ToTable( "Categories" ) ).
				Map<PaymentMethod>( m => m.ToTable( "PaymentMethods" ) ).
				Map<Address>( m => m.ToTable( "Addresses" ) ).
				Map<Option>( m => m.ToTable( "Options" ) ).
				Map<Choice>( m => m.ToTable( "Choices" ) ).
				Map<Allergen>( m => m.ToTable( "Allergens" ) ).
				Map<Ingredient>( m => m.ToTable( "Ingredients" ) ).
				Map<Review>( m => m.ToTable( "Reviews" ) ).
				Map<Reviewable>(m => m.ToTable("Reviewables")).
				Map<ImageOwner>( m =>
				{
					m.ToTable( "ImageOwners" );
				} ).
				Map<Profile>( m =>
				{
					m.ToTable( "Profiles" );
				} ).
				Map<Item>( m =>
				{
					m.ToTable( "Items" );
				} ).
				Map<Order>( m =>
				{
					m.ToTable("Orders");
				} ).
				Map<Account>( m =>
				{
					m.ToTable( "Accounts" );
					m.Requires( "Discriminator" ).HasValue( "Account" );
				} ).
				Map<ShoppingCart>( m =>
				{
					m.ToTable( "ShoppingCarts" );
				} ).
				Map<Comestible>( m =>
				{
					m.ToTable( "Comestibles" );
					m.Requires( "Discriminator" ).HasValue( "Comestible" );
				} ).
				Map<Beverage>( m =>
				{
					m.ToTable( "Comestibles" );
					m.Requires( "Discriminator" ).HasValue( "Beverage" );
				} ).
				Map<Food>( m =>
				{
					m.ToTable( "Comestibles" );
					m.Requires( "Discriminator" ).HasValue( "Food" );
				} ).
				Map<Employee>( m =>
				{
					m.ToTable( "Accounts" );
					m.Requires( "Discriminator" ).HasValue( "Employee" );
				} ).
				Map<Representative>( m =>
				{
					m.ToTable( "Accounts" );
					m.Requires( "Discriminator" ).HasValue( "Representative" );
				} ).
				Map<Chain>( m =>
				{
					m.ToTable( "Chains" );
				} ).
				Map<Restaurant>( m =>
				{
					m.ToTable( "Restaurants" );
				} );

			modelBuilder.Entity<Account>().HasOptional<User>( c => c.User );
			modelBuilder.Entity<Account>().HasOptional<ShoppingCart>( c => c.ShoppingCart ).WithRequired(c => c.Account);
		
			base.OnModelCreating(modelBuilder);
		}
	}
}