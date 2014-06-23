define(['app', 'partialView', 'react'], function (app, partialView, React) {
	var views = {};

	function View(name, props, layout) {
		this.name = name;
		this.props = props;
		this.layout = layout;

		this.viewComponent = null;
		this.layoutComponent = null;
	};

	View.prototype.loadViewPromise = function() {
		var fullName = 'views/' + this.name;
		if (views[fullName]) {
			return new Promise(function(resolve) { resolve(views[fullName]); });
		}
		return new Promise(function(resolve) {
			require([fullName], function onViewLoaded(view) {
				views[fullName] = view;
				this.viewComponent = view;
				resolve(this.viewComponent);
			}.bind(this));
		}.bind(this));
	};

	View.prototype.loadLayoutPromise = function() {
		if (!this.layout) return new Promise(function (resolve) { resolve(true); });
		var fullName = 'views/layout/' + this.layout;
		if (views[fullName]) {
			return new Promise(function(resolve) { resolve(views[fullName]); });
		}
		return new Promise(function(resolve) {
			require([fullName], function onLayoutLoaded(view) {
				views[fullName] = view;
				this.layoutComponent = view;
				resolve(this.layoutComponent);
			}.bind(this));
		}.bind(this));
	};
	
	View.prototype.render = function(renderTarget) {
		var viewInstance = new this.viewComponent(this.props);

		if (this.layoutComponent) viewInstance = new this.layoutComponent({ content: viewInstance });

		React.renderComponent(viewInstance, renderTarget);
	};

	View.prototype.executeActionResult = function () {
		return Promise.all([this.loadLayoutPromise(), this.loadViewPromise()]).then(function() {
			var htmlContainer = document.getElementById('container');
			this.render(htmlContainer);
		}.bind(this));

		
	};

	View.renderPartial = function (controller, action, args) {
		var contentPromise = app.router.dispatch(controller, action, args);
		return new partialView({ contentPromise: contentPromise });
	};

	return View;
});
