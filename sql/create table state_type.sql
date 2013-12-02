CREATE TABLE state_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into state_type (id,name) VALUES 
(1,'плохое'),
(2,'cреднее'),
(3,'Отличное'),
(4,'нужен ремонт'),
(5,'После ремонта'),
(6,'евроремонт'),
(7,'косметический ремонт'),
(8,'от застройщика');

