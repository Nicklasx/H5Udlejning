<?php
    ini_set('display_errors', 1);
    class Database {

        // specify you DB credentials here:
        private $host = ''; //Hostname
        private $db_name = ''; //Db name
        private $username = ''; //Username
        private $password = ''; //Password

        // this is the public connection to the database,
        // it is public so that other parts of the database can use the connection.
        public $conn;

        // get the database connection.
        public function getConnection() {
            // sets the connection to null.
            $this->conn = null;
            try {
                // ceretes a connection to the Database.
                $this->conn = new PDO("mysql:host=" . $this->host . ";dbname=" . $this->db_name, $this->username, $this->password);
                // sets the charset to being utf8.
                $this->conn->exec("set names utf8");
            } catch (PDOException $exception) {
                // if PDO gives an exception echo that error message.
                echo "Connection error: " . $exception->getMessage();
            }
            // here we return the connection
            return $this->conn;
        }
    }