/*
jQuery.fn.EditModal = function ()
{
	$(function ()
	{
		console.debug("TRACE 1/2 EDITFORM BEING PROCESSED");
		if ($(this).valid())
		{
			console.debug("TRACE 1 EDITFORM BEING PROCESSED");
			$(this).parents('.EditModal').modal('hide');

			$('body').removeClass('modal-open');
			$('.modal-backdrop').remove();

			var parent = $(this).parents('.subcontainer');
			$(this).parents('.subcontainer').find('.updating').show();
			$(this).parents('.subcontainer').find('.updated').hide();
			$.ajax(
			{
				url: this.action,
				type: this.method,
				data: $(this).serialize(),
				context: parent,
				success: function (result)
				{
					parent.find('.subcontent').html(result);
					parent.find('.updating').hide();
					parent.find('.updated').show();
					console.debug($(this));

				},
				error: function (xhr, textStatus, errorThrown)
				{
					$(this).parents('.subcontainer').find('.updating').hide();
					$(this).parents('.subcontainer').find('.updated').show();
					if (xhr.status == 400)
					{

					}
				}
			});
		}
		return false;
	});
};
jQuery.fn.DeleteModal = function ()
{

};
jQuery.fn.EditButton = function() {
	console.debug("Tracepoint EDITBUTTON CLICKED");
	$(this).parents('.subcontainer').find('.updating').show();
	$(this).parents('.subcontainer').find('.updated').hide();
	var parent = $(this).parents('.subcontainer');
	$.ajax(
	{
		url: this.action,
		type: this.method,
		data: $(this).serialize(),
		context: parent,
		success: function (data, textStatus, jqXHR) {
			parent.find('.Editor').html(data);
			parent.find('.updating').show();
			parent.find('.updated').hide();
			parent.find('.EditModal').modal('show');
		}
	});
};
*/