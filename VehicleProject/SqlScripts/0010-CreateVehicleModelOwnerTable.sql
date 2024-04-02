create table "VehicleModelOwner" (
"VehicleModel_id" int not null,
"Owner_id" int not null,
primary key("VehicleModel_id","Owner_id"),
foreign key ("VehicleModel_id") references "VehicleModel"("VehicleModel_id"),
foreign key ("Owner_id") references "Owner"("Owner_id") 
);