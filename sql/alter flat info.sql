ALTER TABLE flat_info
 ADD bathunit_type_id INT AFTER PAYMENT,
 ADD buld_type_id INT,
 ADD state_type_id INT,
 ADD term_type_id INT,
 ADD lessor_type_id INT,
 ADD region_type_id INT,
 ADD category_type_id INT,
 ADD rent_type_id INT,
 ADD payment_type_id INT;
ALTER TABLE flat_info
 ADD CONSTRAINT FK_bathunit_type FOREIGN KEY (bathunit_type_id) REFERENCES bath_unit (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_build_type FOREIGN KEY (buld_type_id) REFERENCES build_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_state_type FOREIGN KEY (state_type_id) REFERENCES state_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_term_type FOREIGN KEY (term_type_id) REFERENCES term_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_lessor_type FOREIGN KEY (lessor_type_id) REFERENCES lessor_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_region_type FOREIGN KEY (region_type_id) REFERENCES region_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_category_type FOREIGN KEY (category_type_id) REFERENCES category_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_rent_type FOREIGN KEY (rent_type_id) REFERENCES rent_type (id) ON UPDATE CASCADE ON DELETE SET NULL,
 ADD CONSTRAINT FK_payment_type FOREIGN KEY (payment_type_id) REFERENCES payment_type (id) ON UPDATE CASCADE ON DELETE SET NULL;
