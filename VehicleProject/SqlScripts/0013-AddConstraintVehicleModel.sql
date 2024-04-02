alter table "VehicleModel"
add constraint fk_vehicleMake
foreign key ("VehicleMake_id")
references "VehicleMake"("VehicleMake_id")
on delete cascade;