
Create DataBase DBProvider;

use DBProvider;


CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
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