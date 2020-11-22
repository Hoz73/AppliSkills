<?php
	include('connection.php');

	$regex = $_POST["regex"];
	$userID = $_POST["userID"];

	$checkSearchQuery = "SELECT * FROM skillgroup WHERE skillGroupName REGEXP '^" . $regex . "' AND supervisor = '". $userID ."';";
	//$checkSearchQuery = "SELECT * FROM skillgroup WHERE skillGroupName REGEXP '^" . $regex . "';";

	$searchCheck = mysqli_query($connection, $checkSearchQuery) or die("1-search : supervisor skillgroup search failed");

	$stack = array();
	$i=0;

	echo "0\t";

	//add search results to stack
	while($rows = mysqli_fetch_assoc($searchCheck))
	{
		array_push($stack, $rows["skillGroupName"]);
	}

	//argv[1] of final result
	echo (sizeof($stack)."\t");

	//add the contents of the array to the final result
	while($i<sizeof($stack))
	{
		echo ($stack[$i] ."\t");
		$i++;
	}





	



?>



	