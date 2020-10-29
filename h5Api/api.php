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

        function login() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData = json_decode($data);
            $username = $jsonData->username;
            $password = $jsonData->password;
            if($login = $this->route->verifyLogin($username, $password)) {
                    // $output = array("status" => $login);
                    echo json_encode($login);
            } else {
                // set responce code - 404 Not found
                http_response_code(404);

                // tell the user no users found
                echo json_encode(
                    array("message" => "failed to login.")
                );
            }
        }

        function selectUsers() {
            $this->dbConn();
            // $data = file_get_contents('php://input', true);
            // $jsonData =  json_decode($data);
            // $username = $jsonData->username;
            if($select = $this->route->selectUsers()) {
                echo json_encode($select);
            } else {
                // set responce code - 404 Not found
                http_response_code(404);

                // tell the user no users found
                echo json_encode(
                    array("message" => "failed to login.")
                );
            }
        }

        function createUser() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $username = $jsonData->username;
            $password = $jsonData->password;
            $type = $jsonData->type;
            if($createUser = $this->route->createUser($username, $password, $type)) {
                $output = array("status" => $createUser);
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

        function deleteUser() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $username = $jsonData->username;
            if($deleteUser = $this->route->deleteUser($username)) {
                $output = array("status" => $deleteUser);
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
        
        function getEvents() {
            $this->dbConn();
            if($getEvents = $this->route->getEvents()) {
                // $output = array("events" => $getEvents);
                echo json_encode($getEvents);
            } else {
                // set responce code - 404 Not found
                http_response_code(404);

                // tell the user no users found
                echo json_encode(
                    array("message" => "failed to login.")
                );
            }
        }

        function createEvent() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $eventName = $jsonData->eventName;
            $name = $jsonData->name;
            $phone = $jsonData->phone;
            $street = $jsonData->street;
            $streetNumber = $jsonData->streetNumber;
            $postal = $jsonData->postal;
            if($createEvent = $this->route->createEvent($eventName, $name, $phone, $street, $streetNumber, $postal)) {
                $output = array("status" => $createEvent);
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

        function redigerEvent() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $eventName = $jsonData->eventName;
            $name = $jsonData->name;
            $phone = $jsonData->phone;
            $street = $jsonData->street;
            $streetNumber = $jsonData->streetNumber;
            $postal = $jsonData->postal;
            if($redigerEvent = $this->route->redigerEvent($eventName, $name, $phone, $street, $streetNumber, $postal)) {
                $output = array("status" => $redigerEvent);
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

        function deleteEvent() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $eventName = $jsonData->eventName;
            if($deleteEvent = $this->route->deleteEvent($eventName)) {
                $output = array("status" => $deleteEvent);
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

        function copyEvent() {
            $this->dbConn();
            $data = file_get_contents('php://input', true);
            $jsonData =  json_decode($data);
            $eventName = $jsonData->eventName;
            if($copyEvent = $this->route->copyEvent($eventName)) {
                $output = array("status" => $copyEvent);
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
$data = file_get_contents('php://input', true);
$jsonData =  json_decode($data);
$func = $jsonData->function;
// $func = $_SERVER['HTTP_FUNCTION'];
// echo $func;

switch ($func) {
    case 'login':
        $funcList->login();
        break;
    case 'selectUsers':
        $funcList->selectUsers();
        break;
    case 'createUser':
        $funcList->createUser();
        break;
    case 'deleteUser':
        $funcList->deleteUser();
        break;
    case 'getEvents':
        $funcList->getEvents();
        break;
    case 'createEvent':
        $funcList->createEvent();
        break;
    case 'redigerEvent':
        $funcList->redigerEvent();
        break;
        case 'deleteEvent':
            $funcList->deleteEvent();
            break;
        case 'copyEvent':
            $funcList->copyEvent();
            break;
    default:
        # code...
        break;
}