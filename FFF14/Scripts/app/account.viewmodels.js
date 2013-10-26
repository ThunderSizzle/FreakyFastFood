function AddressViewModel(rid, nick, line1, line2, city, state, zip)
{
	var self = this;
	self.rid = rid;
	self.nick = nick;
	self.line1 = line1;
	self.line2 = line2;
	self.city = city;
	self.state = state;
	self.zip = zip;
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
			self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State, address.ZIP));
			console.log(address);
		});
		console.log(self.list());
	}
	self.hub.client.postback = function (address)
	{
		self.list.push(new AddressViewModel(address.RID, address.Nick, address.Line1, address.Line2, address.CityTitle, address.StateAbbreviation, address.ZIP));
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
