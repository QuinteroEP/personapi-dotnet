CREATE DATABASE arq_per_db;
GO

USE arq_per_db
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'arq_per_db')      
     EXEC (N'CREATE SCHEMA arq_per_db')                                   
 GO                                                               

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'estudios'  AND sc.name = N'arq_per_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'estudios'  AND sc.name = N'arq_per_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [arq_per_db].[estudios]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[arq_per_db].[estudios]
(
   [id_prof] int  NOT NULL,
   [cc_per] int  NOT NULL,
   [fecha] date  NULL,
   [univer] varchar(50)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.estudios',
        N'SCHEMA', N'arq_per_db',
        N'TABLE', N'estudios'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'persona'  AND sc.name = N'arq_per_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'persona'  AND sc.name = N'arq_per_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [arq_per_db].[persona]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[arq_per_db].[persona]
(
   [cc] int  NOT NULL,
   [nombre] varchar(45)  NOT NULL,
   [apellido] varchar(45)  NOT NULL,
   [genero] varchar(1)  NOT NULL,
   [edad] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.persona',
        N'SCHEMA', N'arq_per_db',
        N'TABLE', N'persona'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'profesion'  AND sc.name = N'arq_per_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'profesion'  AND sc.name = N'arq_per_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [arq_per_db].[profesion]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[arq_per_db].[profesion]
(
   [id] int  NOT NULL,
   [nom] varchar(90)  NOT NULL,
   [des] varchar(max)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.profesion',
        N'SCHEMA', N'arq_per_db',
        N'TABLE', N'profesion'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'telefono'  AND sc.name = N'arq_per_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'telefono'  AND sc.name = N'arq_per_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [arq_per_db].[telefono]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[arq_per_db].[telefono]
(
   [num] varchar(15)  NOT NULL,
   [oper] varchar(45)  NOT NULL,
   [duenio] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.telefono',
        N'SCHEMA', N'arq_per_db',
        N'TABLE', N'telefono'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_estudios_id_prof'  AND sc.name = N'arq_per_db'  AND type in (N'PK'))
ALTER TABLE [arq_per_db].[estudios] DROP CONSTRAINT [PK_estudios_id_prof]
 GO



ALTER TABLE [arq_per_db].[estudios]
 ADD CONSTRAINT [PK_estudios_id_prof]
   PRIMARY KEY
   CLUSTERED ([id_prof] ASC, [cc_per] ASC)

GO


USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_persona_cc'  AND sc.name = N'arq_per_db'  AND type in (N'PK'))
ALTER TABLE [arq_per_db].[persona] DROP CONSTRAINT [PK_persona_cc]
 GO



ALTER TABLE [arq_per_db].[persona]
 ADD CONSTRAINT [PK_persona_cc]
   PRIMARY KEY
   CLUSTERED ([cc] ASC)

GO


USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_profesion_id'  AND sc.name = N'arq_per_db'  AND type in (N'PK'))
ALTER TABLE [arq_per_db].[profesion] DROP CONSTRAINT [PK_profesion_id]
 GO



ALTER TABLE [arq_per_db].[profesion]
 ADD CONSTRAINT [PK_profesion_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_telefono_num'  AND sc.name = N'arq_per_db'  AND type in (N'PK'))
ALTER TABLE [arq_per_db].[telefono] DROP CONSTRAINT [PK_telefono_num]
 GO



ALTER TABLE [arq_per_db].[telefono]
 ADD CONSTRAINT [PK_telefono_num]
   PRIMARY KEY
   CLUSTERED ([num] ASC)

GO


USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id 
    WHERE so.name = N'enum2str$persona$genero'  AND sc.name=N'arq_per_db'  AND type in (N'FN',N'TF',N'IF',N'FS',N'FT'))
BEGIN

  DROP FUNCTION [arq_per_db].[enum2str$persona$genero]
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION arq_per_db.enum2str$persona$genero 
( 
   @setval tinyint
)
RETURNS nvarchar(max)
AS 
   BEGIN
      RETURN 
         CASE @setval
            WHEN 1 THEN 'M'
            WHEN 2 THEN 'F'
            ELSE ''
         END
   END
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.persona',
        N'SCHEMA', N'arq_per_db',
        N'FUNCTION', N'enum2str$persona$genero'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id 
    WHERE so.name = N'norm_enum$persona$genero'  AND sc.name=N'arq_per_db'  AND type in (N'FN',N'TF',N'IF',N'FS',N'FT'))
BEGIN

  DROP FUNCTION [arq_per_db].[norm_enum$persona$genero]
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION arq_per_db.norm_enum$persona$genero 
( 
   @setval nvarchar(max)
)
RETURNS nvarchar(max)
AS 
   BEGIN
      RETURN arq_per_db.enum2str$persona$genero(arq_per_db.str2enum$persona$genero(@setval))
   END
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.persona',
        N'SCHEMA', N'arq_per_db',
        N'FUNCTION', N'norm_enum$persona$genero'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id 
    WHERE so.name = N'str2enum$persona$genero'  AND sc.name=N'arq_per_db'  AND type in (N'FN',N'TF',N'IF',N'FS',N'FT'))
BEGIN

  DROP FUNCTION [arq_per_db].[str2enum$persona$genero]
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION arq_per_db.str2enum$persona$genero 
( 
   @setval nvarchar(max)
)
RETURNS tinyint
AS 
   BEGIN
      RETURN 
         CASE @setval
            WHEN 'M' THEN 1
            WHEN 'F' THEN 2
            ELSE 0
         END
   END
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'arq_per_db.persona',
        N'SCHEMA', N'arq_per_db',
        N'FUNCTION', N'str2enum$persona$genero'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE arq_per_db
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'estudios'  AND sc.name = N'arq_per_db'  AND si.name = N'estudio_persona_fk' AND so.type in (N'U'))
   DROP INDEX [estudio_persona_fk] ON [arq_per_db].[estudios] 
GO
CREATE NONCLUSTERED INDEX [estudio_persona_fk] ON [arq_per_db].[estudios]
(
   [cc_per] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE arq_per_db
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'telefono'  AND sc.name = N'arq_per_db'  AND si.name = N'telefono_persona_fk' AND so.type in (N'U'))
   DROP INDEX [telefono_persona_fk] ON [arq_per_db].[telefono] 
GO
CREATE NONCLUSTERED INDEX [telefono_persona_fk] ON [arq_per_db].[telefono]
(
   [duenio] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'estudios$estudio_persona_fk'  AND sc.name = N'arq_per_db'  AND type in (N'F'))
ALTER TABLE [arq_per_db].[estudios] DROP CONSTRAINT [estudios$estudio_persona_fk]
 GO



ALTER TABLE [arq_per_db].[estudios]
 ADD CONSTRAINT [estudios$estudio_persona_fk]
 FOREIGN KEY 
   ([cc_per])
 REFERENCES 
   [arq_per_db].[arq_per_db].[persona]     ([cc])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'estudios$estudio_profesion_fk'  AND sc.name = N'arq_per_db'  AND type in (N'F'))
ALTER TABLE [arq_per_db].[estudios] DROP CONSTRAINT [estudios$estudio_profesion_fk]
 GO



ALTER TABLE [arq_per_db].[estudios]
 ADD CONSTRAINT [estudios$estudio_profesion_fk]
 FOREIGN KEY 
   ([id_prof])
 REFERENCES 
   [arq_per_db].[arq_per_db].[profesion]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE arq_per_db
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'telefono$telefono_persona_fk'  AND sc.name = N'arq_per_db'  AND type in (N'F'))
ALTER TABLE [arq_per_db].[telefono] DROP CONSTRAINT [telefono$telefono_persona_fk]
 GO



ALTER TABLE [arq_per_db].[telefono]
 ADD CONSTRAINT [telefono$telefono_persona_fk]
 FOREIGN KEY 
   ([duenio])
 REFERENCES 
   [arq_per_db].[arq_per_db].[persona]     ([cc])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE arq_per_db
GO
ALTER TABLE  [arq_per_db].[estudios]
 ADD DEFAULT NULL FOR [fecha]
GO

ALTER TABLE  [arq_per_db].[estudios]
 ADD DEFAULT NULL FOR [univer]
GO


USE arq_per_db
GO
ALTER TABLE  [arq_per_db].[persona]
 ADD DEFAULT NULL FOR [edad]
GO

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

INSERT INTO [arq_per_db].[persona] ([cc], [nombre], [apellido], [genero], [edad])
VALUES (1001, 'Juan', 'Diego', 'M', 21);

INSERT INTO [arq_per_db].[estudios] ([id_prof], [cc_per], [fecha], [univer])
VALUES (10, 1001, '2021-01-15', 'Universidad Javeriana');

INSERT INTO [arq_per_db].[telefono] ([num], [oper], [duenio])
VALUES ('3007654321', 'Tigo', 1001)