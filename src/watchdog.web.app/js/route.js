define([], function () {
	function Route(pattern, defaults) {
		this.pattern = pattern;
		this.defaults = defaults;

		var parseResult = this.parse(this.pattern);
		for (var i = 0; i < parseResult.defaults.length; ++i) {
			parseResult.defaults[i].value = this.defaults[parseResult.defaults[i].name];
		}
		this.defaults = parseResult.defaults;
		this.regex = parseResult.regex;
	};

	Route.prototype.match = function(url) {
		var regex = new RegExp(this.regex);
		var match = regex.exec(url);
		if (match == null) return null;
		var result = {};
		for (var i = 0; i < this.defaults.length; ++i) {
			if (match[i + 1] != null) {
				result[this.defaults[i].name] = match[i + 1];
			} else {
				result[this.defaults[i].name] = this.defaults[i].value;
			}
		}
		return result;
	};

	Route.prototype.parse = function(pattern) {
		var regex = new RegExp("{([^}]+}", "g");
		var result = {
			regex: null,
			defaults: []
		};

		while (true) {
			var match = regex.exec(pattern);
			if (match == null) break;
			result.defaults.push({
				name: match[1],
				value: null
			});
			pattern = pattern.replace(string.format("{{0}}", result.defaults[result.defaults.length - 1].name));
		}
	};

	return Route;
});
