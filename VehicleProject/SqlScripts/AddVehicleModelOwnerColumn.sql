alter table "VehicleModelOwner" 
add column "Price" decimal (8,2) not null default 0.00,
add column "IsActive" boolean not null default true;


