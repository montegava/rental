delete from images WHERE FLAT_ID not in (select ID from flat_info);

ALTER TABLE rental.images
 ADD CONSTRAINT FK_images_flat FOREIGN KEY (FLAT_ID) REFERENCES rental.flat_info (ID) ON UPDATE CASCADE ON DELETE CASCADE;
