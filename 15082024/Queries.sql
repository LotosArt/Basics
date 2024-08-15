-- 1) Напишите запрос, который извлечет только год из даты.
SELECT YEAR(hire_date) AS hire_year
FROM employees;

-- 2) Напишите запрос, который найдет все записи, где в текстовом поле содержится слово "SQL".
SELECT *
FROM employees
WHERE employee_name LIKE '%SQL%';

-- 3) Напишите запрос, который заменит все вхождения слова "кошка" на слово "собака" в столбце my_text_column.
SELECT REPLACE(some_column, 'кошка', 'собака') AS updated_text
FROM employees;

-- 4) Напишите запрос, который извлечет часть строки перед первым вхождением символа " - ".
SELECT SUBSTRING(text_column, 1, CHARINDEX(' - ', text_column) - 1) AS before_dash
FROM employees;
-----------------------
SELECT LEFT(text_column, CHARINDEX(' - ', text_column) - 1) AS before_dash
FROM employees
WHERE CHARINDEX(' - ', text_column) > 0;

-- 5) Напишите запрос, который извлечет часть строки после последнего вхождения символа " / ".
SELECT RIGHT(text_column, LEN(text_column) - CHARINDEX(' / ', REVERSE(text_column)))
FROM employees;

-- 6) Напишите запрос, который извлечет все цифры из строки.
WITH Digits AS
         (
             SELECT
                 SUBSTRING(my_text_column, 1, 1) AS CurrentChar,
                 SUBSTRING(my_text_column, 2, LEN(my_text_column)) AS RemainingText
             FROM employees
             WHERE LEN(my_text_column) > 0
         )
SELECT STRING_AGG(CurrentChar, '') AS DigitsOnly
FROM Digits
WHERE CurrentChar LIKE '[0-9]'
OPTION (MAXRECURSION 0);

-- 7) Напишите запрос, который извлечет день недели из даты.
SELECT DATENAME(WEEKDAY, hire_date) AS day_of_week
FROM employees;

-- 8) Напишите запрос, который найдет все записи, где значение числового столбца меньше 10.
SELECT *
FROM employees
WHERE salary < 10;

-- 9) Напишите запрос, который вычислит среднее значение числового столбца.
SELECT AVG(salary) AS average_salary
FROM employees;

-- 10) Напишите запрос, который вычисляет сумму значений числового столбца для всех записей, где значение даты больше 01.01.2021.
SELECT SUM(salary) AS total_salary
FROM employees
WHERE hire_date > '2021-01-01';

-- 11) Напишите запрос, который извлечет только месяц и год из даты.
SELECT FORMAT(hire_date, 'MM-yyyy') AS month_year
FROM employees;

-- 12) Напишите запрос, который удалит все пробелы в начале и конце текстового столбца. Sql
SELECT TRIM(text_column) AS trimmed_text
FROM employees;

-- 13) Напишите запрос, который вычислит разницу между двумя датами в днях.
SELECT DATEDIFF(DAY, hire_date, GETDATE()) AS days_difference
FROM employees;

-- 14) Напишите запрос, который вычислит сумму значений числового столбца для каждого значения текстового столбца.
SELECT text_column, SUM(salary) AS total_salary
FROM employees
GROUP BY text_column;

-- 15) Напишите запрос, который заменит все буквы в текстовом столбце на заглавные.
SELECT UPPER(text_column) AS upper_text
FROM employees;


