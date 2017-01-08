CREATE TABLE #TopSalary
(
	Id INT,
	Salary int
)



INSERT INTO #TopSalary VALUES(1, 1000)
INSERT INTO #TopSalary VALUES(2, 2000)
INSERT INTO #TopSalary VALUES(3, 3000)

SELECT * FROM #TopSalary WHERE Salary =
(
SELECT MAX(s.Salary) FROM #TopSalary s WHERE s.Salary NOT IN (SELECT MAX(f.Salary) FROM #TopSalary f)
)

-- OR

SELECT * FROM #TopSalary WHERE Salary =
(
SELECT MAX(s.Salary) FROM #TopSalary s WHERE s.Salary < (SELECT MAX(f.Salary) FROM #TopSalary f)
)

-- OR
SELECT S.Id, S.Salary FROM #TopSalary S 
WHERE 2 = (
			SELECT COUNT(DISTINCT F.Salary) 
			FROM #TopSalary F 
			--WHERE 2000 <= F.Salary
			WHERE S.Salary <= F.Salary
		  )

-- OR
SELECT TOP 1 WITH TIES S.Id, S.Salary FROM 
(
	SELECT TOP 2 WITH TIES F.Id, F.Salary FROM #TopSalary F 
	ORDER BY F.Salary DESC
) S
ORDER BY S.Salary ASC

-- OR
SELECT * FROM #TopSalary WHERE Salary =
(
	SELECT MIN(S.Salary) FROM 
	(
		SELECT TOP 3 F.Salary FROM #TopSalary F 
		ORDER BY F.Salary DESC
	) S
)



DROP TABLE #TopSalary