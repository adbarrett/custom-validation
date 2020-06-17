$(function () {
	$.validator.unobtrusive.adapters.addBool("istrue", "required");
	$.validator.unobtrusive.parse();
}(jQuery));