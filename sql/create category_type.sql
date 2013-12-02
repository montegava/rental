CREATE TABLE category_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into category_type (id,name) VALUES 
(1,'Квартира'),
(2,'Комната'),
(3,'Дом'),
(4,'Помещение, офис');