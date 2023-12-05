----- Criação do Banco -----

CREATE DATABASE InterComm
GO

USE InterComm
GO

----- Criaçãoo das Tabelas -----

-------------------------------------------------------
CREATE TABLE Users(
	IdUser			int		primary key		identity,
	Nome			varchar(32) not null,
	Login			varchar(32) not null,
	Password		varchar(32) not null
)


CREATE TABLE Locais (
	IdLocal				int		primary key		identity,
	LocalNomeFantasia	varchar(60),
	LocalRazaoSocial	varchar(60)		not null,
	CNPJ				varchar(14)		not null, -- conferir quantos caracteres depois
	TipoLocal			int				not null,
	IE					varchar(30),
	CEP					varchar(8),
	Logradouro			varchar(30)		not null,
	Bairro				varchar(30),
	Cidade				varchar(30)		not null,
	Estado				varchar(30)		not null,
	Numero				int,
	Complemento			varchar(30),
	Desativado			int default 0
)

CREATE TABLE Motoristas (
	IdMotorista		int		primary key		identity,
	NomeMotorista	varchar(60) not null,
	CPF				varchar(11),
	CNH				varchar(11) not null,
	Desativado		int default 0
)

CREATE TABLE Locais_Motoristas (
	CodMotorista	int 	foreign key		references Motoristas(IdMotorista),
	CodLocal		int		foreign key		references Locais(IdLocal)
)

CREATE TABLE Conjuntos (
	IdConjunto		int		primary key		identity,
	CodMotorista	int		foreign key 	references Motoristas(IdMotorista),
	TipoConjunto	int 	not null,
	PlacaA			varchar(7)	not null,
	PlacaB			varchar(7),
	PlacaC			varchar(7),
)

CREATE TABLE Contratos (
	IdContrato		int		primary key		identity,
	CodLocal		int		foreign key references Locais(IdLocal) not null,
	CodCommodity	int		not null,
	DataInicio		date 	not null,
	Volume			float	not null,
	VolumeAtual		float	default 0,
	VolumePendente	float	default 0,
	ValorUnitario	float 	not null,
	Status			int		default 0,
)

CREATE TABLE OrdensDeCarregamento (
	IdOrdem				int			primary key		identity,
	CodContrato			int			foreign key references Contratos(IdContrato) not null,
	CodDestino			int 		foreign key references Locais(IdLocal)	not null,
	CodTransportadora	int			foreign key references Locais(IdLocal) not null,
	CodMotorista		int			foreign key references Motoristas(IdMotorista) not null,
	TipoConjunto		int 		not null,
	PlacaA				varchar(7)	not null,
	PlacaB				varchar(7),
	PlacaC				varchar(7),
	Volume				float 		not null,
	Status				int			default 0,
)

CREATE TABLE NotaFiscal (
	IdNota		int		primary key		identity,
	CodOrdem	int		foreign key references OrdensDeCarregamento(IdOrdem) not null
)


----- Tabelas Complementares -----

-------------------------------------------------------
CREATE TABLE Responsaveis (
	IdResponsavel	int		primary key		identity,
	CodLocal		int		foreign key		references	Locais(IdLocal),
	Responsavel		varchar(60)	not null
)

CREATE TABLE Telefones (
	IdTelefones		int		primary key		identity,
	CodLocal		int		foreign key		references	Locais(IdLocal),
	Telefone		varchar(11)	not null
)

CREATE TABLE Emails (
	IdEmails	int		primary key		identity,
	CodLocal		int		foreign key		references	Locais(IdLocal),
	Email			varchar(60)	not null
)


----- PROCEDURE -----
