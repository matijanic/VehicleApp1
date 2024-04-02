alter table "VehicleModelOwner"
add constraint fk_vehicleModel
foreign key ("VehicleModel_id")
references "VehicleModel"("VehicleModel_id")
on delete cascade
