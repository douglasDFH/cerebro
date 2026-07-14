<?php
/**
 * Database Configuration
 * This file handles the database connection for Banco Camba.
 */
class Database {
    private $host = 'localhost';
    private $db_name = 'banco_camba';
    private $username = 'root';
    private $password = '';
    private $conn;
    
    /**
     * Connect to the database
     * @return PDO connection object
     */
    public function connect() {
        $this->conn = null;
        
        try {
            $this->conn = new PDO(
                'mysql:host=' . $this->host . ';dbname=' . $this->db_name,
                $this->username,
                $this->password
            );
            $this->conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            $this->conn->exec("SET NAMES utf8");
        } catch(PDOException $e) {
            echo 'Connection Error: ' . $e->getMessage();
        }
        
        return $this->conn;
    }
}
?>