
Create DataBase DBProvider;

use DBProvider;

--创建学生表
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[StuID] [int] NOT NULL,
	[StuAge] [int] NOT NULL,
	[StuName] [varchar](50) NOT NULL
	)

Insert Student(StuID, StuAge, StuName) values(1, 20, 'AI');
Insert Student(StuID, StuAge, StuName) values(2, 21, 'Ying');
Insert Student(StuID, StuAge, StuName) values(3, 20, 'Park');
Insert Student(StuID, StuAge, StuName) values(4, 21, 'Tony');
Insert Student(StuID, StuAge, StuName) values(5, 20, 'Charles');
Insert Student(StuID, StuAge, StuName) values(6, 21, 'Mark');
Insert Student(StuID, StuAge, StuName) values(7, 20, 'Bill');
Insert Student(StuID, StuAge, StuName) values(8, 21, 'Vincent');
Insert Student(StuID, StuAge, StuName) values(9, 20, 'William');
Insert Student(StuID, StuAge, StuName) values(10, 21, 'Joseph');
Insert Student(StuID, StuAge, StuName) values(11, 20, 'James');
Insert Student(StuID, StuAge, StuName) values(12, 21, 'Henry');
Insert Student(StuID, StuAge, StuName) values(13, 20, 'Gary');
Insert Student(StuID, StuAge, StuName) values(14, 21, 'Martin');
Insert Student(StuID, StuAge, StuName) values(15, 20, 'Betty');

select * from Student;

--创建日志表
CREATE TABLE [dbo].[Log] (  
    [Id] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY,  
    [Date] [datetime] NOT NULL,  
    [Thread] [varchar] (255) NOT NULL,  
    [Level] [varchar] (50) NOT NULL,  
    [Logger] [varchar] (255) NOT NULL,  
    [Message] [varchar] (4000) NOT NULL,  
    [Exception] [varchar] (2000) NULL  
);  