CREATE TABLE bath_unit (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;

insert into bath_unit (id,name) VALUES 
(1,'Совместно'),
(2,'Раздельно'),
(3,'2 с/у'),
(4,'3 с/у'),
(5,'В доме'),
(6,'На улице');


