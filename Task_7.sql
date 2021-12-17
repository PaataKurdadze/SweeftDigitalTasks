
-- 7.  გვაქვს Teacher ცხრილი, რომელსაც აქვს შემდეგი მახასიათებლები: სახელი, გვარი, სქესი, საგანი. 
--     გვაქვს Pupil ცხრილი, რომელსაც აქვს შემდეგი მახასიათებლები: სახელი, გვარი, სქესი, კლასი. 
--     ააგეთ ნებისმიერ რელაციურ ბაზაში ისეთი დამოკიდებულება, რომელიც საშუალებას მოგვცემს, 
--     რომ მასწავლებელმა ასწავლოს რამოდენიმე მოსწავლეს და ამავდროულად მოსწავლეს ჰყავდეს რამდენიმე 
--     მასწავლებელი (როგორც რეალურ ცხოვრებაში).

-- 1. დაწერეთ sql რომელიც ააგებს შესაბამის table-ებს.
-- 2. დაწერეთ sql რომელიც დააბრუნებს ყველა მასწავლებელს, რომელიც ასწავლის მოსწავლეს, რომელის სახელია: „გიორგი“ .


CREATE DATABASE SweeftDigital_TEST

GO

--1
CREATE TABLE Teacher
(
    TeacherID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    [Subject] NVARCHAR(30) NOT NULL
)

GO

CREATE TABLE Pupil
(
    PupilID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Grade INT NOT NULL
);

GO

CREATE TABLE TeacherPupil(
    TeacherID INT NOT NULL FOREIGN KEY REFERENCES Teacher(TeacherID),
    PupilID INT NOT NUll FOREIGN KEY REFERENCES Pupil(PupilID),
    PRIMARY KEY(TeacherID, PupilID)
)



GO
--2

SELECT T.FirstName, T.LastName
FROM Teacher AS T
    INNER JOIN TeacherPupil AS TP ON T.TeacherID=TP.TeacherID
    INNER JOIN Pupil AS P ON TP.PupilID=P.PupilID
WHERE P.FirstName = N'გიორგი'










------------------------------
--GO

-- INSERT INTO Teacher
--     (FirstName, LastName, Gender, [Subject])
-- VALUES
--     (N'ლამარა', N'ლამარაშვილი', N'ქალი', N'მათემატიკა'),
--     (N'ნონა', N'ნონაშვილი', N'ქალი', N'გერმანული'),
--     (N'დალი', N'დალიშვილი', N'ქალი', N'ისტორია'),
--     (N'თამარა', N'თამარაშვილი', N'ქალი', N'მათემატიკა')

-- GO

-- INSERT INTO Pupil
--     (FirstName, LastName, Gender, Grade)
-- VALUES
--     (N'გიორგი', N'გიორგაშვილი', N'კაცი', 7),
--     (N'დათო', N'დათოშვილი', N'კაცი', 8),
--     (N'ივანე', N'ივანეშვილი', N'კაცი', 9),
--     (N'ბექა', N'ბექაშვილი', N'კაცი', 6)

-- GO

-- INSERT INTO TeacherPupil
--     (TeacherID, PupilID)
-- VALUES
--     (1, 1),
--     (1, 3),
--     (3, 1),
--     (4, 2),
--     (2, 2),
--     (2,4),
--     (3,4),
--     (4,4)

-- GO
------------------------------
