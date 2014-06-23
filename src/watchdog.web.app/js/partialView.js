define(['react'], function(React) {
	return React.createClass({
		propTypes: {
			contentPromise: React.PropTypes.instanceOf(Promise).isRequired
		},

		componentWillMount: function onMount() {
			this.setState({ content: "" });
		},

		componentDidMount: function onMounted() {
			this.props.contentPromise.then(function onContentLoaded(content) {
				this.setState({ content: content });
			}.bind(this));
		},

		render: function render() {
			return (React.DOM.div(null, this.state.content));
		}
	});
});
