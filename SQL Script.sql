create database CompanyManagement
go

use CompanyManagement
go

create table Deparment 
(
	ID int identity(1,1),
	Name varchar(500)
)

insert into Deparment values ('IT')
insert into Deparment values ('Software Development')
insert into Deparment values ('Support')

create table Employee
(
	ID int identity(1,1),
	Name varchar(500),
	DepartmentID int, 
	DateOfJoining date,
	PhotoFileName varchar(500)
)

insert into Employee values ('Duy', 2, '2022-07-08', 'anonymous.png') 