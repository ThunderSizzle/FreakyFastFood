using FFF.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFF.Models
{
	//todo Comment Class
	public class Account : Profile
	{
		public virtual ICollection<Review> ReviewsBy { get; set; }
		public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ShoppingCart ShoppingCart { get; set; }
		public virtual User User { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public virtual Gender Gender { get; set; }
		public virtual DateTime Birthday { get; set; }

		public override bool Removeable
		{
			get
			{
				return false;
			}
		}

        public Account()
			: base()
		{
			this.ReviewsBy = new Collection<Review>();
			this.PaymentMethods = new Collection<PaymentMethod>();
			this.Orders = new Collection<Order>();
        }
	}
	/*
    public class UserLogin : IUserLogin
    {
        [Key, Column(Order = 0)]
        public string LoginProvider { get; set; }
        [Key, Column(Order = 1)]
        public string ProviderKey { get; set; }

        public string UserId { get; set; }

        public UserLogin() { }

        public UserLogin(string userId, string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            UserId = userId;
        }
    }

    public class UserSecret : IUserSecret
    {
        public UserSecret()
        {
        }

        public UserSecret(string userName, string secret)
        {
            UserName = userName;
            Secret = secret;
        }

        [Key]
        public string UserName { get; set; }
        public string Secret { get; set; }
    }

    public class UserRole : IUserRole
    {
        [Key, Column(Order = 0)]
        public string RoleId { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
    }

    public class Role : IRole
    {
        public Role()
            : this(String.Empty)
        {
        }

        public Role(string roleName)
        {
            Id = roleName;
        }

        [Key]
        public string Id { get; set; }
    }*/
}