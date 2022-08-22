
create database Pharmacy
use Pharmacy

--(this table is linked with Adduser.cs form which is in AdministratorUC folder)
--table for user insertion;
create table users(
id int identity(1,1) not null primary key,
userRole varchar(60) not null,
name varchar(259) not null,
cnic varchar(259) not null,
mobile bigint not null,
username varchar(259) unique not null,
pass varchar(259) not null
);
--truncate table users
--truncate table sold
--drop table users
select * from users
truncate table medic
--drop table users
--(this table is linked with Addmedicine form which is in Pharmacy folder)
create table medic(
id int identity(1,1) primary key,
mid varchar(250) ,
invoice varchar(2000) default 'Market',
mname varchar(250) not null,
mnumber varchar(250) not null,
mDate varchar(250) not null,
eDate varchar(250) not null,
quantity bigint not null,
perunit bigint not null,
tradeunit bigint not null,
)
truncate table medic
select * from medic;
invoice as InvoiceNo

select * from medic
--id,mid,mnumber,mDate,eDate,quantity,perunit;
--truncate table medic
select mid as MedicineID,mname as MedicineName,mnumber as MedicineNumber,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit from medic where eDate > getdate()
select mid as MedicineID,mname as MedicineName,mnumber as MedicineNumber,mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit  from medic where eDate <= getdate();
select cast(month(eDate) as varchar) + '.' + cast(year(mDate) as varchar) from medic
--used in viewmedicine;
select count(id) from medic
--select all from medic
select * from medic
select invoice as InvoiceNo,mid as MedicineID,mname as MedicineName,mnumber as  DistributerORSeller,
mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as PerUnit from medic
--view medicine when combo box is selected
select  mid as MedicineID,mname as MedicineName,mnumber as  DistributerORSeller,
mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,perunit as TradePrice ,
tradeunit as Retail from medic where mid!='' AND mname like '%ana%' ;
--negative numbers are also included
select mid as MedicineID,mname as MedicineName,mnumber as  DistributerORSeller,
mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,
perunit as TradePrice ,tradeunit as Retail ,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff  
from medic where DATEDIFF(MM,GETDATE(),eDate)<=6  ;
--only positive
select mid as MedicineID,mname as MedicineName,mnumber as  DistributerORSeller,
mDate as ManiDate,eDate as ExpiryDate,quantity as Quantity,
perunit as TradePrice ,tradeunit as Retail ,DATEDIFF(MM,GETDATE(),eDate)  as MonthsDiff  
from medic where DATEDIFF(MM,GETDATE(),eDate)<=6 AND DATEDIFF(MM,GETDATE(),eDate)!<0 AND DATEDIFF(MM,GETDATE(),eDate)!=0 ;
--Graph
select count(mname)  from medic where DATEDIFF(MM, GETDATE(), eDate)<=6  
select DATEDIFF(MM, GETDATE(), eDate) from medic 
delete from medic where DATEDIFF(MM, GETDATE(), eDate)<=0

create table sold(
id int identity(1,1) primary key not null,
invoiceno varchar(250)  not null,
invoiceD varchar(250) not null Default getdate(),
mid varchar(250) ,
mname varchar(250),
eDate varchar(250),
perunit bigint not null,
numunit bigint not null,
total bigint not null,
disc varchar(10),
Afttotal bigint not null
);

use Pharmacy
drop table sold;
truncate table sold;
select  format(getdate(),'dd-MM-yy hh:mm');
insert into sold(invoiceno,mid,mname,eDate,perunit,numunit,total,disc,Afttotal) values(7791,'7789','panadol','7-8-2022',78,8,889,'5%',700);
select * from sold;
select invoiceno from sold where MAX(invoiceno)
select MAX(invoiceno) from sold
delete  from sold where invoiceno ='Invoice number'
select invoiceno from sold order by invoiceno DESC


use Pharmacy