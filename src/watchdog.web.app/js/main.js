require.config({
	paths: {
		backbone: 'libs/backbone/backbone',
		bootstrap: 'libs/bootstrap/bootstrap',
		jquery: 'libs/jquery/jquery',
		react: 'libs/react/react',
		underscore: 'libs/underscore/underscore'
	},
	map : {
	'*' : {
			'css': 'libs/require-css/css'
		}
	},
	shim: {
		backbone: {
			deps: ['underscore', 'jquery'],
			exports: 'Backbone'
		},
		bootstrap: {
			deps: ['jquery']
		},
		underscore: {
			exports: '_'
		}
	}
});

require(['app', 'router', 'bootstrap'], function (app, router) {
	app.router = new router();
	app.router.defaultController = 'dashboard';
	app.router.defaultAction = 'index';
	app.run();
});
