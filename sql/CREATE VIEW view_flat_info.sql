CREATE VIEW view_flat_info AS
  select
   flat_info.ID
  ,DATA
  ,ROOM_COUNT
  ,ADDRESS
  ,FLOOR
  ,bath_unit.name as BATH_UNIT
  ,build_type.name as BUILD
  ,FURNITURE
  ,state_type.name as STATE
  ,flat_info.NAME
  ,PRICE
  ,PHONE
  ,COMMENT
  ,CONTENT
  ,LINK
  ,term_type.name as TERM
  ,RENT_FROM
  ,RENT_TO
  ,lessor_type.name as LESSOR
  ,FRIDGE
  ,TV
  ,WASHER
  ,COOLER
  ,region_type.name as REGION
  ,EMAIL
  ,category_type.name as CATEGORY
  ,rent_type.name as TYPE
  ,payment_type.name as PAYMENT

from flat_info
left join region_type on flat_info.region_type_id = region_type.id
left join build_type on flat_info.buld_type_id = build_type.id
left join bath_unit on flat_info.bathunit_type_id = bath_unit.id
left join state_type on flat_info.state_type_id = state_type.id
left join term_type on flat_info.term_type_id = term_type.id
left join lessor_type on flat_info.lessor_type_id = lessor_type.id
left join category_type on flat_info.category_type_id = category_type.id
left join rent_type on flat_info.rent_type_id = rent_type.id
left join payment_type on flat_info.payment_type_id = payment_type.id

