#!/user/bin/env node
"use strict";

var _lodash = require("lodash");

var _lodash2 = _interopRequireDefault(_lodash);

var _hapi = require("hapi");

var _hapi2 = _interopRequireDefault(_hapi);

var _hapiMongodb = require("hapi-mongodb");

var _hapiMongodb2 = _interopRequireDefault(_hapiMongodb);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var server = new _hapi2.default.Server({ port: 5333 });
//server.connection({port:5333});
server.start(function (err) {
    if (err) throw err;
    console.log("Server running at : ${server.info.uri}");
});