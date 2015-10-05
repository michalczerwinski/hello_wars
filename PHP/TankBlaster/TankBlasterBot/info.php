<?php
class Info{
	public $Name = "PHP_bot";
	public $Description = "I am PHP bot";
	public $GameType = "TankBlaster";
}
echo json_encode(new Info())
?>