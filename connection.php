<?php
	$host = "127.0.0.1";
	$user = "root";
	$password = "";
	$db = "applimobile";
	$port = 3307;

	$connection = mysqli_connect($host, $user, $password, $db, $port);

	//check the connection
	if(mysqli_connect_errno())
	{
		echo("1 : Connection failed ****************************************"); //error code #1 = connection failed
		exit();
	}

?>