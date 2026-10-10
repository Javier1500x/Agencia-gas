INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-09', N'Compra inicial de prueba');


INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (1, 1101, 1000.00, 0.00); 

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (1, 2101, 0.00, 1000.00); 

-- 3. Consulta para verificar asientos descuadrados
SELECT IdAsiento, 
       SUM(Debe) AS TotalDebe, 
       SUM(Haber) AS TotalHaber 
FROM dbo.Movimientos 
GROUP BY IdAsiento 
HAVING SUM(Debe) <> SUM(Haber);