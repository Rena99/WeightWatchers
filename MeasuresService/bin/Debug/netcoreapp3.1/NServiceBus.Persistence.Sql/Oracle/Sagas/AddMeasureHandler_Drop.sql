
/* TableNameVariable */

/* DropTable */

declare
  n number(10);
begin
  select count(*) into n from user_tables where table_name = 'ADDMEASUREHANDLER';
  if(n > 0)
  then
    execute immediate 'drop table "ADDMEASUREHANDLER"';
  end if;
end;
