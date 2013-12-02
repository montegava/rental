CREATE TABLE region_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into region_type (id,name) VALUES 
(1,'Коминтерновский'),
(2,'Центральный'),
(3,'Ж/Д'),
(4,'Левобережный'),
(5,'Советский'),
(6,'Ленинский');