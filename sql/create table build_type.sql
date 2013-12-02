CREATE TABLE build_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into build_type (id,name) VALUES 
(1,'деревяный'),
(2,'кирпичный'),
(3,'коттедж'),
(4,'монолитный'),
(5,'панельный'),
(6,'помещение'),
(7,'частный дом'),
(8,'часть дома');