<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sqlPlayersData = "SELECT player_name, score FROM game_scores ORDER BY score DESC";
$resultPlayersData = $conn->query($sqlPlayersData);

$playersData = array();

while ($row = $resultPlayersData->fetch_assoc()) {
    $row['score'] = intval($row['score']);
    array_push($playersData, $row);
}

$sqlBestGlobal = "SELECT player_name, score FROM game_scores ORDER BY score DESC LIMIT 1";
$resultBestGlobal = $conn->query($sqlBestGlobal);

$bestGlobal = array();

if ($resultBestGlobal->num_rows > 0) {
    $bestGlobal = $resultBestGlobal->fetch_assoc();
}

$response = array(
    "playersData" => $playersData,
    "bestGlobal" => $bestGlobal
);

echo json_encode($response);

$conn->close();
?>
