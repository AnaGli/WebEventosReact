CREATE DATABASE appeventosteste
use appeventosteste

create table StatusEvento (
   IdEventoStatus		int						identity,
   NomeStatus			varchar(250)			not null,
   constraint PK_StatusEvento primary key (IdEventoStatus)
)
go

create table CategoriaEvento (
   IdCategoriaEvento		int						identity,
   NomeCategoria			varchar(250)			not null,
   constraint PK_CategoriaEvento primary key (IdCategoriaEvento)
)
go


create table Evento (
   IdEvento             int						identity,
   IdEventoStatus		int						not null default 1,
   IdCategoriaEvento	int						not null,		
   Nome					varchar(250)			not null,
   DataHoraInicio       datetime				not null,
   DataHoraFim          datetime				not null,
   Local				varchar(250)			not null,
   Descricao            varchar(1000)			not null,
   LimiteVagas			int						not null,
   constraint PK_Evento primary key (IdEvento)
)
go

alter table "Evento"
add constraint FK_Evento_CategoriaEvento foreign key (IdCategoriaEvento)
references CategoriaEvento (IdCategoriaEvento);

alter table "Evento"
add constraint FK_Evento_EventoStatus foreign key (IdEventoStatus)
references StatusEvento (IdEventoStatus);
go

create table Participacao (
   IdParticipacao		int						identity,
   IdEvento				int						not null,
   LoginParticipante    varchar(250)			not null,
   FlagPresente			bit						not null default 0,
   Nota					int						null,
   Comentario          	varchar(1000)			null,
   constraint PK_Participacao primary key (IdParticipacao)
)
go

alter table Participacao
add constraint FK_Participacao_Evento foreign key (IdEvento)
references Evento (IdEvento);
go


SET IDENTITY_INSERT StatusEvento ON
INSERT INTO StatusEvento ([IdEventoStatus],[NomeStatus]) VALUES (1,'Aberto para inscrições');
INSERT INTO StatusEvento ([IdEventoStatus],[NomeStatus]) VALUES (2,'Em andamento');
INSERT INTO StatusEvento ([IdEventoStatus],[NomeStatus]) VALUES (3,'Concluído');
INSERT INTO StatusEvento ([IdEventoStatus],[NomeStatus]) VALUES (4,'Cancelado');
SET IDENTITY_INSERT StatusEvento OFF

SET IDENTITY_INSERT CategoriaEvento ON
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (1,'Backend');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (2,'Frontend');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (3,'Mobile');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (4,'Cloud & DevOps');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (5,'Modern Workplaces');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (6,'UI/UX');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (7,'Data & Analytics');
INSERT INTO CategoriaEvento ([IdCategoriaEvento],[NomeCategoria]) VALUES (8,'Agilidade & Qualidade ');
SET IDENTITY_INSERT CategoriaEvento OFF

select* from [dbo].[CategoriaEvento]

select* from [dbo].[Evento]

select * from [dbo].[Participacao]

select * from [dbo].[StatusEvento]

INSERT INTO [dbo].[Participacao] ([IdEvento],[LoginParticipante] ) VALUES ( 2, 'ana paula' );

INSERT INTO [dbo].[Participacao] ([IdEvento],[LoginParticipante],[FlagPresente] ) VALUES ( 3, 'ana paula', 1 );

