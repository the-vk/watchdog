define(function() {
	if (window.string == null) window.string = {};

	var placeholderRegExp = /\{(\d+)\}/g;
	string.format = function () {
		var args = Array.prototype.slice.call(arguments);
		var format = args[0];
		var params = args.slice(1);

		var matches;

		do {
			placeholderRegExp.lastIndex = 0;
			matches = placeholderRegExp.exec(format);
			if (matches != null) {
				format = format.replace(matches[0], params[matches[1]]);
			}
		} while (matches != null);

		return format;
	};

	return string;
});
