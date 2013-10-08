using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FFF.Models.ProfileSystem;
using FFF.Models.PaymentSystem;
using FFF.Models.OrderSystem;
using System.Collections.ObjectModel;
using FFF.Models.LocationSystem;
using FFF.Models.ReviewSystem;
using FFF.ViewModels.Account;

namespace FFF.Models.UserSystem
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
		/// <summary>
		/// True for Man. False for Woman. Null for Neither
		/// </summary>
		public virtual Gender Gender { get; set; }
		//public DateTime Birthday { get; set; }

        public Account()
			: base()
		{
			this.ReviewsBy = new Collection<Review>();
			this.PaymentMethods = new Collection<PaymentMethod>();
			this.Orders = new Collection<Order>();
        }
		public Account (RegisterViewModel Register)
			: this()
		{
			this.User = new User();
			this.User.Id = this.RID.ToString();
			this.User.UserName = Register.UserName;
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