define([
	'jquery',
	'underscore',
	'backbone',
	'react'
], function ($, _, Backbone, React) {
	return {
		router: null,

		run: function() {
			Backbone.history.start({ pushState: true });
		}
	};
});
