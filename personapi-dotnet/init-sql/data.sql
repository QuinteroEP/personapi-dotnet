USE arq_per_db;
GO

INSERT INTO [arq_per_db].[persona] ([cc], [nombre], [apellido], [genero], [edad])
VALUES (1005, 'Pablo', 'Quintero', 'M', 22);

INSERT INTO [arq_per_db].[profesion] ([id], [nom], [des])
VALUES (10, 'Ingeniero', 'Profesional en ciberseguridad');

INSERT INTO [arq_per_db].[estudios] ([id_prof], [cc_per], [fecha], [univer])
VALUES (10, 1005, '2021-01-15', 'Universidad Javeriana');

INSERT INTO [arq_per_db].[telefono] ([num], [oper], [duenio])
VALUES ('3001234567', 'Claro', 1005)