var express = require('express');

var info = {
	Name:"node.js_Bot", 
	Description:"I am NODE.JS Bot", 
	GameType:"TankBlaster"
	};
module.exports = (function() {
    var api = express.Router();

    api.get('/info', function(req, res) {
        res.writeHead(200, {"Content-Type": "application/json"});
	   var json = JSON.stringify(info);
	   res.end(json);
    });
    return api;
})();



	
	