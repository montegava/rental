update flat_info set ROOM_COUNT = null where ROOM_COUNT like '--%';
update flat_info set ROOM_COUNT = null where ROOM_COUNT = '';
update flat_info set ROOM_COUNT = null where ROOM_COUNT = ' ';
update flat_info set ROOM_COUNT = null where ROOM_COUNT = 'о';
ALTER TABLE flat_info CHANGE ROOM_COUNT ROOM_COUNT INT;
update flat_info set ROOM_COUNT = null where ROOM_COUNT = 0;



update flat_info set flat_info.FLOOR = NULL where flat_info.FLOOR like '--%';
update flat_info set flat_info.FLOOR = NULL where flat_info.FLOOR like '%/%';
update flat_info set flat_info.FLOOR = NULL where flat_info.FLOOR like '% %';
update flat_info set flat_info.FLOOR = NULL where flat_info.FLOOR like '';
ALTER TABLE flat_info CHANGE FLOOR FLOOR INT;
update flat_info set flat_info.FLOOR = null where flat_info.FLOOR  = 0;


ALTER TABLE flat_info CHANGE PRICE PRICE INT;
update flat_info set flat_info.PRICE = null where flat_info.PRICE  = 0;


select  @curRow := @curRow + 1 AS row_number , f.*
from flat_info f
JOIN    (SELECT @curRow := 0) r;

