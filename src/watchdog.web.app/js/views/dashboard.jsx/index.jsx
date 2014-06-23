/** @jsx React.DOM */
define(['react', 'view'], function(React, View) {
	return React.createClass({
		render: function render() {
			return (
				<div>
					<h1>Hello world</h1>
					{ View.renderPartial('widgets/machines', 'index', null)}
				</div>
			);
		}
	});
});
