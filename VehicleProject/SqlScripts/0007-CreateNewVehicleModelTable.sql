create table "VehicleModel"(
"VehicleModel_id" serial primary key,
"Name" varchar(50),
"Abrv" varchar(5),
"VehicleMake_id" int,
foreign key("VehicleMake_id") references "VehicleMake" ("VehicleMake_id"));