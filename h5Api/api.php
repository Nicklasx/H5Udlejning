<?php
    ini_set('display_errors', 1);
    include_once "route.php";
    include_once "database.php";

    class Api {
        public $database;
        public $db;
        public $route;

        public function dbConn() {
            $this->database = new Database();
            $this->db = $this->database->getConnection();
            $this->route = new Route($this->db);
        }

        function login()
        {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData = json_decode($data);
            $username = $jsonData->username;
            $password = $jsonData->password;
            if ($login = $this->route->verifyLogin($username, $password)) {
                    $output = array("status" => $login);
                    echo json_encode($output);
            } else {
                // set responce code - 404 Not found
                http_response_code(404);

                // tell the user no users found
                echo json_encode(
                    array("message" => "failed to login.")
                );
            }
        }
    }

    //funktions liste for at vælge hvilken funktion som skal bruges i app'en
$funcList = new Api();

$func = $_SERVER['HTTP_FUNCTION'];
// echo $func;

switch ($func) {
    case 'login':
        $funcList->login();
        break;

    default:
        # code...
        break;
}