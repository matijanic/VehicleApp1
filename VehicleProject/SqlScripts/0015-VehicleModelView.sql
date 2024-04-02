create view "VehicleModelView" as
SELECT vk."Name" AS "MakeName", vm."Name" AS "ModelName", o."FirstName", o."LastName", vmo."Price", vmo."IsActive"
FROM "VehicleModelOwner" vmo
JOIN "VehicleModel" vm ON vm."Id" = vmo."VehicleModel_id"
JOIN "VehicleMake" vk ON vk."Id" = vm."MakeId"
JOIN "Owner" o ON o."Owner_id" = vmo."Owner_id"