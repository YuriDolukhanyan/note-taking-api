CREATE DATABASE NotesDb;
USE NotesDb;

CREATE TABLE NoteItems(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    Description VARCHAR(250) NOT NULL,
	Created DATE NOT NULL,
	Updated DATE NOT NULL
);

INSERT INTO NoteItems (Description, Created, Updated) VALUES 
('Learn C# Basics', '2025-01-01', '2025-01-02'),
('Study ASP.NET WEB API', '2025-01-03', '2025-01-04'),
('Write projects using Dapper', '2025-01-05', '2025-01-06'),
('Explore LINQ and ASYNC-AWAIT', '2025-01-07', '2025-01-08'),
('Review Multithreading Concepts', '2025-01-08', '2025-01-09');

SELECT * FROM NoteItems;