//View Models for each Individual Object:
function AddressViewModel(rid, nick, line1, line2, city, state, zip)
{
	var self = this;
	self.rid = ko.observable(rid);
	self.nick = ko.observable(nick);
	self.line1 = ko.observable(line1);
	self.line2 = ko.observable(line2);
	self.city = ko.observable(city);
	if (state != null)
	{
		self.state = ko.observable(new StateViewModel(state.RID, state.Title, state.Abbreviation));
	}
	else
	{
		self.state = ko.observable(new StateViewModel("", "", ""));
	}
	self.zip = ko.observable(zip);
}
function OrderViewModel()
{

}
function PaymentMethodViewModel()
{

}
function SettingViewModel()
{

}
function ReviewViewModel()
{

}
function ProfileViewModel()
{

}
function StateViewModel(rid, title, abbreviation)
{
	var self = this;
	self.rid = rid;
	self.title = title;
	self.abbreviation = abbreviation;
}

var Page = new function()
{
	var page = this;
	page.init = function()
	{
		page.Modal.init();
		page.FFFObject.init();
		page.Employee.init();
		page.Representative.init();
		page.Account.init();
	}
	//Controls the Page's Modal
	page.Modal = new function ()
	{
		var modal = this;
		modal.init = function()
		{

		}
		modal.displayMode = ko.observable('ModalTest');
		modal.data = ko.observable();
		modal.LoadModal = function ()
		{
			$('#Modal').modal('show');
			$.validator.unobtrusive.parse('#ModalContent');
		}
		modal.CloseModal = function ()
		{
			$('#Modal').modal('hide');
			$('body').removeClass('modal-open');
			$('.modal-backdrop').remove();
		}
	}
	//Controls all FFFObjects that is public information.
	page.FFFObject = new function ()
	{
		var fffobject = this;
		fffobject.init = function()
		{
			fffobject.States.init();
			fffobject.Items.init();
		}
		fffobject.States = new function()
		{
			var states = this;
			states.hub = $.connection.accountAddressHub;
			states.list = ko.observableArray([]);
			states.loading = true;
			states.init = function ()
			{
				this.hub.server.stateList();
			}

			states.hub.client.stateListBack = function (stateList)
			{
				$.map(stateList, function (state)
				{
					states.list.push(new StateViewModel(state.RID, state.Title, state.Abbreviation));
				});
				states.list.sort(function (left, right)
				{
					if (left.title == right.title)
					{
						return 0;
					}
					else if (left.title < right.title)
					{
						return -1;
					}
					else
					{
						return 1;
					}
				});
				states.loading = false;
			}
		}
		fffobject.Items = new function()
		{
			var items = this;
			items.init = function ()
			{

			}
			//todo System for real-time filtering and searching of Items, along with passing an Item into a Product and attaching that Product to a Shopping Cart.
		}
		//todo Build System to update all public objects from modifications by all Users, regardless of role.
	}
	//Controls all objects that a Employee can view, edit, or create.
	page.Employee = new function()
	{
		var employee = this;
		employee.init = function()
		{

		}
		//todo Build System for an Employee to view personal information, but also important information to complete the current assigned task.
	}
	//Controls all objects that a Representative can view, edit, or create.
	page.Representative = new function()
	{
		var representative = this;
		representative.init = function()
		{
			representative.Restaurants.init();
			representative.Chains.init();
		}
		//todo Build System for a Rep to view, edit, and create all information about Chains and Retaurants.
		representative.Restaurants = new function()
		{
			var restaurants = this;
			restaurants.init = function ()
			{

			}
		}
		representative.Chains = new function()
		{
			var chains = this;
			chains.init = function ()
			{

			}
		}
	}
	//Controls all objects that an Account-holder can view, edit, or create.
	page.Account = new function ()
	{
		var account = this;
		account.init = function()
		{
			account.Orders.init();
			account.Profile.init();
			account.PaymentMethods.init();
			account.Addresses.init();
			account.Reviews.init();
			account.ShoppingCart.init();
		}
		account.Orders = new function()
		{
			var orders = this;
			orders.hub = $.connection.accountOrderHub;
			orders.list = ko.observableArray([]);
			orders.empty = ko.computed(function ()
			{
				if (orders.list().length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
			orders.loading = ko.observable('true');
			orders.init = function ()
			{
				orders.hub.server.index();
			}
		}
		account.Profile = new function()
		{
			var profile = this;
			profile.hub = $.connection.accountProfileHub;
			profile.loading = ko.observable('true');
			profile.init = function ()
			{
				profile.hub.server.index();
			}
		}
		account.PaymentMethods = new function()
		{
			var paymentMethods = this;
			paymentMethods.hub = $.connection.accountPaymentMethodHub;
			paymentMethods.list = ko.observableArray([]);
			paymentMethods.empty = ko.computed(function ()
			{
				if (paymentMethods.list().length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
			paymentMethods.loading = ko.observable('true');
			paymentMethods.init = function ()
			{
				paymentMethods.hub.server.index();
			}
		}
		account.Addresses = new function ()
		{
			var addresses = this;
			addresses.hub = $.connection.accountAddressHub;
			addresses.list = ko.observableArray([]);

			addresses.init = function ()
			{
				addresses.hub.server.index();
			}

			addresses.displayMode = ko.computed(function ()
			{
				return 'Address';
			});
			addresses.empty = ko.computed(function ()
			{
				if (addresses.list().length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
			addresses.loading = ko.observable(true);

			addresses.hub.client.indexBack = function (addressList)
			{
				$.map(addressList, function (address)
				{
					addresses.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State, address.ZIP));
				});
				addresses.loading(false);
				console.log(addresses.list());
			}
			addresses.hub.client.postback = function (address)
			{
				var selectedState = ko.utils.arrayFirst(Page.States.list(), function (state)
				{
					return state.rid == address.State.RID;
				});
				addresses.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, selectedState, address.ZIP));
			}
			addresses.hub.client.putBack = function (address)
			{
				var selectedAddress = ko.utils.arrayFirst(addresses.list(), function (item)
				{
					return address.RID == item.rid();
				});
				selectedAddress.nick(address.Nick);
				selectedAddress.line1(address.Line1);
				selectedAddress.line2(address.Line2);
				selectedAddress.city(address.City);
				selectedAddress.state(address.State);
				selectedAddress.zip(address.ZIP);
			}
			addresses.hub.client.deleteBack = function (address)
			{
				addresses.list.remove(function (item)
				{
					return item.rid() == address.RID;
				});
			}

			addresses.post = function (addressForm)
			{
				dataSent();
				Page.Modal.CloseModal();
				var address =
				{
					Nick: addressForm.Nick.value,
					Line1: addressForm.Line1.value,
					Line2: addressForm.Line2.value,
					City: addressForm.City.value,
					StateID: addressForm.StateID.value,
					ZIP: addressForm.ZIP.value
				}
				console.log(address);
				addresses.hub.server.post(address);
			}
			addresses.put = function (addressForm)
			{
				dataSent();
				Page.Modal.CloseModal();
				var address =
				{
					RID: addressForm.RID.value,
					Nick: addressForm.Nick.value,
					Line1: addressForm.Line1.value,
					Line2: addressForm.Line2.value,
					City: addressForm.City.value,
					StateID: addressForm.StateID.value,
					ZIP: addressForm.ZIP.value
				}
				console.log(address);
				addresses.hub.server.put(address);
			}
			addresses.delete = function (addressForm)
			{
				dataSent();
				Page.Modal.CloseModal();
				console.log(addressForm);
				addresses.hub.server.delete(addressForm.RID.value);
			}

			addresses.Create = function ()
			{
				console.log("Create Button Pressed.");
				Page.Modal.data(new AddressViewModel());
				Page.Modal.displayMode('AddressCreate');
				Page.Modal.LoadModal();
			}
			addresses.Edit = function (address)
			{
				console.log("Edit Button Pressed.");
				Page.Modal.data(address);
				Page.Modal.displayMode('AddressEdit');
				Page.Modal.LoadModal();
			}
			addresses.Remove = function (address)
			{
				console.log("Remove Button Pressed.");
				Page.Modal.data(address);
				Page.Modal.displayMode('AddressRemove');
				Page.Modal.LoadModal();
			}
		}
		account.Reviews = new function()
		{
			var reviews = this;
			reviews.hub = $.connection.accountReviewHub;
			reviews.list = ko.observableArray([]);
			reviews.empty = ko.computed(function ()
			{
				if (reviews.list().length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
			reviews.loading = ko.observable('true');
			reviews.init = function ()
			{
				reviews.hub.server.index();
			}
		}
		account.Settings = new function()
		{
			var settings = this;
			settings.hub = $.connection.accountSettingHub;
			settings.loading = ko.observable('true');
			settings.init = function()
			{
				settings.hub.server.index();
			}
		}
		account.ShoppingCart = new function()
		{
			var shoppingCart = this;
			shoppingCart.hub = $.connection.accountShoppingCartHub;
			shoppingCart.loading = ko.observable(true);
			shoppingCart.products = ko.observableArray([]);
			shoppingCart.address = ko.observable();
			shoppingCart.subtotal = ko.observable(0.00);
			shoppingCart.empty = ko.computed(function ()
			{
				if (shoppingCart.products().length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
			shoppingCart.init = function ()
			{
				shoppingCart.hub.server.index();
			}
			shoppingCart.hub.client.indexBack = function(ShoppingCart)
			{
				if (Page.Account.Addresses.list().length > 0)
				{
					shoppingCart.address(ko.utils.arrayFirst(Page.Account.Addresses.list(), function (address)
					{
						return ShoppingCart.DeliveryAddressRID == address.RID;
					}));
				}
				$.map(ShoppingCart.Products, function (product)
				{
				 	shoppingCart.products.push(new ProductViewModel());
				 });
				shoppingCart.loading(false);
				shoppingCart.subtotal = ShoppingCart.Subtotal;
				console.log(ShoppingCart);
				console.log(shoppingCart);
			}

			//todo Build Shopping Cart System for Logged Out user but allow migration upon login.
		}
	}
}

function States()
{
	var self = this;
	self.hub = $.connection.accountAddressHub;
	self.list = ko.observableArray([]);

	self.init = function ()
	{
		this.hub.server.stateList();
	}

	self.hub.client.stateListBack = function (stateList)
	{
		$.map(stateList, function (state)
		{
			self.list.push(new StateViewModel(state.RID, state.Title, state.Abbreviation));
		});
		self.list.sort(function (left, right)
		{
			if(left.title == right.title)
			{
				return 0;
			}
			else if( left.title < right.title)
			{
				return -1;
			}
			else
			{
				return 1;
			}
		});
	}
}

function Modal()
{
	var self = this;
	self.displayMode = ko.observable('ModalTest');
	self.data = ko.observable();
	self.LoadModal = function()
	{
		$('#Modal').modal('show');
		$.validator.unobtrusive.parse('#ModalContent');
	}
	self.CloseModal = function()
	{
		$('#Modal').modal('hide');
		$('body').removeClass('modal-open');
		$('.modal-backdrop').remove();
	}
}
function Account()
{
	var thisAccount = this;
	thisAccount.Addresses = new function ()
	{
		var self = this;
		self.hub = $.connection.accountAddressHub;
		self.list = ko.observableArray([]);

		self.displayMode = ko.computed(function ()
		{
			return 'Address';
		});
		self.empty = ko.computed(function ()
		{
			if (self.list().length == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		});
		self.loading = ko.observable('true');

		self.init = function ()
		{
			self.hub.server.index();
		}

		self.hub.client.indexBack = function (addressList)
		{
			$.map(addressList, function (address)
			{
				console.log(address);
				var selectedState = ko.utils.arrayFirst(Page.States.list(), function (state)
				{
					return state.rid == address.State.RID;
				});
				console.log(selectedState);
				self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, selectedState, address.ZIP));
				self.loading('false');
			});
			console.log(self.list());
		}
		self.hub.client.postback = function (address)
		{
			var selectedState = ko.utils.arrayFirst(Page.States.list(), function (state)
			{
				return state.rid == address.State.RID;
			});
			self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, selectedState, address.ZIP));
		}
		self.hub.client.putBack = function (address)
		{
			var selectedAddress = ko.utils.arrayFirst(self.list(), function (item)
			{
				return address.RID == item.rid();
			});
			selectedAddress.nick(address.Nick);
			selectedAddress.line1(address.Line1);
			selectedAddress.line2(address.Line2);
			selectedAddress.city(address.City);
			selectedAddress.state(address.State);
			selectedAddress.zip(address.ZIP);
		}
		self.hub.client.deleteBack = function(address)
		{
			self.list.remove(function (item)
			{
				return item.rid() == address.RID;
			});
		}

		self.post = function (addressForm)
		{
			dataSent();
			Page.Modal.CloseModal();
			var address =
			{
				Nick: addressForm.Nick.value,
				Line1: addressForm.Line1.value,
				Line2: addressForm.Line2.value,
				City: addressForm.City.value,
				StateID: addressForm.StateID.value,
				ZIP: addressForm.ZIP.value
			}
			console.log(address);
			self.hub.server.post(address);
		}
		self.put = function (addressForm)
		{
			dataSent();
			Page.Modal.CloseModal();
			var address =
			{
				RID: addressForm.RID.value,
				Nick: addressForm.Nick.value,
				Line1: addressForm.Line1.value,
				Line2: addressForm.Line2.value,
				City: addressForm.City.value,
				StateID: addressForm.StateID.value,
				ZIP: addressForm.ZIP.value
			}
			console.log(address);
			self.hub.server.put(address);
		}
		self.delete = function (addressForm)
		{
			dataSent();
			Page.Modal.CloseModal();
			console.log(addressForm);
			self.hub.server.delete(addressForm.RID.value);
		}

		self.Create = function ()
		{
			console.log("Create Button Pressed.");
			Page.Modal.data(new AddressViewModel());
			Page.Modal.displayMode('AddressCreate');
			Page.Modal.LoadModal();
		}
		self.Edit = function (address)
		{
			console.log("Edit Button Pressed.");
			Page.Modal.data(address);
			Page.Modal.displayMode('AddressEdit');
			Page.Modal.LoadModal();
		}
		self.Remove = function (address)
		{
			console.log("Remove Button Pressed.");
			Page.Modal.data(address);
			Page.Modal.displayMode('AddressRemove');
			Page.Modal.LoadModal();
		}
	}
	thisAccount.Orders = new function ()
	{

	}
	thisAccount.PaymentMethods = new function ()
	{

	}
	thisAccount.Settings = new function ()
	{

	}
	thisAccount.Review = new function ()
	{

	}
	thisAccount.Profiles = new function ()
	{

	}
}

function dataSent()
{
	$('.updated').hide();
	$('.updating').show();
}
$(function ()
{
	$.connection.hub.starting(function ()
	{
		$('.updated').hide();
		$('.updating').show();
	});
	$.connection.hub.received(function ()
	{
		$('.updating').hide();
		$('.updated').show();
	});
	$.connection.hub.reconnecting(function ()
	{
		$('.updated').hide();
		$('.updating').show();
	});
	$.connection.hub.reconnected(function ()
	{
		$('.updating').hide();
		$('.updated').show();
	});
	$.connection.hub.disconnected(function ()
	{
		setTimeout(function ()
		{
			$.connection.hub.start();
		}, 5000); // Restart connection after 5 seconds.
	});

	ko.applyBindings(Page);
	$.connection.hub.start(function ()
	{
		Page.init();
	});
});