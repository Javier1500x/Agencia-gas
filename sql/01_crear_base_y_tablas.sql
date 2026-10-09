/* =====================================================
   Sistema contable - Agencia de gas
   Script 01: crea la base, las tablas y el catálogo de cuentas
   Se puede ejecutar varias veces sin que dé error ni duplique datos.
   Ejecutar en SSMS: abrir el archivo y presionar Ejecutar (F5).
   ===================================================== */

-- 1. BASE DE DATOS ------------------------------------
IF DB_ID('AgenciaGas') IS NULL
    CREATE DATABASE AgenciaGas;
GO

USE AgenciaGas;
GO

-- 2. TABLA CUENTAS ------------------------------------
IF OBJECT_ID('dbo.Cuentas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Cuentas
    (
        Codigo         INT           NOT NULL PRIMARY KEY,
        Nombre         NVARCHAR(100) NOT NULL,
        Tipo           VARCHAR(20)   NOT NULL,
        Naturaleza     VARCHAR(10)   NOT NULL,
        ActividadFlujo VARCHAR(20)   NOT NULL,

      -- CONSTRAINT CK_Cuentas_Tipo
        --    CHECK (Tipo IN ('Activo', 'Pasivo', 'Capital', 'Ingreso', 'Costo', 'Gasto')),
       --CONSTRAINT CK_Cuentas_Naturaleza
         --   CHECK (Naturaleza IN ('Deudora', 'Acreedora')),
       --CONSTRAINT CK_Cuentas_ActividadFlujo
         --   CHECK (ActividadFlujo IN ('Operacion', 'Inversion', 'Financiamiento', 'Ninguna'))
    );
END
GO

-- 3. TABLA ASIENTOS -----------------------------------
IF OBJECT_ID('dbo.Asientos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Asientos
    (
        Id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Fecha       DATE              NOT NULL,
        Descripcion NVARCHAR(200)     NOT NULL
    );
END
GO

-- 4. TABLA MOVIMIENTOS --------------------------------
IF OBJECT_ID('dbo.Movimientos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Movimientos
    (
        Id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        IdAsiento    INT               NOT NULL,
        CodigoCuenta INT               NOT NULL,
        Debe         DECIMAL(18,2)     NOT NULL CONSTRAINT DF_Movimientos_Debe  DEFAULT 0,
        Haber        DECIMAL(18,2)     NOT NULL CONSTRAINT DF_Movimientos_Haber DEFAULT 0,

        CONSTRAINT FK_Movimientos_Asientos
            FOREIGN KEY (IdAsiento) REFERENCES dbo.Asientos(Id),
        CONSTRAINT FK_Movimientos_Cuentas
            FOREIGN KEY (CodigoCuenta) REFERENCES dbo.Cuentas(Codigo),

        -- Montos no negativos, y un movimiento va al Debe o al Haber, no a los dos
        CONSTRAINT CK_Movimientos_Montos
            CHECK (Debe >= 0 AND Haber >= 0 AND (Debe = 0 OR Haber = 0))
    );
END
GO

-- 5. CATÁLOGO DE CUENTAS ------------------------------
-- Solo se carga si la tabla está vacía.
IF NOT EXISTS (SELECT 1 FROM dbo.Cuentas)
BEGIN
    INSERT INTO dbo.Cuentas (Codigo, Nombre, Tipo, Naturaleza, ActividadFlujo) VALUES
    -- Activo
    (1101, N'Caja',                      'Activo',  'Deudora',   'Ninguna'),
    (1102, N'Bancos',                    'Activo',  'Deudora',   'Ninguna'),
    (1103, N'Clientes',                  'Activo',  'Deudora',   'Operacion'),
    (1104, N'Inventario de gas',         'Activo',  'Deudora',   'Operacion'),
    (1201, N'Camiones de reparto',       'Activo',  'Deudora',   'Inversion'),
    (1202, N'Cilindros y tanques',       'Activo',  'Deudora',   'Inversion'),
    (1203, N'Depreciación acumulada',    'Activo',  'Acreedora', 'Operacion'),
    -- Pasivo
    (2101, N'Proveedores',               'Pasivo',  'Acreedora', 'Operacion'),
    (2102, N'Sueldos por pagar',         'Pasivo',  'Acreedora', 'Operacion'),
    (2201, N'Préstamos bancarios',       'Pasivo',  'Acreedora', 'Financiamiento'),
    -- Capital
    (3101, N'Capital social',            'Capital', 'Acreedora', 'Financiamiento'),
    (3102, N'Utilidades acumuladas',     'Capital', 'Acreedora', 'Ninguna'),
    (3103, N'Retiros del dueño',         'Capital', 'Deudora',   'Financiamiento'),
    -- Ingresos
    (4101, N'Ventas de gas',             'Ingreso', 'Acreedora', 'Operacion'),
    -- Costos
    (5101, N'Costo de ventas',           'Costo',   'Deudora',   'Operacion'),
    -- Gastos
    (6101, N'Sueldos',                   'Gasto',   'Deudora',   'Operacion'),
    (6102, N'Combustible de reparto',    'Gasto',   'Deudora',   'Operacion'),
    (6103, N'Mantenimiento de unidades', 'Gasto',   'Deudora',   'Operacion'),
    (6104, N'Renta',                     'Gasto',   'Deudora',   'Operacion'),
    (6105, N'Depreciación',              'Gasto',   'Deudora',   'Operacion');
END
GO

-- 6. VERIFICACIÓN -------------------------------------
SELECT COUNT(*) AS TotalCuentas FROM dbo.Cuentas;   -- debe salir 20
SELECT * FROM dbo.Cuentas ORDER BY Codigo;
GO
