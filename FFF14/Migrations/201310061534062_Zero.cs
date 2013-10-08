namespace FFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zero : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Objects",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title6 = c.String(),
                        Discriminator = c.String(maxLength: 128),
                        Chain_RID = c.Guid(),
                        Menu_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Chains", t => t.Chain_RID)
                .ForeignKey("dbo.Objects", t => t.Menu_RID)
                .Index(t => t.Chain_RID)
                .Index(t => t.Menu_RID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Account_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_RID)
                .Index(t => t.Account_RID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserManagement",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        DisableSignIn = c.Boolean(nullable: false),
                        LastSignInTimeUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        ValidUntilUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserSecrets",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.CategoryItems",
                c => new
                    {
                        Category_RID = c.Guid(nullable: false),
                        Item_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_RID, t.Item_RID })
                .ForeignKey("dbo.Categories", t => t.Category_RID)
                .ForeignKey("dbo.Items", t => t.Item_RID)
                .Index(t => t.Category_RID)
                .Index(t => t.Item_RID);
            
            CreateTable(
                "dbo.ChoiceItems",
                c => new
                    {
                        Choice_RID = c.Guid(nullable: false),
                        Item_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Choice_RID, t.Item_RID })
                .ForeignKey("dbo.Choices", t => t.Choice_RID)
                .ForeignKey("dbo.Items", t => t.Item_RID)
                .Index(t => t.Choice_RID)
                .Index(t => t.Item_RID);
            
            CreateTable(
                "dbo.OptionProducts",
                c => new
                    {
                        Option_RID = c.Guid(nullable: false),
                        Product_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Option_RID, t.Product_RID })
                .ForeignKey("dbo.Options", t => t.Option_RID)
                .ForeignKey("dbo.Products", t => t.Product_RID)
                .Index(t => t.Option_RID)
                .Index(t => t.Product_RID);
            
            CreateTable(
                "dbo.RepresentativeChains",
                c => new
                    {
                        Representative_RID = c.Guid(nullable: false),
                        Chain_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Representative_RID, t.Chain_RID })
                .ForeignKey("dbo.Accounts", t => t.Representative_RID)
                .ForeignKey("dbo.Chains", t => t.Chain_RID)
                .Index(t => t.Representative_RID)
                .Index(t => t.Chain_RID);
            
            CreateTable(
                "dbo.RestaurantRepresentatives",
                c => new
                    {
                        Restaurant_RID = c.Guid(nullable: false),
                        Representative_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restaurant_RID, t.Representative_RID })
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RID)
                .ForeignKey("dbo.Accounts", t => t.Representative_RID)
                .Index(t => t.Restaurant_RID)
                .Index(t => t.Representative_RID);
            
            CreateTable(
                "dbo.AllergenComestibles",
                c => new
                    {
                        Allergen_RID = c.Guid(nullable: false),
                        Comestible_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Allergen_RID, t.Comestible_RID })
                .ForeignKey("dbo.Allergens", t => t.Allergen_RID)
                .ForeignKey("dbo.Comestibles", t => t.Comestible_RID)
                .Index(t => t.Allergen_RID)
                .Index(t => t.Comestible_RID);
            
            CreateTable(
                "dbo.IngredientComestibles",
                c => new
                    {
                        Ingredient_RID = c.Guid(nullable: false),
                        Comestible_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_RID, t.Comestible_RID })
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_RID)
                .ForeignKey("dbo.Comestibles", t => t.Comestible_RID)
                .Index(t => t.Ingredient_RID)
                .Index(t => t.Comestible_RID);
            
            CreateTable(
                "dbo.Reviewables",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.ImageOwners",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Reviewables", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        DefaultEmail_RID = c.Guid(),
                        DefaultPhone_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.ImageOwners", t => t.RID)
                .ForeignKey("dbo.Emails", t => t.DefaultEmail_RID)
                .ForeignKey("dbo.Phones", t => t.DefaultPhone_RID)
                .Index(t => t.RID)
                .Index(t => t.DefaultEmail_RID)
                .Index(t => t.DefaultPhone_RID);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Gender_RID = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Profiles", t => t.RID)
                .ForeignKey("dbo.Genders", t => t.Gender_RID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.RID)
                .Index(t => t.Gender_RID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Profile_RID = c.Guid(),
                        State_RID = c.Guid(nullable: false),
                        Nick = c.String(),
                        Line1 = c.String(nullable: false),
                        Line2 = c.String(),
                        City = c.String(nullable: false),
                        ZIP = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Profiles", t => t.Profile_RID)
                .ForeignKey("dbo.States", t => t.State_RID)
                .Index(t => t.RID)
                .Index(t => t.Profile_RID)
                .Index(t => t.State_RID);
            
            CreateTable(
                "dbo.Allergens",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.CardTypes",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Parent_RID = c.Guid(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Categories", t => t.Parent_RID)
                .Index(t => t.RID)
                .Index(t => t.Parent_RID);
            
            CreateTable(
                "dbo.Chains",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.ImageOwners", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Comestibles",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Nutrition_Calories = c.Int(nullable: false),
                        Nutrition_CaloriesFromFat = c.Int(nullable: false),
                        Nutrition_Protein = c.Int(nullable: false),
                        Nutrition_TotalFat = c.Int(nullable: false),
                        Nutrition_Sodium = c.Int(nullable: false),
                        Nutrition_SaturatedFat = c.Int(nullable: false),
                        Nutrition_TransFat = c.Int(nullable: false),
                        Nutrition_Cholesterol = c.Int(nullable: false),
                        Nutrition_DietaryFiber = c.Int(nullable: false),
                        Nutrition_Sugars = c.Int(nullable: false),
                        Nutrition_TotalCarbohydrate = c.Int(nullable: false),
                        Nutrition_VitaminA = c.Int(nullable: false),
                        Nutrition_VitaminC = c.Int(nullable: false),
                        Nutrition_Calcium = c.Int(nullable: false),
                        Nutrition_Iron = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Items", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Profile_RID = c.Guid(),
                        Profile_RID1 = c.Guid(),
                        EmailAddress = c.String(nullable: false),
                        OrderUpdates = c.Boolean(nullable: false),
                        SpecialOffers = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Profiles", t => t.Profile_RID)
                .ForeignKey("dbo.Profiles", t => t.Profile_RID1)
                .Index(t => t.RID)
                .Index(t => t.Profile_RID)
                .Index(t => t.Profile_RID1);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.ImagePaths",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        ImageOwner_RID = c.Guid(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.ImageOwners", t => t.ImageOwner_RID)
                .Index(t => t.RID)
                .Index(t => t.ImageOwner_RID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Choice_RID = c.Guid(),
                        Title = c.String(),
                        Description = c.String(),
                        AdditionalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Choices", t => t.Choice_RID)
                .Index(t => t.RID)
                .Index(t => t.Choice_RID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Account_RID = c.Guid(),
                        DeliveryAddress_RID = c.Guid(),
                        PaymentMethod_RID = c.Guid(),
                        CreateStamp = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Reviewables", t => t.RID)
                .ForeignKey("dbo.Accounts", t => t.Account_RID)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddress_RID)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_RID)
                .Index(t => t.RID)
                .Index(t => t.Account_RID)
                .Index(t => t.DeliveryAddress_RID)
                .Index(t => t.PaymentMethod_RID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Account_RID = c.Guid(),
                        BillingAddress_RID = c.Guid(),
                        CardType_RID = c.Guid(nullable: false),
                        CardHolderName = c.String(nullable: false, maxLength: 20),
                        CardNumber = c.String(nullable: false, maxLength: 19),
                        Expiration = c.DateTime(nullable: false),
                        CCV = c.String(nullable: false, maxLength: 4),
                        Expired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Accounts", t => t.Account_RID)
                .ForeignKey("dbo.Addresses", t => t.BillingAddress_RID)
                .ForeignKey("dbo.CardTypes", t => t.CardType_RID)
                .Index(t => t.RID)
                .Index(t => t.Account_RID)
                .Index(t => t.BillingAddress_RID)
                .Index(t => t.CardType_RID);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Carrier_RID = c.Guid(),
                        Profile_RID = c.Guid(nullable: false),
                        Profile_RID1 = c.Guid(),
                        AreaCode = c.String(nullable: false, maxLength: 3),
                        Prefix = c.String(nullable: false, maxLength: 3),
                        Line = c.String(nullable: false, maxLength: 4),
                        OrderUpdates = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Carriers", t => t.Carrier_RID)
                .ForeignKey("dbo.Profiles", t => t.Profile_RID)
                .ForeignKey("dbo.Profiles", t => t.Profile_RID1)
                .Index(t => t.RID)
                .Index(t => t.Carrier_RID)
                .Index(t => t.Profile_RID)
                .Index(t => t.Profile_RID1);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        ShoppingCart_RID = c.Guid(),
                        Item_RID = c.Guid(nullable: false),
                        Order_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_RID)
                .ForeignKey("dbo.Items", t => t.Item_RID)
                .ForeignKey("dbo.Orders", t => t.Order_RID)
                .Index(t => t.RID)
                .Index(t => t.ShoppingCart_RID)
                .Index(t => t.Item_RID)
                .Index(t => t.Order_RID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Address_RID = c.Guid(),
                        Chain_RID = c.Guid(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Addresses", t => t.Address_RID)
                .ForeignKey("dbo.Chains", t => t.Chain_RID)
                .Index(t => t.RID)
                .Index(t => t.Address_RID)
                .Index(t => t.Chain_RID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Poster_RID = c.Guid(),
                        Reviewed_RID = c.Guid(),
                        Message = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Helpful = c.Int(nullable: false),
                        Unhelpful = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Reported = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .ForeignKey("dbo.Accounts", t => t.Poster_RID)
                .ForeignKey("dbo.Reviewables", t => t.Reviewed_RID)
                .Index(t => t.RID)
                .Index(t => t.Poster_RID)
                .Index(t => t.Reviewed_RID);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        DeliveryAddress_RID = c.Guid(),
                        PaymentMethod_RID = c.Guid(),
                        Account_RID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Reviewables", t => t.RID)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddress_RID)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_RID)
                .ForeignKey("dbo.Accounts", t => t.Account_RID)
                .Index(t => t.RID)
                .Index(t => t.DeliveryAddress_RID)
                .Index(t => t.PaymentMethod_RID)
                .Index(t => t.Account_RID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        RID = c.Guid(nullable: false),
                        Abbreviation = c.String(maxLength: 2),
                        Title = c.String(),
                        Allowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.Objects", t => t.RID)
                .Index(t => t.RID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "RID", "dbo.Objects");
            DropForeignKey("dbo.ShoppingCarts", "Account_RID", "dbo.Accounts");
            DropForeignKey("dbo.ShoppingCarts", "PaymentMethod_RID", "dbo.PaymentMethods");
            DropForeignKey("dbo.ShoppingCarts", "DeliveryAddress_RID", "dbo.Addresses");
            DropForeignKey("dbo.ShoppingCarts", "RID", "dbo.Reviewables");
            DropForeignKey("dbo.Reviews", "Reviewed_RID", "dbo.Reviewables");
            DropForeignKey("dbo.Reviews", "Poster_RID", "dbo.Accounts");
            DropForeignKey("dbo.Reviews", "RID", "dbo.Objects");
            DropForeignKey("dbo.Restaurants", "Chain_RID", "dbo.Chains");
            DropForeignKey("dbo.Restaurants", "Address_RID", "dbo.Addresses");
            DropForeignKey("dbo.Restaurants", "RID", "dbo.Objects");
            DropForeignKey("dbo.Products", "Order_RID", "dbo.Orders");
            DropForeignKey("dbo.Products", "Item_RID", "dbo.Items");
            DropForeignKey("dbo.Products", "ShoppingCart_RID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Products", "RID", "dbo.Objects");
            DropForeignKey("dbo.Phones", "Profile_RID1", "dbo.Profiles");
            DropForeignKey("dbo.Phones", "Profile_RID", "dbo.Profiles");
            DropForeignKey("dbo.Phones", "Carrier_RID", "dbo.Carriers");
            DropForeignKey("dbo.Phones", "RID", "dbo.Objects");
            DropForeignKey("dbo.PaymentMethods", "CardType_RID", "dbo.CardTypes");
            DropForeignKey("dbo.PaymentMethods", "BillingAddress_RID", "dbo.Addresses");
            DropForeignKey("dbo.PaymentMethods", "Account_RID", "dbo.Accounts");
            DropForeignKey("dbo.PaymentMethods", "RID", "dbo.Objects");
            DropForeignKey("dbo.Orders", "PaymentMethod_RID", "dbo.PaymentMethods");
            DropForeignKey("dbo.Orders", "DeliveryAddress_RID", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "Account_RID", "dbo.Accounts");
            DropForeignKey("dbo.Orders", "RID", "dbo.Reviewables");
            DropForeignKey("dbo.Options", "Choice_RID", "dbo.Choices");
            DropForeignKey("dbo.Options", "RID", "dbo.Objects");
            DropForeignKey("dbo.Ingredients", "RID", "dbo.Objects");
            DropForeignKey("dbo.ImagePaths", "ImageOwner_RID", "dbo.ImageOwners");
            DropForeignKey("dbo.ImagePaths", "RID", "dbo.Objects");
            DropForeignKey("dbo.Genders", "RID", "dbo.Objects");
            DropForeignKey("dbo.Emails", "Profile_RID1", "dbo.Profiles");
            DropForeignKey("dbo.Emails", "Profile_RID", "dbo.Profiles");
            DropForeignKey("dbo.Emails", "RID", "dbo.Objects");
            DropForeignKey("dbo.Comestibles", "RID", "dbo.Items");
            DropForeignKey("dbo.Items", "RID", "dbo.ImageOwners");
            DropForeignKey("dbo.Choices", "RID", "dbo.Objects");
            DropForeignKey("dbo.Chains", "RID", "dbo.Objects");
            DropForeignKey("dbo.Categories", "Parent_RID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "RID", "dbo.Objects");
            DropForeignKey("dbo.Carriers", "RID", "dbo.Objects");
            DropForeignKey("dbo.CardTypes", "RID", "dbo.Objects");
            DropForeignKey("dbo.Allergens", "RID", "dbo.Objects");
            DropForeignKey("dbo.Addresses", "State_RID", "dbo.States");
            DropForeignKey("dbo.Addresses", "Profile_RID", "dbo.Profiles");
            DropForeignKey("dbo.Addresses", "RID", "dbo.Objects");
            DropForeignKey("dbo.Accounts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accounts", "Gender_RID", "dbo.Genders");
            DropForeignKey("dbo.Accounts", "RID", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "DefaultPhone_RID", "dbo.Phones");
            DropForeignKey("dbo.Profiles", "DefaultEmail_RID", "dbo.Emails");
            DropForeignKey("dbo.Profiles", "RID", "dbo.ImageOwners");
            DropForeignKey("dbo.ImageOwners", "RID", "dbo.Reviewables");
            DropForeignKey("dbo.Reviewables", "RID", "dbo.Objects");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Account_RID", "dbo.Accounts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserManagement", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IngredientComestibles", "Comestible_RID", "dbo.Comestibles");
            DropForeignKey("dbo.IngredientComestibles", "Ingredient_RID", "dbo.Ingredients");
            DropForeignKey("dbo.AllergenComestibles", "Comestible_RID", "dbo.Comestibles");
            DropForeignKey("dbo.AllergenComestibles", "Allergen_RID", "dbo.Allergens");
            DropForeignKey("dbo.RestaurantRepresentatives", "Representative_RID", "dbo.Accounts");
            DropForeignKey("dbo.RestaurantRepresentatives", "Restaurant_RID", "dbo.Restaurants");
            DropForeignKey("dbo.RepresentativeChains", "Chain_RID", "dbo.Chains");
            DropForeignKey("dbo.RepresentativeChains", "Representative_RID", "dbo.Accounts");
            DropForeignKey("dbo.Objects", "Menu_RID", "dbo.Objects");
            DropForeignKey("dbo.Objects", "Chain_RID", "dbo.Chains");
            DropForeignKey("dbo.OptionProducts", "Product_RID", "dbo.Products");
            DropForeignKey("dbo.OptionProducts", "Option_RID", "dbo.Options");
            DropForeignKey("dbo.ChoiceItems", "Item_RID", "dbo.Items");
            DropForeignKey("dbo.ChoiceItems", "Choice_RID", "dbo.Choices");
            DropForeignKey("dbo.CategoryItems", "Item_RID", "dbo.Items");
            DropForeignKey("dbo.CategoryItems", "Category_RID", "dbo.Categories");
            DropIndex("dbo.States", new[] { "RID" });
            DropIndex("dbo.ShoppingCarts", new[] { "Account_RID" });
            DropIndex("dbo.ShoppingCarts", new[] { "PaymentMethod_RID" });
            DropIndex("dbo.ShoppingCarts", new[] { "DeliveryAddress_RID" });
            DropIndex("dbo.ShoppingCarts", new[] { "RID" });
            DropIndex("dbo.Reviews", new[] { "Reviewed_RID" });
            DropIndex("dbo.Reviews", new[] { "Poster_RID" });
            DropIndex("dbo.Reviews", new[] { "RID" });
            DropIndex("dbo.Restaurants", new[] { "Chain_RID" });
            DropIndex("dbo.Restaurants", new[] { "Address_RID" });
            DropIndex("dbo.Restaurants", new[] { "RID" });
            DropIndex("dbo.Products", new[] { "Order_RID" });
            DropIndex("dbo.Products", new[] { "Item_RID" });
            DropIndex("dbo.Products", new[] { "ShoppingCart_RID" });
            DropIndex("dbo.Products", new[] { "RID" });
            DropIndex("dbo.Phones", new[] { "Profile_RID1" });
            DropIndex("dbo.Phones", new[] { "Profile_RID" });
            DropIndex("dbo.Phones", new[] { "Carrier_RID" });
            DropIndex("dbo.Phones", new[] { "RID" });
            DropIndex("dbo.PaymentMethods", new[] { "CardType_RID" });
            DropIndex("dbo.PaymentMethods", new[] { "BillingAddress_RID" });
            DropIndex("dbo.PaymentMethods", new[] { "Account_RID" });
            DropIndex("dbo.PaymentMethods", new[] { "RID" });
            DropIndex("dbo.Orders", new[] { "PaymentMethod_RID" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_RID" });
            DropIndex("dbo.Orders", new[] { "Account_RID" });
            DropIndex("dbo.Orders", new[] { "RID" });
            DropIndex("dbo.Options", new[] { "Choice_RID" });
            DropIndex("dbo.Options", new[] { "RID" });
            DropIndex("dbo.Ingredients", new[] { "RID" });
            DropIndex("dbo.ImagePaths", new[] { "ImageOwner_RID" });
            DropIndex("dbo.ImagePaths", new[] { "RID" });
            DropIndex("dbo.Genders", new[] { "RID" });
            DropIndex("dbo.Emails", new[] { "Profile_RID1" });
            DropIndex("dbo.Emails", new[] { "Profile_RID" });
            DropIndex("dbo.Emails", new[] { "RID" });
            DropIndex("dbo.Comestibles", new[] { "RID" });
            DropIndex("dbo.Items", new[] { "RID" });
            DropIndex("dbo.Choices", new[] { "RID" });
            DropIndex("dbo.Chains", new[] { "RID" });
            DropIndex("dbo.Categories", new[] { "Parent_RID" });
            DropIndex("dbo.Categories", new[] { "RID" });
            DropIndex("dbo.Carriers", new[] { "RID" });
            DropIndex("dbo.CardTypes", new[] { "RID" });
            DropIndex("dbo.Allergens", new[] { "RID" });
            DropIndex("dbo.Addresses", new[] { "State_RID" });
            DropIndex("dbo.Addresses", new[] { "Profile_RID" });
            DropIndex("dbo.Addresses", new[] { "RID" });
            DropIndex("dbo.Accounts", new[] { "User_Id" });
            DropIndex("dbo.Accounts", new[] { "Gender_RID" });
            DropIndex("dbo.Accounts", new[] { "RID" });
            DropIndex("dbo.Profiles", new[] { "DefaultPhone_RID" });
            DropIndex("dbo.Profiles", new[] { "DefaultEmail_RID" });
            DropIndex("dbo.Profiles", new[] { "RID" });
            DropIndex("dbo.ImageOwners", new[] { "RID" });
            DropIndex("dbo.Reviewables", new[] { "RID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Account_RID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserManagement", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.IngredientComestibles", new[] { "Comestible_RID" });
            DropIndex("dbo.IngredientComestibles", new[] { "Ingredient_RID" });
            DropIndex("dbo.AllergenComestibles", new[] { "Comestible_RID" });
            DropIndex("dbo.AllergenComestibles", new[] { "Allergen_RID" });
            DropIndex("dbo.RestaurantRepresentatives", new[] { "Representative_RID" });
            DropIndex("dbo.RestaurantRepresentatives", new[] { "Restaurant_RID" });
            DropIndex("dbo.RepresentativeChains", new[] { "Chain_RID" });
            DropIndex("dbo.RepresentativeChains", new[] { "Representative_RID" });
            DropIndex("dbo.Objects", new[] { "Menu_RID" });
            DropIndex("dbo.Objects", new[] { "Chain_RID" });
            DropIndex("dbo.OptionProducts", new[] { "Product_RID" });
            DropIndex("dbo.OptionProducts", new[] { "Option_RID" });
            DropIndex("dbo.ChoiceItems", new[] { "Item_RID" });
            DropIndex("dbo.ChoiceItems", new[] { "Choice_RID" });
            DropIndex("dbo.CategoryItems", new[] { "Item_RID" });
            DropIndex("dbo.CategoryItems", new[] { "Category_RID" });
            DropTable("dbo.States");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Reviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Products");
            DropTable("dbo.Phones");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Orders");
            DropTable("dbo.Options");
            DropTable("dbo.Ingredients");
            DropTable("dbo.ImagePaths");
            DropTable("dbo.Genders");
            DropTable("dbo.Emails");
            DropTable("dbo.Comestibles");
            DropTable("dbo.Items");
            DropTable("dbo.Choices");
            DropTable("dbo.Chains");
            DropTable("dbo.Categories");
            DropTable("dbo.Carriers");
            DropTable("dbo.CardTypes");
            DropTable("dbo.Allergens");
            DropTable("dbo.Addresses");
            DropTable("dbo.Accounts");
            DropTable("dbo.Profiles");
            DropTable("dbo.ImageOwners");
            DropTable("dbo.Reviewables");
            DropTable("dbo.IngredientComestibles");
            DropTable("dbo.AllergenComestibles");
            DropTable("dbo.RestaurantRepresentatives");
            DropTable("dbo.RepresentativeChains");
            DropTable("dbo.OptionProducts");
            DropTable("dbo.ChoiceItems");
            DropTable("dbo.CategoryItems");
            DropTable("dbo.AspNetUserSecrets");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetTokens");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserManagement");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Objects");
        }
    }
}
