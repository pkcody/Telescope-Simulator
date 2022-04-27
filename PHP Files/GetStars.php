<?php

require 'ConnectionSettings.php';

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT Name, Size, DistanceFrom, Color, RightAscension, Declination, Constellation FROM Stars";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  $rows = array();
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }
  echo json_encode($rows);
} else {
  echo "0";
}
$conn->close();

?>