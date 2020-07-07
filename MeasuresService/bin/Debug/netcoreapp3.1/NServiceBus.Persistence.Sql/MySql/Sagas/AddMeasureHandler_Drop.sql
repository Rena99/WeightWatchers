
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'AddMeasureHandler`');
set @tableNameNonQuoted = concat(@tablePrefix, 'AddMeasureHandler');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;
