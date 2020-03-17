use DB_Teste;

create table tbUsuario
(
	idUser int identity primary key,
	Nome varchar(255),
	Idade int,
	CPF varchar(255)
);

create table tbLogin
(
	idLogin int identity primary key,
	Email varchar(255),
	Senha varchar(255),
	Id_User int,
	constraint fk_Login_User foreign key (Id_User) references tbUsuario(idUser) ON UPDATE CASCADE ON DELETE CASCADE/*FOREIGN KEY*/	
);


insert into tbUsuario values('Lucas Andrade Silva',17, '519-269-498-11');
insert into tbLogin values('Lucas@gmail.com', '123', (select max(idUser) from tbUsuario));
select * from tbUsuario inner join tbLogin on tbLogin.Id_User = tbUsuario.idUser;
select * from tbLogin ;

drop table tbUsuario;