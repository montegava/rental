ALTER TABLE images RENAME image_list;
ALTER TABLE image_list DROP FOREIGN KEY FK_images_flat;
delete FROM image_list where FLAT_ID not in (select id from flat_info);
ALTER TABLE image_list ADD CONSTRAINT FK_images_flat FOREIGN KEY (FLAT_ID) REFERENCES flat_info (ID) ON UPDATE CASCADE ON DELETE CASCADE;