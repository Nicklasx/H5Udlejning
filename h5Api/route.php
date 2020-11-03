<?php
    ini_set('display_errors', 1);
    class Route {
        // database connection
        private $conn;

        public $username;
        public $password;
        public $usersType;
        public $eventName;
        public $name;
        public $phone;
        public $street;
        public $streetNumber;
        public $postal;
        public $info;
        // constructor with $db as database connection
        public function __construct($db)
        {
            $this->conn = $db;
        }

        function verifyLogin(string $username, string $password)
        {
            //query
            $query =
                "SELECT username, password, usersType
                    FROM users
                    WHERE username = '" . $username . "'";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));
            $this->password = htmlspecialchars(strip_tags($password));

            //bind values
            $stmt->bindParam(":brugenavn", $this->username, PDO::PARAM_INT);

            //execute query
            if ($stmt->execute()) {
                //get retrived row
                $row = $stmt->fetch(PDO::FETCH_ASSOC);
                if ($username == $row['username'] && $password == $row["password"]) {
                    // if ($row['usersType'] == 0 || $row['usersType'] == 1) {
                    //     $output = array("status" => "true", "type" => $row['usersType']);
                    //     // echo json_encode($output);
                    //     return $output;
                    // }
                    return true;
                } else {
                    return false;
                }
                //set values to obiest properties

            }
        }

        function selectUsers() {

            // query
            $query =
                "SELECT *
                FROM users";
        
            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // execute query
            $stmt->execute();

            // get retrived row
            $userArr = array();
            while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
                array_push($userArr, $row['username']);
            }
            // print_r($userArr);
            $jsonOut = array("usernames" => $userArr);
            return $jsonOut;
        
            // set valuse to objest properties
            // $usernames = $row['username'];
        
            // return $test;
        }

        function selectOneUser(string $username) {

            // query
            $query =
            "SELECT username
            FROM users
            WHERE username = :username";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));

            // bind values
            $stmt->bindParam(":username", $this->username, PDO::PARAM_STR);

            // execute query
            $stmt->execute();

            // get retrived row
            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            // set valuse to objest properties
            $username = $row['username'];

            return $username;
        }

        function selectInfo(string $username, string $info) {

            // query
            $query =
            "SELECT $info
            FROM users
            WHERE username = :username";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));
            $this->info = htmlspecialchars(strip_tags($info));

            // bind values
            $stmt->bindParam(":username", $this->username, PDO::PARAM_STR);
            // $stmt->bindParam(":info", $this->info, PDO::PARAM_STR);

            // execute query
            $stmt->execute();

            // get retrived row
            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            // set valuse to objest properties
            $info = $row["$info"];

            return $info;
        }

        function createUser(string $username, string $password, string $name, string $phone, string $usersType) {
            //query
            $query =
            "INSERT INTO users
            SET username = :username,
            password = :password,
            name = :name,
            phone = :phone,
            usersType = :usersType
            ON DUPLICATE KEY UPDATE
            password = :password,
            name = :name,
            phone = :phone,
            usersType = :usersType";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));
            $this->password = htmlspecialchars(strip_tags($password));
            $this->name = htmlspecialchars(strip_tags($name));
            $this->phone = htmlspecialchars(strip_tags($phone));
            $this->usersType = htmlspecialchars(strip_tags($usersType));

            // bind values
            $stmt->bindParam(":username", $this->username, PDO::PARAM_STR);
            $stmt->bindParam(":password", $this->password, PDO::PARAM_STR);
            $stmt->bindParam(":name", $this->name, PDO::PARAM_STR);
            $stmt->bindParam(":phone", $this->phone, PDO::PARAM_STR);
            $stmt->bindParam(":usersType", $this->usersType, PDO::PARAM_STR);

            // execute query
            if ($stmt->execute()) {
                return "user created";
            } else {
                return "user not created";
            }
        }

        function deleteUser(string $username) {

            // query
            $query =
                "DELETE FROM users
                WHERE username = :username";
        
            // prepare query statement
            $stmt = $this->conn->prepare($query);
        
            // sanitize (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));
        
            $stmt->bindParam(":username", $this->username, PDO::PARAM_STR);
        
            // execute the query
            if ($stmt->execute()) {
                return "user deleted";
            } else {
                return "user not deleted";
            }
        }

        function getEvents() {
            // query
            $query =
                "SELECT *
                FROM Events";
        
            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // execute query
            $stmt->execute();

            // get retrived row
            $eventArr = array();
            while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
                array_push($eventArr, $row['eventName']);
            }
            // print_r($eventArr);
            $jsonOut = array("events" => $eventArr);
            return $jsonOut;
        
            // set valuse to objest properties
            // $usernames = $row['username'];
        
            // return $test;
        }

        function createEvent(string $eventName, string $name, int $phone, string $street, string $streetNumber, string $postal) {
            //query
            $query =
            "INSERT INTO Events
            SET eventName = :eventName,
            name = :name,
            phone = :phone,
            street = :street,
            streetNumber = :streetNumber,
            postal = :postal";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->eventName = htmlspecialchars(strip_tags($eventName));
            $this->name = htmlspecialchars(strip_tags($name));
            $this->phone = htmlspecialchars(strip_tags($phone));
            $this->street = htmlspecialchars(strip_tags($street));
            $this->streetNumber = htmlspecialchars(strip_tags($streetNumber));
            $this->postal = htmlspecialchars(strip_tags($postal));

            // bind values
            $stmt->bindParam(":eventName", $this->eventName, PDO::PARAM_STR);
            $stmt->bindParam(":name", $this->name, PDO::PARAM_STR);
            $stmt->bindParam(":phone", $this->phone, PDO::PARAM_STR);
            $stmt->bindParam(":street", $this->street, PDO::PARAM_STR);
            $stmt->bindParam(":streetNumber", $this->streetNumber, PDO::PARAM_STR);
            $stmt->bindParam(":postal", $this->postal, PDO::PARAM_STR);

            // execute query
            if ($stmt->execute()) {
                return "event created";
            } else {
                return "event not created";
            }
        }

        function redigerEvent(string $eventName, string $name, int $phone, string $street, string $streetNumber, string $postal) {
            //query
            $query =
            "UPDATE Events
            SET eventName = :eventName,
            name = :name,
            phone = :phone,
            street = :street,
            streetNumber = :streetNumber,
            postal = :postal
            WHERE eventName = :eventName";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->eventName = htmlspecialchars(strip_tags($eventName));
            $this->name = htmlspecialchars(strip_tags($name));
            $this->phone = htmlspecialchars(strip_tags($phone));
            $this->street = htmlspecialchars(strip_tags($street));
            $this->streetNumber = htmlspecialchars(strip_tags($streetNumber));
            $this->postal = htmlspecialchars(strip_tags($postal));

            // bind values
            $stmt->bindParam(":eventName", $this->eventName, PDO::PARAM_STR);
            $stmt->bindParam(":name", $this->name, PDO::PARAM_STR);
            $stmt->bindParam(":phone", $this->phone, PDO::PARAM_STR);
            $stmt->bindParam(":street", $this->street, PDO::PARAM_STR);
            $stmt->bindParam(":streetNumber", $this->streetNumber, PDO::PARAM_STR);
            $stmt->bindParam(":postal", $this->postal, PDO::PARAM_STR);

            // execute query
            if ($stmt->execute()) {
                return "event redigeret";
            } else {
                return "event not redigeret";
            }
        }

        function deleteEvent(string $eventName) {

            // query
            $query =
                "DELETE FROM Events
                WHERE eventName = :eventName";
        
            // prepare query statement
            $stmt = $this->conn->prepare($query);
        
            // sanitize (remove HTML tags)
            $this->eventName = htmlspecialchars(strip_tags($eventName));
        
            $stmt->bindParam(":eventName", $this->eventName, PDO::PARAM_STR);
        
            // execute the query
            if ($stmt->execute()) {
                return "event deleted";
            } else {
                return "event not deleted";
            }
        }

        function copyEvent(string $eventName) {
            //query
            $query =
            "INSERT INTO Events
            (eventName,
            name,
            phone,
            street,
            streetNumber,
            postal)
            SELECT 
            eventName,
            name,
            phone,
            street,
            streetNumber,
            postal
            FROM Events
            WHERE eventName = :eventName
            AND id = (SELECT MAX(id) FROM Events WHERE eventName = :eventName)";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->eventName = htmlspecialchars(strip_tags($eventName));

            // bind values
            $stmt->bindParam(":eventName", $this->eventName, PDO::PARAM_STR);

            // execute query
            if ($stmt->execute()) {
                return "event copyed";
            } else {
                return "event not copyed";
            }
        }
    }