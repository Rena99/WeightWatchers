
/* TableNameVariable */

declare @tableName nvarchar(max) = '[' + @schema + '].[' + @tablePrefix + N'AddMeasureHandler]';
declare @tableNameWithoutSchema nvarchar(max) = @tablePrefix + N'AddMeasureHandler';


/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty Id */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_Id' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_Id nvarchar(max);
  set @createColumn_Id = '
  alter table ' + @tableName + N'
    add Correlation_Id nvarchar(200);';
  exec(@createColumn_Id);
end

/* VerifyColumnType String */

declare @dataType_Id nvarchar(max);
set @dataType_Id = (
  select data_type
  from INFORMATION_SCHEMA.COLUMNS
  where
    table_name = @tableNameWithoutSchema and
    table_schema = @schema and
    column_name = 'Correlation_Id'
);
if (@dataType_Id <> 'nvarchar')
  begin
    declare @error_Id nvarchar(max) = N'Incorrect data type for Correlation_Id. Expected nvarchar got ' + @dataType_Id + '.';
    throw 50000, @error_Id, 0
  end

/* WriteCreateIndex Id */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_Id' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_Id nvarchar(max);
  set @createIndex_Id = N'
  create unique index Index_Correlation_Id
  on ' + @tableName + N'(Correlation_Id)
  where Correlation_Id is not null;';
  exec(@createIndex_Id);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = object_id(@tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_Id'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from INFORMATION_SCHEMA.COLUMNS
    where
        table_name = @tableNameWithoutSchema and
        table_schema = @schema and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_Id'
);
exec sp_executesql @dropPropertiesQuery

/* CompleteSagaScript */
