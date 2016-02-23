<?php

    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";

    $user = $_GET["Username"];
    $action = $_GET["Transaction"];
    
    $conn = new mysqli($dbhost, $dbuser, $dbpass, $dbname);
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    } 
    $conn->set_charset("utf8");
    
    
    if($action == "Deposit" || $action == "Deposit_Quiz") {
      $points = $_GET["Points"];
      $timestamp = $_GET["Timestamp"];
      
      $sql = "SELECT * FROM players WHERE username = '" . $user . "'";
      $result = $conn->query($sql);

      if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            $currpoints = $row["points"];
        }
      }
      
      $sql = "UPDATE players SET points = '" . ($currpoints + $points) . "', timestamp = '" . $timestamp . "' WHERE username = '" . $user . "'";
      $result = $conn->query($sql);
      echo $conn->affected_rows;
      
      $item = $_GET["Item"];
      if (empty($item))
      {
        $sql = "INSERT INTO transactions (username, action, points) VALUES('$user', '$action', '$points')";
      }
      else
      {
        $sql = "INSERT INTO transactions (username, action, points, item) VALUES('$user', '$action', '$points', '$item')";
      }
      
      $result = $conn->query($sql);
      echo $conn->affected_rows;
      
      $conn->close();  
    }

    if($action == "Purchase_Points") {
      $points = $_GET["Points"];
      $item = $_GET["Item"];
      $timestamp = $_GET["Timestamp"];
      
      $sql = "SELECT * FROM players WHERE username = '" . $user . "'";
      $result = $conn->query($sql);

      if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            $currpoints = $row["points"];
        }
      }
      
      $sql = "UPDATE players SET points = '" . ($currpoints - $points) . "', timestamp = '" . $timestamp . "' WHERE username = '" . $user . "'";
      $result = $conn->query($sql);
      echo $conn->affected_rows;
      
      $sql = "INSERT INTO transactions (username, action, points, item) VALUES('$user', '$action', '$points', '$item')";
      $result = $conn->query($sql);
      echo $conn->affected_rows;
      
      $conn->close();  
    }
    
?>