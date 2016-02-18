<?php

    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";

    $user = $_GET["Username"];
    $points = $_GET["Points"];

    $conn = new mysqli($dbhost, $dbuser, $dbpass, $dbname);
    
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    } 

    $conn->set_charset("utf8");
    $sql = "UPDATE players SET points = '" . $points . "' WHERE username = '" . $user . "'";
    $result = $conn->query($sql);

    echo $conn->affected_rows;

    $conn->close();
?>