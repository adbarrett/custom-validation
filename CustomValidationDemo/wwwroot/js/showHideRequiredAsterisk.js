$(document).ready(function () {
	bindRequiredIfMatchFields();
});

function bindRequiredIfMatchFields() {
	$('[data-val-requiredifmatch-propertyname]').each(function () {
		var targetId = $(this).attr('id');
		var requiredValues = $(this).data('val-requiredifmatch-propertyvalues').split(';');
		var matchField = $(this).data('val-requiredifmatch-propertyname');

		$('#' + matchField).unbind().on('change', function () {
			var value = $(this).val();
			showHideElement(value, requiredValues, targetId);
		});
	});
};

function showHideElement(value, showValues, targetId) {
	if ($.inArray(value, showValues) !== -1) {
		$('label[for="' + targetId + '"] > sup').removeAttr('hidden');
	} else {
		$('label[for="' + targetId + '"] > sup').attr('hidden', 'hidden');
	}
}