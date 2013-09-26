delete from images WHERE FLAT_ID not in (select ID from flat_info);

ALTER TABLE images
 ADD CONSTRAINT FK_images_flat FOREIGN KEY (FLAT_ID) REFERENCES flat_info (ID) ON UPDATE CASCADE ON DELETE CASCADE;
