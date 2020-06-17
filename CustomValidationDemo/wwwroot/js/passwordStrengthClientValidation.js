$(function () {
	$.validator.addMethod("passwordstrength", function (value, element, params) {
		if (params.allownull === "True" && !value)
			return true;

		if (value.length < params.minlength)
			return false;

		var totalLowers = value.replace(/[^a-z]/g, "").length;
		if (totalLowers < params.lowers)
			return false;

		var totalUppers = value.replace(/[^A-Z]/g, "").length;
		if (totalUppers < params.uppers)
			return false;

		var totalNumbers = value.replace(/[^0-9]/g, "").length;
		if (totalNumbers < params.numbers)
			return false;

		var totalSpecials = value.replace(/[^\W]/g, "").length;
		if (totalSpecials < params.specials)
			return false;

		return true;
	});

	$.validator.unobtrusive.adapters.add("passwordstrength", ["allownull", "minlength", "lowers", "uppers", "numbers", "specials"], function (options) {
		options.rules["passwordstrength"] = {
			allownull: options.params.allownull,
			minlength: options.params.minlength,
			lowers: options.params.lowers,
			uppers: options.params.uppers,
			numbers: options.params.numbers,
			specials: options.params.specials
		};
		options.messages["passwordstrength"] = options.message;
	});
})(jQuery);