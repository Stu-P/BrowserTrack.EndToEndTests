(function(angular) {
	"use strict";
	angular.module("app", [
		"ngRoute",
		"ui.bootstrap"
	]).filter("truncate", function() {
		return function(text, length, end) {
			if (isNaN(length))
				length = 10;

			if (end === undefined)
				end = "...";

			if (text.length <= length || text.length - end.length <= length) {
				return text;
			} else {
				return String(text).substring(0, length - end.length) + end;
			}
		};
	}).filter("trusted", [
		"$sce", function($sce) {
			return function(text) {
				return $sce.trustAsHtml(text);
			};
		}
	]).filter("encodeURIComponent", function() {
		return window.encodeURIComponent;
	});
})(this.angular);