define(['backbone', 'string'], function (Backbone, string) {
	var controllers = {};

	return Backbone.Router.extend({
		defaultController: null,
		defaultAction: null,

		routes: function() {
			return {
				'dashboard': this.dispatch.bind(this, 'dashboard', 'index'),
				// default route
				'(:controller)(/:action)(/:params)': 'dispatch'
			};
		},

		dispatch: function() {
			var args = Array.prototype.slice.call(arguments, 0);
			var controllerName = args[0] || this.defaultController;
			var action = args[1] || this.defaultAction;
			var actionArgs = args.slice(2);

			var resultPromise = new Promise(function(resolve, reject) {
				var controllerPromise = this.getController(controllerName);
				controllerPromise.then(function(controller) {
					return this.executeAction(actionArgs, action, controller);
				}.bind(this)).then(function (result) {
					if (result.executeActionResult instanceof Function) {
						resolve(result.executeActionResult());
					}
					resolve(result);
				});
			}.bind(this));

			return resultPromise;
		},

		executeAction: function(args, action, controller) {
			return controller[action].apply(controller, args);
		},

		getController: function(name) {
			var controller = controllers[name];
			if (controller) {
				return new Promise(function(resolve, reject) { resolve(controller); });
			}
			return this.loadController(name);
		},

		loadController: function(name) {
			var fullName = string.format("controllers/{0}Controller", name);
			var controllerLoadPromise = new Promise(function(resolve, reject) {
				require([fullName], function(controller) { resolve(controller); }, function(err) { reject(Error("Failed to load controller")); });

			});
			return controllerLoadPromise.then(function(controller) {
				controllers[fullName] = controller;
				return controller;
			});
		}
	});
});
