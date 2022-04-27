<?php

require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$name = $_POST["Name"];
$size = $_POST["Size"];
$distanceFrom = $_POST["DistanceFrom"];
$color = $_POST["Color"];
$rightAsc = $_POST["RightAscension"];
$declin = $_POST["Declination"];
$constell = $_POST["Constellation"];
$addedBy = $_POST["AddedBy"];

$sql = "INSERT INTO Stars VALUES (DEFAULT, '" . $name . "', '" . $size . "', '" . $distanceFrom . "', '" . $color . "', '" . $rightAsc . "', '" . $declin . "', '" . $constell . "', '" . $addedBy . "')";

$result = $conn->query($sql);

if ($result === TRUE) {
        echo "New record has been added successfully !";
     } else {
        echo "Error: " . $sql . ":-" . mysqli_error($conn);
     }

$conn->close();

?>