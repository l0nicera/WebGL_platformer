<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$playerName = $_POST['player_name'];
$score = $_POST['score'];

// Vérifiez d'abord si un enregistrement existe pour ce player_name
$query = "SELECT * FROM game_scores WHERE player_name = ?";
$stmt = $conn->prepare($query);
$stmt->bind_param("s", $playerName);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    // Un enregistrement existe, mettez à jour le score
    $updateQuery = "UPDATE game_scores SET score = ? WHERE player_name = ?";
    $updateStmt = $conn->prepare($updateQuery);
    $updateStmt->bind_param("is", $score, $playerName);
    $updateStmt->execute();
    $updateStmt->close();
    echo "Record updated successfully";
} else {
    // Aucun enregistrement, créez un nouvel enregistrement
    $insertQuery = "INSERT INTO game_scores (player_name, score) VALUES (?, ?)";
    $insertStmt = $conn->prepare($insertQuery);
    $insertStmt->bind_param("si", $playerName, $score);
    $insertStmt->execute();
    $insertStmt->close();
    echo "New record created successfully";
}

$stmt->close();
$conn->close();
?>
