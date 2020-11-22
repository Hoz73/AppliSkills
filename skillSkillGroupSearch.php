<?php
	include('connection.php');
	

	$regex = $_POST["regex"];
	$skillGroupName = $_POST["skillGroupName"];

	// Get the right id for the skillGroup
	$queryGroupID = "SELECT * FROM skillgroup WHERE skillGroupName = '" . $skillGroupName . "';";

	$searchGroupID =  mysqli_query($connection, $queryGroupID) or die("1-Supervisor/skillSkillGroupSearch : skillgroupID search failed");

	$groupIds = mysqli_fetch_assoc($searchGroupID);

	$groupId = $groupIds["id"];

	// Get the skills from the skillGroupID
	$querySkillsId =  "SELECT * FROM skill_skillgroup WHERE idSkillGroup  = '" . $groupId . "';";

	$searchSkillsID =  mysqli_query($connection, $querySkillsId) or die("2-Supervisor/skillSkillGroupSearch : skillID search failed");

	//add search results to variable
	$skillNames = array();
	
	echo "0\t";
	while($skillIds = mysqli_fetch_assoc($searchSkillsID))
	{
		$skillNameQuery = "SELECT * FROM skill WHERE id = '" . $skillIds["idSkill"] . "' AND skillName REGEXP '^". $regex . "';";
		$skillNameResult =  mysqli_query($connection, $skillNameQuery) or die("3-Supervisor/skillSkillGroupSearch : skillName search failed");
		if(mysqli_num_rows($skillNameResult) > 0)
		{
			$fetch = mysqli_fetch_assoc($skillNameResult);
			array_push($skillNames, $fetch["skillName"]);
		}
	}
	
	$i=0;
	//argv[1] of final result
	echo (sizeof($skillNames)."\t");
	//add the contents of the array to the final result
	while($i<sizeof($skillNames))
	{
		echo ($skillNames[$i] ."\t");
		$i++;
	}





	



?>



	