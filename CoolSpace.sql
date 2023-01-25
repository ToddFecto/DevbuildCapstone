CREATE DATABASE coolspace;
USE coolspace;

CREATE TABLE user (
	id INT NOT NULL AUTO_INCREMENT,
	firstName VARCHAR(50),
	lastName VARCHAR(50),
	email VARCHAR(80),
	phone VARCHAR(10),
    userName VARCHAR(50),
	password VARCHAR(50),
	PRIMARY KEY(id)
);

CREATE TABLE favoriteApod (
	id INT NOT NULL AUTO_INCREMENT,
	date VARCHAR(11),
	userId INT NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY (userId) REFERENCES user(id)
);

CREATE TABLE favoriteRover (
	id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50),
    cameraName VARCHAR(50),
    image VARCHAR(500),
    date VARCHAR(11),
    userId INT NOT NULL,
    PRIMARY KEY(id),
    FOREIGN KEY (userId) REFERENCES user(id)	
);

insert into user (firstName, lastName, email, phone, userName, password) values ('Adam', 'Rolain', 'AdamRolain@RocketMortage.com', '8888888888', 'ARolain', 'dog1234');
insert into user (firstName, lastName, email, phone, userName, password) values ('Jessica', 'Miller-Nims', 'JessicaMiller-Nims@RocketMortage.com', '9999999999', 'JMiller-Nims', 'dog1234');
insert into user (firstName, lastName, email, phone, userName, password) values ('Todd', 'Fecto', 'ToddFecto@RocketMortage.com', '1111111111', 'TFecto', 'dog1234');