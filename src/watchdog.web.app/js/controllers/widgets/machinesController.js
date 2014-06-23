define(['underscore', 'controller'], function(_, controller) {
	return _.extend({}, controller, {
		index: function() {
			return this.partialViewPromise("views/widgets/machines/index", null);
		}
	});
});
