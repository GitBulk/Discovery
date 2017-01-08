CREATE TABLE #TopSalary
(
	Id INT,
	Salary int
)



INSERT INTO #TopSalary VALUES(1, 1000)
INSERT INTO #TopSalary VALUES(2, 2000)
INSERT INTO #TopSalary VALUES(3, 3000)


SELECT MAX(s.Salary) FROM #TopSalary s WHERE s.Salary NOT IN (SELECT MAX(f.Salary) FROM #TopSalary f)

-- OR
SELECT MAX(s.Salary) FROM #TopSalary s WHERE s.Salary < (SELECT MAX(f.Salary) FROM #TopSalary f)


DROP TABLE #TopSalary