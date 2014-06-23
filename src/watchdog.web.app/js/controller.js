define(['view'], function(View) {
	return {
		view: function(viewName, props, layoutName) {
			var view = new View(viewName, props, layoutName);
			return view;
		},

		partialViewPromise: function(viewName, props) {
			return new Promise(function(resolve, reject) {
				require([viewName], function(view) {
					 resolve(new view(props));
				}, function() {
					 return Error("Failed to load view " + viewName);
				});
			});
		}
	};
});
