create database CompanyManagement
go

use CompanyManagement
go

create table Department 
(
	ID int identity(1,1),
	Name varchar(500)
)

insert into Department values ('IT')
insert into Department values ('Software Development')
insert into Department values ('Support')

create table Employee
(
	ID int identity(1,1),
	Name varchar(500),
	DepartmentID int, 
	DateOfJoining date,
	PhotoFileName varchar(500)
)

insert into Employee values ('Duy', 2, '2022-07-08', 'anonymous.png') 