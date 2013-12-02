CREATE TABLE rent_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into rent_type (id,name) VALUES 
(1,'Сдам'),
(2, 'Сниму');
