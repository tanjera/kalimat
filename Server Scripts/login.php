<?php
    $dbhost = "kalimat.tanjera.com";
    include '../../kalimat.password.php';  // Database username and password stored in private folder (above web root)
    $dbname = "tanjera_kalimat";
    mysql_connect($dbhost, $dbuser, $dbpass) or die("Cant connect into database");
    mysql_select_db($dbname)or die("Cant connect into database");   
    // =============================================================================
    $Act = $_GET["Act"];  // what is action, Login or Register?
    $nick = $_GET["User"];
    $pass = $_GET["Pass"];     
    $hashpass = password_hash($pass, PASSWORD_BCRYPT);
    if($Act == "Login"){
    if(!$nick || !$pass) {
        echo "Login or password cant be empty.";
        } else {
       
                $SQL = "SELECT * FROM players WHERE username = '" . $nick . "'";
                $result_id = @mysql_query($SQL) or die("DB ERROR");
                $total = mysql_num_rows($result_id);
                    if($total) {
                        $datas = @mysql_fetch_array($result_id);
                            if(password_verify($pass, $datas["hashpass"])) {
                                echo "verified";
                            } else {
                                echo "invalid";
                            }
                    } else {
                        echo "no_user";
                }
            
    }
    }
   if($Act == "Register"){
       
        $checkuser = mysql_query("SELECT Username FROM players WHERE username ='$nick'"); 
        $username_exist = mysql_num_rows($checkuser);
        if($username_exist > 0)
        {
              echo "username_taken"; 
              unset($nick);
              exit();
        }else{
            $query = "INSERT INTO players (username, hashpass) VALUES('$nick', '$hashpass')";
            mysql_query($query) or die("ERROR");
            echo "registration_complete";
        }
    }

    mysql_close();
    ?>