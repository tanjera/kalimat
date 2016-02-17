<?php

    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";

    $conn = new mysqli($dbhost, $dbuser, $dbpass, $dbname);

    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    } 

    $sql = "SELECT language FROM stacks";
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["language"] . "\n";
        }
    } else {
        echo "0 results";
    }

    $conn->close();
?>