-- Create Database
CREATE DATABASE TeacherStudentPortal;
USE [TeacherStudentPortal]
--Create Tables

CREATE TABLE AdminUsers(
   Id INT IDENTITY(1, 1),
   Name varchar(30)  NOT NULL,
   Email varchar(30) NOT NULL,
   Username varchar(30) NOT NULL,
   Password varchar(30) NOT NULL  
   PRIMARY KEY( Username )
);

CREATE TABLE Teachers(
   Id INT IDENTITY(1, 1),
   Name varchar(30)  NOT NULL,
   Email varchar(30) NOT NULL,
   CourseTeaching varchar(30) NOT NULL,
   CourseId int NOT NULL,
   Username varchar(30) NOT NULL,
   Password varchar(30) NOT NULL, 
   CONSTRAINT PK_Teachers PRIMARY KEY (CourseId),
   CONSTRAINT UC_Teachers UNIQUE (Username)
);

CREATE TABLE Students(
   Id INT IDENTITY(1, 1),
   Name varchar(30)  NOT NULL,
   Email varchar(30) NOT NULL,
   StudentId int NOT NULL,
   Username varchar(30) NOT NULL,
   Password varchar(30) NOT NULL, 
   CONSTRAINT PK_Students PRIMARY KEY (StudentId),
   CONSTRAINT UC_Students UNIQUE (Username)
);

CREATE TABLE StudentGrades(
	StudentId int NOT NULL,
	CourseID int NOT NULL,
	Grade varchar(10) NOT NULL,
	FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
	FOREIGN KEY (CourseID) REFERENCES Teachers(CourseId)
)

-- Create Stored procedures
CREATE PROCEDURE [dbo].[AddNewAdmin] (
	@Name VARCHAR(30)
	,@Email VARCHAR(30)
	,@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	INSERT INTO AdminUsers
	VALUES (
		@Name
		,@Email
		,@Username
		,@Password
		)
END
GO

CREATE PROCEDURE [dbo].[AddNewStudent] (
	@Name VARCHAR(30)
	,@Email VARCHAR(30)
	,@StudentId INT
	,@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	INSERT INTO Students
	VALUES (
		@Name
		,@Email		
		,@StudentId
		,@Username
		,@Password
		)
END
GO

CREATE PROCEDURE [dbo].[AddNewTeacher] (
	@Name VARCHAR(30)
	,@Email VARCHAR(30)
	,@CourseTeaching varchar(30)
	,@CourseId int
	,@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	INSERT INTO Teachers
	VALUES (
		@Name
		,@Email
		,@CourseTeaching
		,@CourseId
		,@Username
		,@Password
		)
END
GO

CREATE PROCEDURE [dbo].[AssignGrade] (
	@StudentId INT
	,@CourseId INT
	,@Grade VARCHAR(10)
	)
AS
BEGIN
	IF EXISTS (
			SELECT *
			FROM StudentGrades
			WHERE StudentId = @StudentId
				AND CourseID = @CourseId
			)
	BEGIN
		UPDATE StudentGrades
		SET Grade = @Grade
		WHERE StudentId = @StudentId
			AND CourseID = @CourseId
	END
	ELSE
	BEGIN
		INSERT INTO StudentGrades
		VALUES (
			@StudentId
			,@CourseId
			,@Grade
			)
	END
END
GO

CREATE PROCEDURE [dbo].[GetCourseId] (@Username VARCHAR(10))
AS
BEGIN
	SELECT CourseId
	FROM Teachers
	WHERE Username = @Username
END
GO

CREATE PROCEDURE [dbo].[GetStudentCourseGrades] (@StudentId INT)
AS
BEGIN
	SELECT CourseId
		,CourseTeaching
	FROM Teachers
	ORDER BY 1 ASC

	SELECT sg.CourseId
		,CourseTeaching
		,Grade
	FROM Students st
	INNER JOIN StudentGrades sg ON st.StudentId = sg.StudentId
	INNER JOIN Teachers te ON te.CourseId = sg.CourseID
	WHERE st.StudentId = @StudentId
	ORDER BY CourseId ASC
END
GO

CREATE PROCEDURE [dbo].[GetStudentsAndTeachers]	
AS
BEGIN
	Select Name,CourseTeaching from Teachers
	Select Name from Students
END
GO

CREATE PROCEDURE [dbo].[GetStudentsList]
AS
BEGIN
	SELECT StudentId,Name
	FROM Students
END
GO

CREATE PROCEDURE [dbo].[ValidateAdminLogin] (
	@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	SELECT *
	FROM AdminUsers
	WHERE Username = @Username
		AND Password = @Password
END
GO

CREATE PROCEDURE [dbo].[ValidateStudentLogin] (
	@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	SELECT *
	FROM Students
	WHERE Username = @Username
		AND Password = @Password
END
GO

CREATE PROCEDURE [dbo].[ValidateTeacherLogin] (
	@Username VARCHAR(30)
	,@Password VARCHAR(30)
	)
AS
BEGIN
	SELECT *
	FROM Teachers
	WHERE Username = @Username
		AND Password = @Password
END
GO