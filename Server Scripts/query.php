<?php

    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";

    $conn = new mysqli($dbhost, $dbuser, $dbpass, $dbname);
    $conn->set_charset("utf8");
    
    if ($conn->connect_error) 
    {
        die("Connection failed: " . $conn->connect_error);
    } 

    $query= $_GET["Query"];

    if ($query == "Stacklist")
    {
        $language = $_GET["Language"];

        $sql = "SELECT * FROM stacks WHERE language = '" . $language . "'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            while($row = $result->fetch_assoc()) 
            {
                echo $row["uid"] . "\n" . $row["title"] . "\n" . $row["description"] . "\n" . $row["price_points"] . "\n" . $row["price_dollars"] . "\n";
            }
        } 
        else 
        {
            echo "0 results";
        }
    }
    else if ($query == "Stack")
    {
        $uid = $_GET["UID"];

        $sql = "SELECT * FROM stacks WHERE uid = '" . $uid . "'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            while($row = $result->fetch_assoc()) 
            {
                echo $row["xml"];
            }
        }
    }
    else if ($query == "Languages")
    {
        $sql = "SELECT language FROM stacks";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            while($row = $result->fetch_assoc()) 
            {
                echo $row["language"] . "\n";
            }
        }
    }
    else if ($query == "Player")
    {
        $user = $_GET["Username"];
        
        $sql = "SELECT * FROM players WHERE username = '" . $user . "'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            while($row = $result->fetch_assoc()) 
            {
                echo $row["username"] . "\n";
                echo $row["points"] . "\n";
                echo $row["timestamp"] . "\n";
            }
        }
        else 
        {
          echo 0;
        }
    }
    else if ($query == "Player_Update")
    {
        $user = $_GET["Username"];
        $points = $_GET["Points"];
        $timestamp = $_GET["Timestamp"];

        $sql = "UPDATE players SET points = '" . $points . ", timestamp = '" . $timestamp . "' WHERE username = '" . $user . "'";
        $result = $conn->query($sql);
        echo $conn->affected_rows;

    }
    else if ($query == "Player_Points")
    {
        $user = $_GET["Username"];

        $sql = "SELECT * FROM players WHERE username = '" . $user . "'";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) 
        {
            while($row = $result->fetch_assoc()) 
            {
                echo $row["points"];
            }
        }
        else 
        {
          echo 0;
        }
    }

?>