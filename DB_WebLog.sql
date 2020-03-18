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

create procedure Editar
@var_Nome varchar(255), @var_Idade int, @var_CPF varchar(255), @var_Email varchar(255), @var_Senha varchar(255), @var_Id int
as
	update tbUsuario set Nome = @var_Nome, Idade = @var_Idade, CPF = @var_CPF
	where tbUsuario.idUser = @var_Id;

	update tbLogin set Email = @var_Email, Senha = @var_Senha
	where tbLogin.Id_User = @var_Id;
go

Execute Editar 'Lucas'