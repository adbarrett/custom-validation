$.validator.addMethod("requiredifmatch",
	function(value, element, params) {
		var thisPropertyFullName = $(element).attr('name');
		var nameParts = thisPropertyFullName.split('.');
		var thisPropertyName = nameParts[nameParts.length - 1];
		var otherPropertyFullName = thisPropertyFullName.replace(thisPropertyName, params.propertyname);

		var actualPropertyValue = $('[name="' + otherPropertyFullName + '"]').val();
		var propertyValues = params.propertyvalues.split(';');

		if ($.inArray(actualPropertyValue, propertyValues) === -1)
			return true;

		//#region Require method taken from jQuery Validation Plugin v1.17.0
		if (element.nodeName.toLowerCase() === "select") {

			// Could be an array for select-multiple or a string, both are fine this way
			var val = $(element).val();
			return val && val.length > 0;
		}
		if (this.checkable(element)) {
			return this.getLength(value, element) > 0;
		}
		return value.length > 0;
		//#endregion Taken from jQuery Validation Plugin v1.17.0
	});

$.validator.unobtrusive.adapters.add("requiredifmatch",
	["propertyname", "propertyvalues"],
	function(options) {
		options.rules["requiredifmatch"] = {
			propertyname: options.params.propertyname,
			propertyvalues: options.params.propertyvalues
		};
		options.messages["requiredifmatch"] = options.message;
	});
