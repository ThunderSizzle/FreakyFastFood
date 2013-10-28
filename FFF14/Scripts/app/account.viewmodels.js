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
		self.state = ko.observable(state);
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
});