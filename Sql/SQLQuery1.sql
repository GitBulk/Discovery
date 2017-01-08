CREATE TABLE #Employee
(
	Id INT,
	Salary INT,
	DepId INT
)

CREATE TABLE #Department
(
	Id INT,
	DepNam NVARCHAR(100)
)

INSERT INTO #Department VALUES(1, 'IT')
INSERT INTO #Department VALUES(2, 'HR')
INSERT INTO #Department VALUES(3, 'Marketing')
INSERT INTO #Department VALUES(4, 'it')

SELECT * FROM #Department WHERE DepNam LIKE '%it%'

INSERT INTO #Employee VALUES(1, 1000, 1)
INSERT INTO #Employee VALUES(2, 2000, 1)
INSERT INTO #Employee VALUES(3, 3000, 2)
INSERT INTO #Employee VALUES(4, 2000, 2)

-- Question 1: SQL Query to find second highest salary of Employee

SELECT * FROM #Employee WHERE Salary =
(
SELECT MAX(s.Salary) FROM #Employee s WHERE s.Salary NOT IN (SELECT MAX(f.Salary) FROM #Employee f)
)

-- OR

SELECT * FROM #Employee WHERE Salary =
(
	SELECT MAX(s.Salary) FROM #Employee s WHERE s.Salary < (SELECT MAX(f.Salary) FROM #Employee f)
)

-- OR, SPECIAL
SELECT S.Id, S.Salary FROM #Employee S 
WHERE 2 = (
			SELECT COUNT(DISTINCT F.Salary) 
			FROM #Employee F 
			--WHERE 2000 <= F.Salary
			WHERE S.Salary <= F.Salary
		  )


-- OR, SPECIAL
SELECT * FROM #Employee WHERE Salary =
(
	SELECT MIN(S.Salary) FROM 
	(
		SELECT TOP 3 F.Salary FROM #Employee F 
		ORDER BY F.Salary DESC
	) S
)


-- OR
SELECT TOP 1 WITH TIES S.Id, S.Salary FROM 
(
	SELECT TOP 2 WITH TIES F.Id, F.Salary FROM #Employee F 
	ORDER BY F.Salary DESC
) S
ORDER BY S.Salary ASC


-- Question 2: find Max Salary from each department
SELECT MAX(Salary) AS [MaxSalary], DepId FROM #Employee
GROUP BY DepId

-- or
SELECT MAX(Salary) AS [MaxSalary], D.DepNam
FROM #Employee E RIGHT JOIN #Department D
ON E.DepId = D.Id
GROUP BY D.DepNam


DROP TABLE #Employee
DROP TABLE #Department

-- http://www.java67.com/2013/04/10-frequently-asked-sql-query-interview-questions-answers-database.html