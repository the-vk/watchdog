/** @jsx React.DOM */
define(['react', 'css!/css/layout/default'], function(React) {
	return React.createClass({
		propTypes: {
			content: React.PropTypes.component.isRequired
		},

		render: function render() {
			return (
				<div>
					<div className="navbar navbar-inverse navbar-fixed-top" role="navigation">
						<div className="container">
							<div className="navbar-header">
								<a className="navbar-brand" href="#">watchdog</a>
							</div>
						</div>
					</div>
					<div className="container">
						{this.props.content}
					</div>
				</div>
			);
		}
	});
});
