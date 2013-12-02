CREATE TABLE term_type (
   id int(11) NOT NULL AUTO_INCREMENT,
   name VARCHAR(255),
  PRIMARY KEY (id)
) ENGINE = InnoDB ROW_FORMAT = DEFAULT CHARSET=utf8;


insert into term_type (id,name) VALUES 
(1,'на длительный срок'),
(2,'на сутки')