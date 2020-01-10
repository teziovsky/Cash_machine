//Stworzenie bazy danych
CREATE DATABASE IF NOT EXISTS Cash_machine;

//Stworzenie tabeli cards
CREATE TABLE IF NOT EXISTS Cards(
    ID INTEGER NOT NULL AUTO_INCREMENT  PRIMARY KEY,
    Card_nr BIGINT NOT NULL UNIQUE,
    Pass SMALLINT NOT NULL collate utf8_general_ci,
    acc_balance DECIMAL(18, 2) NOT NULL)
	
//Wprowadzenie danych do tabeli cards
INSERT INTO cards VALUES (1, 123456789, 1234, 10000), (2, 987654321, 4321, 5000)