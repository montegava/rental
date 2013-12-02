CREATE TABLE lessor_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into lessor_type (id,name) VALUES 
(1,'Мы'),
(2, 'Агенство'),
(3,'Хозяин');