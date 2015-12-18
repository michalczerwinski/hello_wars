var express = require('express');
var bodyParser = require('body-parser');

var arena = new Object();

module.exports = (function() {
    var api = express.Router();
	api.use(bodyParser.json());
    api.post('/PerformNextMove', function(req, res) {
		arena = req.body;
		var result = CalculateNextMove()
		res.writeHead(200, {"Content-Type": "application/json"});
		var json = JSON.stringify(result);
		res.end(json);
    });
    return api;
})();

function CalculateNextMove()
{
	var result = new Object(); 
	
	if(arena.MissileAvailableIn == 0 &&  Math.floor(Math.random() * 10) == 0){
		result.Action = 2;
		result.FireDirection = Math.floor(Math.random() * 4);
	}
	else if(Math.floor(Math.random() * 10 ) == 0) {
		result.Action = 1;
		result.FireDirection = 0;
	}
	else{
		result.Action = 0;
		result.FireDirection = 0;
	}
	
	result.Direction = GetDirection();

	return result;
}

function GetDirection(){
	var directions = [0, 1, 2, 3];
	var availableDirections = new Array();
	
	for(var i = 0; i < directions.length; i++){
		if(!IsInDangerZone(AddDirectionMove(ParsePoint(arena.BotLocation) , directions[i]))){
			availableDirections.push(directions[i]);
		}
	}
	
	return availableDirections[Math.floor(Math.random() * availableDirections.length)];
}

function GetSurroundingPoints(centerLocation, radius){
	var locations = new Array();
	for (var i = 1; i <= radius; i++){
		locations.push({ X:centerLocation.X, Y:centerLocation.Y + i});
		locations.push({ X:centerLocation.X, Y:centerLocation.Y - i}); 
		locations.push({ X:centerLocation.X + i, Y:centerLocation.Y}); 
		locations.push({ X:centerLocation.X - i, Y:centerLocation.Y});		
	}
	return ValidateMultipleLocations(locations);
}

function ValidateMultipleLocations(locations){
	var result = new Array();
	for (var i = 0; i < locations.length; i++){
		if(IsLocationValid(locations[i])){
			result.push(locations[i]);
		}			
	}
	return result;
}

function IsLocationValid(location){
	xlen = arena.Board.length;
	ylen = arena.Board[0].length;
	var result = location.X >= 0 && location.X < xlen && location.Y >= 0 && location.Y < ylen;
	return result;
}

function IsInDangerZone(location){
	if (!IsLocationValid(location)){
		return true;
	}
	
	for(var i = 0; i < arena.Bombs.length; i++){
		var dangerZone = GetDangerZone(ParsePoint(arena.Bombs[i].Location) , arena.Bombs[i].ExplosionRadius);
			for(var j = 0; j < dangerZone.length; j++){
				if (dangerZone[j].X == location.X && dangerZone[j].Y == location.Y){
					return true;
				}
			}
	}
	
	for(var i = 0; i < arena.Missiles.length; i++){
		var dangerZone = GetDangerZone(ParsePoint(arena.Missiles[i].Location) , arena.Missiles[i].ExplosionRadius);
		for(var j = 0; j < dangerZone.length; j++){
			if (dangerZone[j].X == location.X && dangerZone[j].Y == location.Y){
				return true;
			}
		}
	}

	if (arena.Board[location.X][location.Y] != 0){
		return true;
	}
	return false;
}
	
function GetDangerZone(centerLocation, explosionRadius){
	var result = GetSurroundingPoints(centerLocation, explosionRadius);
	result.push(centerLocation);
	return result;
}

function AddDirectionMove(location, direction){
	var result = { X: location.X, Y: location.Y};
	switch (direction){
		case 0:
			result.Y--;
			break;

		case 1:
			result.Y++;
			break;

		case 2:
			result.X++;
			break;

		case 3:
			result.X--;
			break;
	}

	return result;
}

function ParsePoint(str){
	var arr = str.split(",");
	return {X: parseInt(arr[0]) , Y: parseInt(arr[1])};
}
	