
/*
 u mssql-u comanda za ekzekuciju koda je `go`
 */
create database posao;


use posao;


create table employees(id INT IDENTITY(1,1),ime text,prezime text,adresa text,bruto_plata int , pozicija text);
 



