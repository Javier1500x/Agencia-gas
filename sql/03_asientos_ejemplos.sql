
DECLARE @Id INT;


INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-01', N'Aportación inicial del dueño');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 1102, 100000.00, 0.00),
       (@Id, 3101, 0.00, 100000.00);


INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-02', N'Préstamo bancario');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 1102, 50000.00, 0.00),
       (@Id, 2201, 0.00, 50000.00);


INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-03', N'Compra de un camión');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 1201, 60000.00, 0.00),
       (@Id, 1102, 0.00, 60000.00);


INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-04', N'Compra de gas a crédito');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 1104, 20000.00, 0.00),
       (@Id, 2101, 0.00, 20000.00);


INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-05', N'Venta de gas de contado');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 1101, 2500.00, 0.00),
       (@Id, 4101, 0.00, 2500.00);


-- 6. Asiento 6: Costo de la venta
INSERT INTO dbo.Asientos (Fecha, Descripcion) 
VALUES ('2026-10-05', N'Costo de la venta');
SET @Id = SCOPE_IDENTITY();

INSERT INTO dbo.Movimientos (IdAsiento, CodigoCuenta, Debe, Haber) 
VALUES (@Id, 5101, 1500.00, 0.00),
       (@Id, 1104, 0.00, 1500.00);
GO


-- =====================================================
-- 7. VERIFICACIÓN DE DATOS E INTEGRIDAD
-- =====================================================

-- Ver cantidad total de asientos (debe coincidir con los insertados)
SELECT COUNT(*) AS TotalAsientos FROM dbo.Asientos;

-- Verificar si existe algún asiento descuadrado (no debe devolver ninguna fila)
SELECT IdAsiento, 
       SUM(Debe) AS TotalDebe, 
       SUM(Haber) AS TotalHaber 
FROM dbo.Movimientos 
GROUP BY IdAsiento 
HAVING SUM(Debe) <> SUM(Haber);
GO
