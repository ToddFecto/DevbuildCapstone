CREATE DATABASE coolspace;
USE coolspace;



CREATE TABLE apod (
id INT NOT NULL AUTO_INCREMENT,
date VARCHAR(11),
explanation VARCHAR(3000),
hdurl VARCHAR(200),
media_type VARCHAR(6),
service_version VARCHAR(200),
title VARCHAR(200),
url VARCHAR(200),
PRIMARY KEY(id)
);



CREATE TABLE user (
id INT NOT NULL AUTO_INCREMENT,
firstName VARCHAR(50),
lastName VARCHAR(50),
email VARCHAR(80),
phone VARCHAR(10),
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



CREATE TABLE camera (
id INT NOT NULL,
name VARCHAR(7),
rover_id INT NOT NULL,
full_name VARCHAR(50),
PRIMARY KEY(id)
);



CREATE TABLE rover (
id INT NOT NULL,
name VARCHAR(30),
landing_date VARCHAR(11),
launch_date VARCHAR(11),
status VARCHAR(10),
PRIMARY KEY(id)
);



CREATE TABLE marsRover (
id INT NOT NULL AUTO_INCREMENT,
sol INT NOT NULL,
cameraId INT NOT NULL,
img_src VARCHAR(200),
earth_date VARCHAR(11),
roverId INT NOT NULL,
PRIMARY KEY(id),
FOREIGN KEY (cameraId) REFERENCES camera(id),
FOREIGN KEY (roverId) REFERENCES rover(id)
);



insert into apod (date, explanation, hdurl, media_type, service_version, title, url) values ('2021-11-09','Why would you want to fake a universe? For one reason -- to better understand our real universe. Many astronomical projects seeking to learn properties of our universe now start with a robotic telescope taking sequential images of the night sky. Next, sophisticated computer algorithms crunch these digital images to find stars and galaxies and measure their properties. To calibrate these algorithms, it is useful to test them on fake images from a fake universe to see if the algorithms can correctly deduce purposely imprinted properties. The featured mosaic of fake images was created to specifically mimic the images that have appeared on NASA\'s Astronomy Picture of the Day (APOD). Only one image of the 225 images is real -- can you find it? The accomplished deceptors have made available individual fake APOD images that can be displayed by accessing their ThisIsNotAnAPOD webpage or Twitter feed. More useful for calibrating and understanding our distant universe, however, are fake galaxies -- a sampling of which can be seen at their ThisIsNotAGalaxy webpage. Astrophysicists: Browse 2,600+ codes in the Astrophysics Source Code Library', 'https://apod.nasa.gov/apod/image/2111/AIapods01_Geach_3840.jpg', 'image', 'v1', 'All of These Space Images are Fake Except One', 'https://apod.nasa.gov/apod/image/2111/AIapods01_Geach_960.jpg');
insert into user (firstName, lastName, email, phone, password) values ('Adam', 'Rolain', 'AdamRolain@RocketMortage.com', '8888888888', 'dog1234');
insert into user (firstName, lastName, email, phone, password) values ('Jessica', 'Miller-Nims', 'JessicaMiller-Nims@RocketMortage.com', '9999999999', 'dog1234');
insert into user (firstName, lastName, email, phone, password) values ('Todd', 'Fecto', 'ToddFecto@RocketMortage.com', '1111111111', 'dog1234');
insert into favoriteApod (date, userId) values ('2021-11-09', 1);
insert into camera (id, name, rover_id, full_name) values (20, 'FHAZ', 5, 'Front Hazard Avoidance Camera');
insert into rover (id, name, landing_date, launch_date, status) values (5, 'Curiosity', '2012-08-06', '2011-11-26', 'active');
insert into marsRover (sol, cameraId, img_src, earth_date, roverId) values (1000, 20, 'http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01000/opgs/edr/fcam/FLB_486265257EDR_F0481570FHAZ00323M_.JPG', '2015-05-30', 5);