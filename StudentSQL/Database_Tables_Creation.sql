use master
go

create database StudentLife
go

use StudentLife
go

create table Subjects
(
    Id int identity primary key not null,
    Description nvarchar(50) not null,
)

alter table Subjects
add constraint UK_Subjects unique (Description)


create table ClassroomTaskTypes
(
    Id int identity primary key not null,
    Description nvarchar(30) not null,
)

alter table ClassroomTaskTypes
add constraint UK_ClassroomTaskTypes unique (Description)


create table ClassroomTasks
(
    Id int identity primary key not null,
	WhenDate datetime not null,
	Vote int null,
	TaskId int not null,
	SubjectId int not null
)

alter table ClassroomTasks
add constraint FK_ClassroomTasks_ClassroomTaskTypes foreign key (TaskId)
    references ClassroomTaskTypes (Id) on delete cascade on update cascade

alter table ClassroomTasks
add constraint FK_ClassroomTasks_Subjects foreign key (SubjectId)
    references Subjects (Id) on delete cascade on update cascade 

create table Homeworks
(
    Id int identity primary key not null,
    Description nvarchar(max) not null,
	StartDate datetime not null,
	EndDate datetime null,
	SubjectId int not null
)

alter table Homeworks
add constraint FK_Homeworks_Subjects foreign key (SubjectId)
    references Subjects (Id) on delete cascade on update cascade
 

