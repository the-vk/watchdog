define(['underscore', 'controller'], function(_, controller) {
	return _.extend({}, controller, {
		index: function() {
			return this.view('dashboard/index', {}, "default");
		}
	});
});
