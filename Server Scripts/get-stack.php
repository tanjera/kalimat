<?php

    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";

    $uid = $_GET["UID"];

    $conn = new mysqli($dbhost, $dbuser, $dbpass, $dbname);
    
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    } 

    $conn->set_charset("utf8");
    $sql = "SELECT * FROM stacks WHERE uid = '" . $uid . "'";
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["xml"];
        }
    }

    $conn->close();
?>