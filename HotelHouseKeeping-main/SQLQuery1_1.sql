create database Hotel

use Hotel

create table Member(
MemberID int identity(1,1) primary key,
Email varchar(100) not null,
Password varchar(30) not null,
Name varchar(40) not null,
Address varchar(100) not null,
Phone varchar(15) not null,
Role varchar(15) not null,
Status int not null
)

create table Room(
RoomNumber int not null primary key,
RoomStatus varchar(30) not null
)

create table Task(
TaskID int identity(1,1) primary key,
MemberID int not null references Member(MemberID),
RoomNumber int not null references Room(RoomNumber),
DateCreate datetime not null,
MemberName varchar(40) not null,
TaskStatus varchar(30) not null
)

create table Service(
ServiceID int identity(1,1) primary key,
Name varchar(40) not null,
Price money not null,
Quantity int not null,
Status int not null
)

create table ServiceDetail(
ServiceDetailID int identity(1,1) primary key,
ServiceID int not null references Service(ServiceID),
RoomNumber int not null references Room(RoomNumber),
Quantity int not null,
Price money not null,
CreateDate	date not null
)

insert into Member values('tai@gmail.com','tai','Nguyen Ba Nhat Tai','220 Vo Van Hat, Quan 9, TPCM','0835537718','manager','1')
insert into Member values('manh@gmail.com','manh','Nguyen Quoc Manh','220 Vo Van Hat, Quan 9, TPCM','0835537718','housekeeper','1')
insert into Member values('kiet@gmail.com','kiet','Truong Anh Kiet','220 Vo Van Hat, Quan 9, TPCM','0835537718','housekeeper','1')
insert into Room values('001','Dirty')
insert into Room values('002','Clean')
insert into Room values('101','Dirty')
insert into Room values('201','Dirty')
insert into Task values('2','1','2022/10/22','Nguyen Quoc Manh','Done')
insert into Task values('2','2','2022/10/22','Nguyen Quoc Manh','Processing')
insert into Service values('Nuoc ngot', '20000',10,1)
insert into Service values('Nuoc suoi','10000',10,1)
insert into Service values('Khan lanh','7000',30,1)
insert into Service values('Bia','30000', 15,1)
insert into Service values('Giat,ui quan ao','20000', 0,1)
insert into ServiceDetail values('1','1','2','40000','2022/10/22')
insert into ServiceDetail values('2','1','1','10000','2022/10/22')
insert into ServiceDetail values('2','2','3','10000','2022/10/10')

