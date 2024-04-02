create table "Owner" (
"Owner_id" serial primary key,
"FirstName" varchar(20) not null,
"LastName" varchar(25) not null,
"Address" varchar (30)not null,
"DateCreated" timestamp,
"DateUpdated" timestamp
)
