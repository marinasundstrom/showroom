/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Scripts/helpers.ts":
/*!****************************!*\
  !*** ./Scripts/helpers.ts ***!
  \****************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

window.helpers = {
    alert: function (message) { alert(message); },
    confirm: function (message) { return confirm(message); },
    prompt: function (message, _default) { return prompt(message, _default); },
    setTitle: function (title) { document.title = title; },
    scrollToTop: function () { window.scrollTo({ left: 0, top: 0, behavior: 'smooth' }); },
    scrollToBottom: function () { setTimeout(function () { return window.scrollTo({ left: 0, top: document.body.scrollHeight, behavior: 'smooth' }); }, 200); },
    select: function (element) { return element.select(); }
};


/***/ }),

/***/ "./Scripts/listbox.ts":
/*!****************************!*\
  !*** ./Scripts/listbox.ts ***!
  \****************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
window.elementHelpers = {
    focus: function (element) { return element.focus(); },
    blur: function (element) { return element.blur(); },
    select: function (element) { return element.select(); }
};
window.listbox = {
    number: 0,
    removeHandlers: {},
    init: function (listbox, ref) {
        var _this = this;
        var listener = function (event) { return __awaiter(_this, void 0, void 0, function () {
            var closestListBox;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!event.target) return [3 /*break*/, 2];
                        closestListBox = event.target.closest('.listbox');
                        if (!(listbox != closestListBox)) return [3 /*break*/, 2];
                        return [4 /*yield*/, ref.invokeMethodAsync("Blur")];
                    case 1:
                        _a.sent();
                        _a.label = 2;
                    case 2: return [2 /*return*/];
                }
            });
        }); };
        document.addEventListener("click", listener);
        var removers = [];
        removers.push(function () { return document.removeEventListener("click", listener); });
        ++this.number;
        this.removeHandlers[this.number] = removers;
        return this.number;
    },
    dispose: function (id) {
        var listener = this.removeHandlers[id];
        delete this.removeHandlers[id];
        for (var _i = 0, listener_1 = listener; _i < listener_1.length; _i++) {
            var l = listener_1[_i];
            l();
        }
    }
};


/***/ }),

/***/ "./Scripts/simplemde.ts":
/*!******************************!*\
  !*** ./Scripts/simplemde.ts ***!
  \******************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

window.simpleMdeInterop = {
    initialize: function (element, obj) {
        var editor = new SimpleMDE({
            element: element,
            autosave: true,
            forceSync: true
        });
        editor.codemirror.on("change", function () {
            obj.invokeMethodAsync("OnChanged", editor.value());
        });
    },
};


/***/ }),

/***/ 0:
/*!******************************************************************************!*\
  !*** multi ./Scripts/helpers.ts ./Scripts/listbox.ts ./Scripts/simplemde.ts ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ./Scripts/helpers.ts */"./Scripts/helpers.ts");
__webpack_require__(/*! ./Scripts/listbox.ts */"./Scripts/listbox.ts");
module.exports = __webpack_require__(/*! ./Scripts/simplemde.ts */"./Scripts/simplemde.ts");


/***/ })

/******/ });
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vd2VicGFjay9ib290c3RyYXAiLCJ3ZWJwYWNrOi8vLy4vU2NyaXB0cy9oZWxwZXJzLnRzIiwid2VicGFjazovLy8uL1NjcmlwdHMvbGlzdGJveC50cyIsIndlYnBhY2s6Ly8vLi9TY3JpcHRzL3NpbXBsZW1kZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiO1FBQUE7UUFDQTs7UUFFQTtRQUNBOztRQUVBO1FBQ0E7UUFDQTtRQUNBO1FBQ0E7UUFDQTtRQUNBO1FBQ0E7UUFDQTtRQUNBOztRQUVBO1FBQ0E7O1FBRUE7UUFDQTs7UUFFQTtRQUNBO1FBQ0E7OztRQUdBO1FBQ0E7O1FBRUE7UUFDQTs7UUFFQTtRQUNBO1FBQ0E7UUFDQSwwQ0FBMEMsZ0NBQWdDO1FBQzFFO1FBQ0E7O1FBRUE7UUFDQTtRQUNBO1FBQ0Esd0RBQXdELGtCQUFrQjtRQUMxRTtRQUNBLGlEQUFpRCxjQUFjO1FBQy9EOztRQUVBO1FBQ0E7UUFDQTtRQUNBO1FBQ0E7UUFDQTtRQUNBO1FBQ0E7UUFDQTtRQUNBO1FBQ0E7UUFDQSx5Q0FBeUMsaUNBQWlDO1FBQzFFLGdIQUFnSCxtQkFBbUIsRUFBRTtRQUNySTtRQUNBOztRQUVBO1FBQ0E7UUFDQTtRQUNBLDJCQUEyQiwwQkFBMEIsRUFBRTtRQUN2RCxpQ0FBaUMsZUFBZTtRQUNoRDtRQUNBO1FBQ0E7O1FBRUE7UUFDQSxzREFBc0QsK0RBQStEOztRQUVySDtRQUNBOzs7UUFHQTtRQUNBOzs7Ozs7Ozs7Ozs7OztBQ3RFQSxNQUFNLENBQUMsT0FBTyxHQUFHO0lBQ2IsS0FBSyxFQUFFLFVBQUMsT0FBTyxJQUFPLEtBQUssQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLENBQUM7SUFDdkMsT0FBTyxFQUFFLFVBQUMsT0FBTyxJQUFLLGNBQU8sQ0FBQyxPQUFPLENBQUMsRUFBaEIsQ0FBZ0I7SUFDdEMsTUFBTSxFQUFFLFVBQUMsT0FBTyxFQUFFLFFBQVEsSUFBSyxhQUFNLENBQUMsT0FBTyxFQUFFLFFBQVEsQ0FBQyxF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WUFDZCxTQUFTLEVBQUUsSUFBSTtTQUNsQixDQUFDLENBQUM7UUFFSCxNQUFNLENBQUMsVUFBVSxDQUFDLEVBQUUsQ0FBQyxRQUFRLEVBQUU7WUFDM0IsR0FBRyxDQUFDLGlCQUFpQixDQUFDLFdBQVcsRUFBRSxNQUFNLENBQUMsS0FBSyxFQUFFLENBQUMsQ0FBQztRQUN2RCxDQUFDLENBQUMsQ0FBQztJQUNQLENBQUM7Q0FDSixDQUFDIiwiZmlsZSI6Im1haW4uanMiLCJzb3VyY2VzQ29udGVudCI6WyIgXHQvLyBUaGUgbW9kdWxlIGNhY2hlXG4gXHR2YXIgaW5zdGFsbGVkTW9kdWxlcyA9IHt9O1xuXG4gXHQvLyBUaGUgcmVxdWlyZSBmdW5jdGlvblxuIFx0ZnVuY3Rpb24gX193ZWJwYWNrX3JlcXVpcmVfXyhtb2R1bGVJZCkge1xuXG4gXHRcdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuIFx0XHRpZihpbnN0YWxsZWRNb2R1bGVzW21vZHVsZUlkXSkge1xuIFx0XHRcdHJldHVybiBpbnN0YWxsZWRNb2R1bGVzW21vZHVsZUlkXS5leHBvcnRzO1xuIFx0XHR9XG4gXHRcdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG4gXHRcdHZhciBtb2R1bGUgPSBpbnN0YWxsZWRNb2R1bGVzW21vZHVsZUlkXSA9IHtcbiBcdFx0XHRpOiBtb2R1bGVJZCxcbiBcdFx0XHRsOiBmYWxzZSxcbiBcdFx0XHRleHBvcnRzOiB7fVxuIFx0XHR9O1xuXG4gXHRcdC8vIEV4ZWN1dGUgdGhlIG1vZHVsZSBmdW5jdGlvblxuIFx0XHRtb2R1bGVzW21vZHVsZUlkXS5jYWxsKG1vZHVsZS5leHBvcnRzLCBtb2R1bGUsIG1vZHVsZS5leHBvcnRzLCBfX3dlYnBhY2tfcmVxdWlyZV9fKTtcblxuIFx0XHQvLyBGbGFnIHRoZSBtb2R1bGUgYXMgbG9hZGVkXG4gXHRcdG1vZHVsZS5sID0gdHJ1ZTtcblxuIFx0XHQvLyBSZXR1cm4gdGhlIGV4cG9ydHMgb2YgdGhlIG1vZHVsZVxuIFx0XHRyZXR1cm4gbW9kdWxlLmV4cG9ydHM7XG4gXHR9XG5cblxuIFx0Ly8gZXhwb3NlIHRoZSBtb2R1bGVzIG9iamVjdCAoX193ZWJwYWNrX21vZHVsZXNfXylcbiBcdF9fd2VicGFja19yZXF1aXJlX18ubSA9IG1vZHVsZXM7XG5cbiBcdC8vIGV4cG9zZSB0aGUgbW9kdWxlIGNhY2hlXG4gXHRfX3dlYnBhY2tfcmVxdWlyZV9fLmMgPSBpbnN0YWxsZWRNb2R1bGVzO1xuXG4gXHQvLyBkZWZpbmUgZ2V0dGVyIGZ1bmN0aW9uIGZvciBoYXJtb255IGV4cG9ydHNcbiBcdF9fd2VicGFja19yZXF1aXJlX18uZCA9IGZ1bmN0aW9uKGV4cG9ydHMsIG5hbWUsIGdldHRlcikge1xuIFx0XHRpZighX193ZWJwYWNrX3JlcXVpcmVfXy5vKGV4cG9ydHMsIG5hbWUpKSB7XG4gXHRcdFx0T2JqZWN0LmRlZmluZVByb3BlcnR5KGV4cG9ydHMsIG5hbWUsIHsgZW51bWVyYWJsZTogdHJ1ZSwgZ2V0OiBnZXR0ZXIgfSk7XG4gXHRcdH1cbiBcdH07XG5cbiBcdC8vIGRlZmluZSBfX2VzTW9kdWxlIG9uIGV4cG9ydHNcbiBcdF9fd2VicGFja19yZXF1aXJlX18uciA9IGZ1bmN0aW9uKGV4cG9ydHMpIHtcbiBcdFx0aWYodHlwZW9mIFN5bWJvbCAhPT0gJ3VuZGVmaW5lZCcgJiYgU3ltYm9sLnRvU3RyaW5nVGFnKSB7XG4gXHRcdFx0T2JqZWN0LmRlZmluZVByb3BlcnR5KGV4cG9ydHMsIFN5bWJvbC50b1N0cmluZ1RhZywgeyB2YWx1ZTogJ01vZHVsZScgfSk7XG4gXHRcdH1cbiBcdFx0T2JqZWN0LmRlZmluZVByb3BlcnR5KGV4cG9ydHMsICdfX2VzTW9kdWxlJywgeyB2YWx1ZTogdHJ1ZSB9KTtcbiBcdH07XG5cbiBcdC8vIGNyZWF0ZSBhIGZha2UgbmFtZXNwYWNlIG9iamVjdFxuIFx0Ly8gbW9kZSAmIDE6IHZhbHVlIGlzIGEgbW9kdWxlIGlkLCByZXF1aXJlIGl0XG4gXHQvLyBtb2RlICYgMjogbWVyZ2UgYWxsIHByb3BlcnRpZXMgb2YgdmFsdWUgaW50byB0aGUgbnNcbiBcdC8vIG1vZGUgJiA0OiByZXR1cm4gdmFsdWUgd2hlbiBhbHJlYWR5IG5zIG9iamVjdFxuIFx0Ly8gbW9kZSAmIDh8MTogYmVoYXZlIGxpa2UgcmVxdWlyZVxuIFx0X193ZWJwYWNrX3JlcXVpcmVfXy50ID0gZnVuY3Rpb24odmFsdWUsIG1vZGUpIHtcbiBcdFx0aWYobW9kZSAmIDEpIHZhbHVlID0gX193ZWJwYWNrX3JlcXVpcmVfXyh2YWx1ZSk7XG4gXHRcdGlmKG1vZGUgJiA4KSByZXR1cm4gdmFsdWU7XG4gXHRcdGlmKChtb2RlICYgNCkgJiYgdHlwZW9mIHZhbHVlID09PSAnb2JqZWN0JyAmJiB2YWx1ZSAmJiB2YWx1ZS5fX2VzTW9kdWxlKSByZXR1cm4gdmFsdWU7XG4gXHRcdHZhciBucyA9IE9iamVjdC5jcmVhdGUobnVsbCk7XG4gXHRcdF9fd2VicGFja19yZXF1aXJlX18ucihucyk7XG4gXHRcdE9iamVjdC5kZWZpbmVQcm9wZXJ0eShucywgJ2RlZmF1bHQnLCB7IGVudW1lcmFibGU6IHRydWUsIHZhbHVlOiB2YWx1ZSB9KTtcbiBcdFx0aWYobW9kZSAmIDIgJiYgdHlwZW9mIHZhbHVlICE9ICdzdHJpbmcnKSBmb3IodmFyIGtleSBpbiB2YWx1ZSkgX193ZWJwYWNrX3JlcXVpcmVfXy5kKG5zLCBrZXksIGZ1bmN0aW9uKGtleSkgeyByZXR1cm4gdmFsdWVba2V5XTsgfS5iaW5kKG51bGwsIGtleSkpO1xuIFx0XHRyZXR1cm4gbnM7XG4gXHR9O1xuXG4gXHQvLyBnZXREZWZhdWx0RXhwb3J0IGZ1bmN0aW9uIGZvciBjb21wYXRpYmlsaXR5IHdpdGggbm9uLWhhcm1vbnkgbW9kdWxlc1xuIFx0X193ZWJwYWNrX3JlcXVpcmVfXy5uID0gZnVuY3Rpb24obW9kdWxlKSB7XG4gXHRcdHZhciBnZXR0ZXIgPSBtb2R1bGUgJiYgbW9kdWxlLl9fZXNNb2R1bGUgP1xuIFx0XHRcdGZ1bmN0aW9uIGdldERlZmF1bHQoKSB7IHJldHVybiBtb2R1bGVbJ2RlZmF1bHQnXTsgfSA6XG4gXHRcdFx0ZnVuY3Rpb24gZ2V0TW9kdWxlRXhwb3J0cygpIHsgcmV0dXJuIG1vZHVsZTsgfTtcbiBcdFx0X193ZWJwYWNrX3JlcXVpcmVfXy5kKGdldHRlciwgJ2EnLCBnZXR0ZXIpO1xuIFx0XHRyZXR1cm4gZ2V0dGVyO1xuIFx0fTtcblxuIFx0Ly8gT2JqZWN0LnByb3RvdHlwZS5oYXNPd25Qcm9wZXJ0eS5jYWxsXG4gXHRfX3dlYnBhY2tfcmVxdWlyZV9fLm8gPSBmdW5jdGlvbihvYmplY3QsIHByb3BlcnR5KSB7IHJldHVybiBPYmplY3QucHJvdG90eXBlLmhhc093blByb3BlcnR5LmNhbGwob2JqZWN0LCBwcm9wZXJ0eSk7IH07XG5cbiBcdC8vIF9fd2VicGFja19wdWJsaWNfcGF0aF9fXG4gXHRfX3dlYnBhY2tfcmVxdWlyZV9fLnAgPSBcIlwiO1xuXG5cbiBcdC8vIExvYWQgZW50cnkgbW9kdWxlIGFuZCByZXR1cm4gZXhwb3J0c1xuIFx0cmV0dXJuIF9fd2VicGFja19yZXF1aXJlX18oX193ZWJwYWNrX3JlcXVpcmVfXy5zID0gMCk7XG4iLCJpbnRlcmZhY2UgV2luZG93IHtcbiAgICBoZWxwZXJzOiB7XG4gICAgICAgIGFsZXJ0KG1lc3NhZ2U6IHN0cmluZyk6IHZvaWQ7XG4gICAgICAgIGNvbmZpcm0obWVzc2FnZTogc3RyaW5nKTogYm9vbGVhbjtcbiAgICAgICAgcHJvbXB0KG1lc3NhZ2U6IHN0cmluZywgX2RlZmF1bHQ6IGFueSk6IHN0cmluZyB8IG51bGw7XG4gICAgICAgIHNldFRpdGxlKHRpdGxlOiBzdHJpbmcpOiB2b2lkO1xuICAgICAgICBzY3JvbGxUb1RvcCgpOiB2b2lkO1xuICAgICAgICBzY3JvbGxUb0JvdHRvbSgpOiB2b2lkO1xuICAgICAgICBzZWxlY3QoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCk6IHZvaWQ7XG4gICAgfVxufVxuXG53aW5kb3cuaGVscGVycyA9IHtcbiAgICBhbGVydDogKG1lc3NhZ2UpID0+IHsgYWxlcnQobWVzc2FnZSk7IH0sXG4gICAgY29uZmlybTogKG1lc3NhZ2UpID0+IGNvbmZpcm0obWVzc2FnZSksXG4gICAgcHJvbXB0OiAobWVzc2FnZSwgX2RlZmF1bHQpID0+IHByb21wdChtZXNzYWdlLCBfZGVmYXVsdCksXG4gICAgc2V0VGl0bGU6ICh0aXRsZSkgPT4geyBkb2N1bWVudC50aXRsZSA9IHRpdGxlOyB9LFxuICAgIHNjcm9sbFRvVG9wOiAoKSA9PiB7IHdpbmRvdy5zY3JvbGxUbyh7IGxlZnQ6IDAsIHRvcDogMCwgYmVoYXZpb3I6ICdzbW9vdGgnIH0pOyB9LFxuICAgIHNjcm9sbFRvQm90dG9tOiAoKSA9PiB7IHNldFRpbWVvdXQoKCkgPT4gd2luZG93LnNjcm9sbFRvKHsgbGVmdDogMCwgdG9wOiBkb2N1bWVudC5ib2R5LnNjcm9sbEhlaWdodCwgYmVoYXZpb3I6ICdzbW9vdGgnIH0pLCAyMDApOyB9LFxuICAgIHNlbGVjdDogKGVsZW1lbnQpID0+IGVsZW1lbnQuc2VsZWN0KClcbn07IiwiaW50ZXJmYWNlIFdpbmRvdyB7XG4gICAgZWxlbWVudEhlbHBlcnM6IHtcbiAgICAgICAgZm9jdXMoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCk6IHZvaWQ7XG4gICAgICAgIGJsdXIoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCk6IHZvaWQ7XG4gICAgICAgIHNlbGVjdChlbGVtZW50OiBIVE1MSW5wdXRFbGVtZW50KTogdm9pZDtcbiAgICB9LFxuICAgIGxpc3Rib3g6IHtcbiAgICAgICAgbnVtYmVyOiBudW1iZXIsXG4gICAgICAgIHJlbW92ZUhhbmRsZXJzOiB7XG4gICAgICAgICAgICBba2V5OiBudW1iZXJdOiBGdW5jdGlvbltdXG4gICAgICAgIH0sXG4gICAgICAgIGluaXQobGlzdGJveDogSFRNTEVsZW1lbnQsIHJlZjogRG90TmV0LkRvdE5ldE9iamVjdCk6IG51bWJlcjtcbiAgICAgICAgZGlzcG9zZShpZDogbnVtYmVyKTogdm9pZDtcbiAgICB9XG59XG5cbmludGVyZmFjZSBFdmVudFRhcmdldCB7XG4gICAgY2xvc2VzdChzZWxlY3Rvcjogc3RyaW5nKTogRWxlbWVudDtcbn1cblxud2luZG93LmVsZW1lbnRIZWxwZXJzID0ge1xuICAgIGZvY3VzOiAoZWxlbWVudCkgPT4gZWxlbWVudC5mb2N1cygpLFxuICAgIGJsdXI6IChlbGVtZW50KSA9PiBlbGVtZW50LmJsdXIoKSxcbiAgICBzZWxlY3Q6IChlbGVtZW50KSA9PiBlbGVtZW50LnNlbGVjdCgpXG59O1xuXG53aW5kb3cubGlzdGJveCA9IHtcbiAgICBudW1iZXI6IDAsXG4gICAgcmVtb3ZlSGFuZGxlcnM6IHt9LFxuICAgIGluaXQ6IGZ1bmN0aW9uIChsaXN0Ym94LCByZWYpIHtcbiAgICAgICAgY29uc3QgbGlzdGVuZXIgPSBhc3luYyAoZXZlbnQ6IE1vdXNlRXZlbnQpID0+IHtcbiAgICAgICAgICAgIGlmIChldmVudC50YXJnZXQpIHtcbiAgICAgICAgICAgICAgICBjb25zdCBjbG9zZXN0TGlzdEJveCA9IGV2ZW50LnRhcmdldC5jbG9zZXN0KCcubGlzdGJveCcpO1xuICAgICAgICAgICAgICAgIGlmIChsaXN0Ym94ICE9IGNsb3Nlc3RMaXN0Qm94KSB7XG4gICAgICAgICAgICAgICAgICAgIGF3YWl0IHJlZi5pbnZva2VNZXRob2RBc3luYyhcIkJsdXJcIik7XG4gICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgfVxuICAgICAgICB9O1xuXG4gICAgICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJjbGlja1wiLCBsaXN0ZW5lcik7XG5cbiAgICAgICAgY29uc3QgcmVtb3ZlcnMgPSBbXTtcbiAgICAgICAgcmVtb3ZlcnMucHVzaCgoKSA9PiBkb2N1bWVudC5yZW1vdmVFdmVudExpc3RlbmVyKFwiY2xpY2tcIiwgbGlzdGVuZXIpKTtcblxuICAgICAgICArK3RoaXMubnVtYmVyO1xuICAgICAgICB0aGlzLnJlbW92ZUhhbmRsZXJzW3RoaXMubnVtYmVyXSA9IHJlbW92ZXJzO1xuICAgICAgICByZXR1cm4gdGhpcy5udW1iZXI7XG4gICAgfSxcbiAgICBkaXNwb3NlOiBmdW5jdGlvbiAoaWQpIHtcbiAgICAgICAgY29uc3QgbGlzdGVuZXIgPSB0aGlzLnJlbW92ZUhhbmRsZXJzW2lkXTtcbiAgICAgICAgZGVsZXRlIHRoaXMucmVtb3ZlSGFuZGxlcnNbaWRdXG4gICAgICAgIGZvciAobGV0IGwgb2YgbGlzdGVuZXIpXG4gICAgICAgIHtcbiAgICAgICAgICAgIGwoKTtcbiAgICAgICAgfVxuICAgIH1cbn07IiwiZGVjbGFyZSB2YXIgU2ltcGxlTURFOiBhbnk7XG5cbmludGVyZmFjZSBXaW5kb3cge1xuICAgIHNpbXBsZU1kZUludGVyb3A6IHtcbiAgICAgICAgaW5pdGlhbGl6ZShlbGVtZW50OiBIVE1MRWxlbWVudCwgb2JqOiBhbnkpOiB2b2lkO1xuICAgIH1cbn1cblxud2luZG93LnNpbXBsZU1kZUludGVyb3AgPSB7XG4gICAgaW5pdGlhbGl6ZTogKGVsZW1lbnQsIG9iaikgPT4ge1xuICAgICAgICB2YXIgZWRpdG9yID0gbmV3IFNpbXBsZU1ERSh7XG4gICAgICAgICAgICBlbGVtZW50OiBlbGVtZW50LFxuICAgICAgICAgICAgYXV0b3NhdmU6IHRydWUsXG4gICAgICAgICAgICBmb3JjZVN5bmM6IHRydWVcbiAgICAgICAgfSk7XG5cbiAgICAgICAgZWRpdG9yLmNvZGVtaXJyb3Iub24oXCJjaGFuZ2VcIiwgKCkgPT4ge1xuICAgICAgICAgICAgb2JqLmludm9rZU1ldGhvZEFzeW5jKFwiT25DaGFuZ2VkXCIsIGVkaXRvci52YWx1ZSgpKTtcbiAgICAgICAgfSk7XG4gICAgfSxcbn07Il0sInNvdXJjZVJvb3QiOiIifQ==