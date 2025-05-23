<?php

$servername = "localhost";


$dbname = "dbtest";

$username = "user1";

$password = "uzer1pass";

$api_key_value = "tPmAT5Ab3j7F9";

$api_key = $sensor = $location = $inservice = $alarm = "";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $api_key = test_input($_POST["api_key"]);
    if($api_key == $api_key_value) {
        $sensor = test_input($_POST["sensor"]);
        $location = test_input($_POST["location"]);
        $inservice = test_input($_POST["inservice"]);
        $alarm = test_input($_POST["alarm"]);
        
        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        } 
        
        $sql = "INSERT INTO LaserData (sensor, location, inservice, alarm)
        VALUES ('" . $sensor . "', '" . $location . "', '" . $inservice . "', '" . $alarm . "')";
        
        if ($conn->query($sql) === TRUE) {
            echo "New record created successfully";
        } 
        else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
    
        $conn->close();
    }
    else {
        echo "Wrong API Key provided.";
    }

}
else {
    echo "No data posted with HTTP POST.";
}

function test_input($data) {
    $data = trim($data);
    $data = stripslashes($data);
    $data = htmlspecialchars($data);
    return $data;
}