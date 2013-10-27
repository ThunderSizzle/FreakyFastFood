function AddressViewModel(rid, nick, line1, line2, city, state, stateid, zip)
{
	var self = this;
	self.rid = ko.observable(rid);
	self.nick = ko.observable(nick);
	self.line1 = ko.observable(line1);
	self.line2 = ko.observable(line2);
	self.city = ko.observable(city);
	self.state = ko.observable(state);
	self.stateid = ko.observable(stateid);
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
	var self = this;
	self.Addresses = new function()
	{
		var selfadd = this;
		selfadd.hub = $.connection.accountAddressHub;
		selfadd.list = ko.observableArray([]);

		selfadd.displayMode = ko.computed(function ()
		{
			return 'AddressTemplate';
		});
		selfadd.empty = ko.computed(function ()
		{
			if (selfadd.list().length == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		});

		selfadd.init = function ()
		{
			selfadd.hub.server.index();
		}

		selfadd.hub.client.indexBack = function (addressList)
		{
			$.map(addressList, function (address)
			{
				selfadd.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State, address.StateRID, address.ZIP));
				console.log(address);
			});
			console.log(selfadd.list());
		}
		selfadd.hub.client.postback = function (address)
		{
			selfadd.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State, address.StateRID, address.ZIP));
		}
		selfadd.hub.client.putBack = function (address)
		{
			var selectedAddress = ko.utils.arrayFirst(selfadd.list(), function (item)
			{
				return address.RID == item.rid();
			});
			selectedAddress.nick(address.Nick);
			selectedAddress.line1(address.Line1);
			selectedAddress.line2(address.Line2);
			selectedAddress.city(address.City);
			selectedAddress.state(address.State);
			selectedAddress.stateid(address.StateRID);
			selectedAddress.zip(address.ZIP);
		}
		selfadd.hub.client.deleteBack = function(address)
		{
			selfadd.list.remove(function (item)
			{
				return item.rid() == address.RID;
			});
		}

		selfadd.post = function (addressForm)
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
			selfadd.hub.server.post(address);
		}
		selfadd.put = function (addressForm)
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
			selfadd.hub.server.put(address);
		}
		selfadd.delete = function (addressForm)
		{
			dataSent();
			Page.Modal.CloseModal();
			console.log(addressForm);
			selfadd.hub.server.delete(addressForm.RID.value);
		}

		selfadd.Create = function ()
		{
			console.log("Create Button Pressed.");
			Page.Modal.data(new AddressViewModel());
			Page.Modal.displayMode('AddressCreate');
			Page.Modal.LoadModal();
		}
		selfadd.Edit = function (address)
		{
			console.log("Edit Button Pressed.");
			Page.Modal.data(address);
			Page.Modal.displayMode('AddressEdit');
			Page.Modal.LoadModal();
		}
		selfadd.Remove = function (address)
		{
			console.log("Remove Button Pressed.");
			Page.Modal.data(address);
			Page.Modal.displayMode('AddressRemove');
			Page.Modal.LoadModal();
		}
	}
}
function Addresses()
{
	var self = this;
	self.hub = $.connection.accountAddressHub;
	self.list = ko.observableArray([]);

	self.displayMode = ko.computed(function ()
	{
		return 'AddressTemplate';
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

	self.init = function ()
	{
		this.hub.server.index();
	}

	self.hub.client.indexBack = function (addressList)
	{
		$.map(addressList, function (address)
		{
			self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State, address.StateRID, address.ZIP));
			console.log(address);
		});
		console.log(self.list());
	}
	self.hub.client.postback = function (address)
	{
		self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.CityTitle, address.State, address.StateRID,  address.ZIP));
	}

	self.post = function (addressForm)
	{
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

	self.Create = function()
	{
		$parent.Modal.displayMode = '#AddressCreate';
		LoadModal(template);
	}
	self.Edit = function(address)
	{
		$parent.Modal.displayMode = '#AddressEdit';
		LoadModal(template);
	}
	self.Remove = function(address)
	{
		$parent.Modal.displayMode = '#AddressRemove';
		LoadModal(template);
	}
}
function Orders()
{

}
function PaymentMethods()
{

}
function Settings()
{

}
function Reviews()
{

}
function Profiles()
{

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