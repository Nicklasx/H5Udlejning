<?php
    // ini_set('display_errors', 1);
    class Route {
        // database connection
        private $conn;

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
    }