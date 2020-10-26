<?php
    ini_set('display_errors', 1);
    class Route {
        // database connection
        private $conn;

        public $username;
        public $password;
        // constructor with $db as database connection
        public function __construct($db)
        {
            $this->conn = $db;
        }

        function verifyLogin(string $username, string $password)
        {
            //query
            $query =
                "SELECT username, password
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
                if ($password == $row["password"]) {
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
            print_r($userArr);
            $jsonOut = array("usernames" => $userArr);
            return $jsonOut;
        
            // set valuse to objest properties
            // $usernames = $row['username'];
        
            // return $test;
        }

        function createUser(string $username, string $password) {
            //query
            $query =
            "INSERT INTO users
            SET username = :username,
            password = :password";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // sanitizee (remove HTML tags)
            $this->username = htmlspecialchars(strip_tags($username));
            $this->password = htmlspecialchars(strip_tags($password));

            // bind values
            $stmt->bindParam(":username", $this->username, PDO::PARAM_STR);
            $stmt->bindParam(":password", $this->password, PDO::PARAM_STR);

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
    }