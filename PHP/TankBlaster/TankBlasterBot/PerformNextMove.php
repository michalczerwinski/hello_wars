
<?php
/// This is just a sample bot.

// Exec

$arena = loadPostData();
echo json_encode(CalculateNextMove());

// Classes

class BotArenaInfo

	{
	public $RoundNumber;
	
	public $TurnNumber;

	public $BotId;

	public $Board;

	public $BotLocation;

	public $MissileAvailableIn;

	public $OpponentLocations;

	public $Bombs;

	public $Missiles;

	public $GameConfig;

	}

class BotMove

	{
	public $Direction;

	public $Action;

	public $FireDirection;

	}

class Point

	{
	function __construct($x, $y)
		{
		$this->X = $x;
		$this->Y = $y;
		}

	public $X;

	public $Y;

	}

//Methods

function loadPostData()
	{
	$rest_json = file_get_contents("php://input");
	$result = json_decode($rest_json);
	return $result;
	}

function GetSurroundingPoints($centerLocation, $radius)
	{
	$result = array();
	for ($i = 1; $i <= $radius; $i++)
		{
		$result[] = new Point($centerLocation->X, $centerLocation->Y + $i);
		$result[] = new Point($centerLocation->X, $centerLocation->Y - $i);
		$result[] = new Point($centerLocation->X + $i, $centerLocation->Y);
		$result[] = new Point($centerLocation->X - $i, $centerLocation->Y);
		}

	return array_values(array_filter($result, "IsLocationValid"));
	}

function IsLocationValid($location)
	{
	global $arena;
	$xlen = count($arena->Board);
	$ylen = count($arena->Board[0]);
	return $location->X >= 0 && $location->X < $xlen && $location->Y >= 0 && $location->Y < $ylen;
	}

function IsInDangerZone($location)
	{
	global $arena;
	if (!IsLocationValid($location))
		{
		return true;
		}

	foreach($arena->Bombs as $bomb)
		{
		$dangerZone = GetDangerZone(ParsePoint($bomb->Location) , $bomb->ExplosionRadius);
		foreach($dangerZone as $dangerLocation)
			{
			if ($dangerLocation->X == $location->X && $dangerLocation->Y == $location->Y)
				{
				return true;
				}
			}
		}

	foreach($arena->Missiles as $missile)
		{
		$dangerZone = GetDangerZone(ParsePoint($missile->Location) , $missile->ExplosionRadius);
		foreach($dangerZone as $dangerLocation)
			{
			if ($dangerLocation->X == $location->X && $dangerLocation->Y == $location->Y)
				{
				return true;
				}
			}
		}

	if ($arena->Board[$location->X][$location->Y] != 0)
		{
		return true;
		}
	}

function GetDangerZone($centerLocation, $explosionRadius)
	{
	$result = GetSurroundingPoints($centerLocation, $explosionRadius);
	$result[] = $centerLocation;
	return $result;
	}

function AddDirectionMove($location, $direction)
	{
	$result = new Point($location->X, $location->Y);
	switch ($direction)
		{
	case 0:
		$result->Y--;
		break;

	case 1:
		$result->Y++;
		break;

	case 2:
		$result->X++;
		break;

	case 3:
		$result->X--;
		break;
		}

	return $result;
	}

function CalculateNextMove()
	{
	global $arena;
	$result = new BotMove();
	$result->Action = (mt_rand(0, 9) == 0) ? 1 : 0;
	$result->FireDirection = 0;
	$viableDirections = array_values(array_filter([0, 1, 2, 3],
	function ($input)
		{
		global $arena;
		return !IsInDangerZone(AddDirectionMove(ParsePoint($arena->BotLocation) , $input));
		}));
	if (count($viableDirections) > 0)
		{
		$result->Direction = $viableDirections[mt_rand(0, count($viableDirections) - 1) ];
		}

	return $result;
	}

function ParsePoint($str)
	{
	$arr = explode(",", $str);
	return new Point(intval($arr[0]) , intval($arr[1]));
	}

?>