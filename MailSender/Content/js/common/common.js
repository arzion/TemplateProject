function dispatchPluginCall(options) {
	var settings = $.extend(
		{
			$context: null,
			pluginName: null,
			pluginObject: null,
			arguments: null, //arguments of plugin function
			defaults: {}
		}, options);

	//try get method name and arguments
	var methodName = null;
	var methodArguments = [];
	if (typeof settings.arguments[0] === 'string') {
		if (settings.pluginObject[settings.arguments[0]]) {
			methodName = settings.arguments[0];
			methodArguments = Array.prototype.slice.call(settings.arguments, 1);
		}
		else {
			$.error('Method ' + settings.arguments[0] + ' does not exist on jQuery.' + settings.pluginName);
		}
	}

	//consider plugin settings as defaults
	var pluginSettings = $.extend({}, settings.defaults);
	if (!methodName) {
		if (typeof settings.arguments[0] === 'object') {
			//validate plugin settings
			for (var prop in settings.arguments[0]) {
				if (settings.defaults[prop] === undefined) {
					$.error(settings.pluginName + ' - Invalid option specified: "' + prop +
								'"\nPlease check your spelling and try again.');
				}
			}
		}
		pluginSettings = $.extend({}, settings.defaults, settings.arguments[0]);
	}

	var returnedSingleValue = null;
	var returnedDom = settings.$context.each(function () {
		var pluginObject = null;

		if (methodName) {
			pluginObject = $(this).data(settings.pluginName);
		}

		if (!pluginObject) {
			pluginObject = Object.create(settings.pluginObject);
			pluginObject.init(pluginSettings, this);
			$(this).data(settings.pluginName, pluginObject);
		}

		if (methodName) {
			returnedSingleValue = pluginObject[methodName].apply(pluginObject, methodArguments);
		}
	});
	if (returnedSingleValue != null && returnedSingleValue != undefined && settings.$context.length == 1) {
		return returnedSingleValue;
	}
	else {
		return returnedDom;
	}
}

if (!Array.prototype.find) {
	Object.defineProperty(Array.prototype, 'find', {
		enumerable: false,
		configurable: true,
		writable: true,
		value: function (predicate) {
			if (this == null) {
				throw new TypeError('Array.prototype.find called on null or undefined');
			}
			if (typeof predicate !== 'function') {
				throw new TypeError('predicate must be a function');
			}
			var list = Object(this);
			var length = list.length >>> 0;
			var thisArg = arguments[1];
			var value;

			for (var i = 0; i < length; i++) {
				if (i in list) {
					value = list[i];
					if (predicate.call(thisArg, value, i, list)) {
						return value;
					}
				}
			}
			return undefined;
		}
	});
}

if (!Array.prototype.filter) {
	Array.prototype.filter = function (fun) {
		var len = this.length;
		if (typeof fun != "function")
			throw new TypeError();

		var res = new Array();
		var thisp = arguments[1];
		for (var i = 0; i < len; i++) {
			if (i in this) {
				var val = this[i]; // in case fun mutates this
				if (fun.call(thisp, val, i, this))
					res.push(val);
			}
		}
		return res;
	};
}

if (!Array.prototype.any) {
	Array.prototype.any = function (fun) {
		var len = this.length;
		if (typeof fun != "function")
			throw new TypeError();

		var thisp = arguments[1];
		for (var i = 0; i < len; i++) {
			if (i in this) {
				var val = this[i]; // in case fun mutates this
				if (fun.call(thisp, val, i, this))
					return true;
			}
		}
		return false;
	};
}

if (!Date.prototype.timeNow) {
	Date.prototype.timeNow = function() {
		return ((this.getHours() < 10) ? "0" : "") + this.getHours() + ":" + ((this.getMinutes() < 10) ? "0" : "") + this.getMinutes() + ":" + ((this.getSeconds() < 10) ? "0" : "") + this.getSeconds();
	};
}