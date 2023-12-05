----- Criação do Banco -----

CREATE DATABASE InterComm
GO

USE InterComm
GO

----- Criação das Tabelas -----

-------------------------------------------------------
CREATE TABLE Users(
	IdUser			int		primary key		identity,
	Nome			varchar(32) not null,
	Login			varchar(32) not null,
	Password		varchar(32) not null
)

CREATE TABLE Locais (
	IdLocal				int				primary key		identity,
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
	Ativo				int default 0
)

CREATE TABLE Motoristas (
	IdMotorista		int				primary key		identity,
	NomeMotorista	varchar(60) 	not null,
	CPF				varchar(11),
	CNH				varchar(11) 	not null,
	Ativo			int default 0
)

CREATE TABLE Locais_Motoristas (
	CodMotorista	int     foreign key	   references Motoristas(IdMotorista),
	CodLocal	    int 	foreign key	   references Locais(IdLocal),
	primary key	    (CodMotorista, CodLocal)
)

CREATE TABLE Conjuntos (
	IdConjunto		int			primary key		identity, 
	CodMotorista	int			foreign key		references Motoristas(IdMotorista),
	tipoConjunto    int     	not null,      
	PlacaA			varchar(7)	not null,
	PlacaB			varchar(7)  null,
	PlacaC			varchar(7)  null
)

CREATE TABLE Contratos (
	IdContrato		int		primary key	    	identity,
	CodLocal		int		foreign key         references Locais(IdLocal) not null,
	CodCommodity	int		not null,
	DataInicio		date 	not null,
	Volume			float	not null,
	VolumeAtual		float	default 0,
	VolumePendente	float	default 0,
	ValorUnitario	float 	not null,
	Status			int		default 0
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
	CodOrdem	int		foreign key     references OrdensDeCarregamento(IdOrdem) not null
)


----- Tabelas Complementares -----

-------------------------------------------------------
CREATE TABLE Responsaveis (
	IdResponsavel	int			primary key	identity,
	CodLocal		int			foreign key	references Locais(IdLocal),
	Responsavel		varchar(60)	not null
)

CREATE TABLE Telefones (
	IdTelefone	    int			primary key	identity,
	CodLocal		int			foreign key	references	Locais(IdLocal),
	Telefone		varchar(11)	not null
)

CREATE TABLE Emails (
	IdEmail	int		primary 	key	identity,
	CodLocal		int			foreign key	references	Locais(IdLocal),
	Email			varchar(60)	not null
) 

----- Criação das Views -----

-- CREATE VIEW vw_Motorista_Conjunto
-- as
-- 	select *
-- 	from Conjuntos C 
-- 	inner join Motoristas M ON M.IdMotorista = C.CodMotorista

-- select * from vw_Motorista_Conjunto

-- -->

-- CREATE VIEW vw_Contrato_Local
-- as
-- 	select *
-- 	from Contratos C 
-- 	inner join Locais L ON L.IdLocal = C.CodLocal

-- select * from vw_Contrato_Local

-- -->

-- CREATE VIEW vw_Local_LocalMotorista
-- as
-- 	select *
-- 	from Locais L
-- 	inner join Locais_Motoristas LM ON 
-- 	LM.CodLocal = L.IdLocal
-- 	AND 
-- 	LM.CodMotorista = @IdMotorista

-- select * from vw_Local_LocalMotorista

-- -->

-- Alter VIEW vw_Motorista_LocalMotorista
-- as
-- 	select *
-- 	from Motoristas M
-- 	inner join Locais_Motoristas LM ON 
-- 	LM.CodMotorista = M.IdMotorista
-- 	AND 
-- 	LM.CodLocal = 4

-- select * from vw_Motorista_LocalMotorista


-- Inserções nas tabelas -- 

-- Tabela Locais --

INSERT INTO Users (Nome, Login, Password)
VALUES ('Administrador', 'admin','adm123')

INSERT INTO Locais (LocalNomeFantasia, LocalRazaoSocial, CNPJ, TipoLocal, Logradouro, Bairro, Cidade, Estado, Numero)
values ('Intercommpany', 'Intercommpany Inc.', '12345678000100', 1, 'Rua Anhanguera', 'Eldorado', 'São José do Rio Preto', '1', 0000), 	--BASE
	   ('Combuscom', 'Commbuscom Inc.', '71436643654320', 0, 'Rua Etanolísio', 'Dionísio', 'Ouro Preto', '2', 3939),					--USINA
	   ('Transportais', 'Transporte Inc.', '56654580284375', 3, 'Rua Trás pra Frente', 'Subida Baixa', 'Morro Abaixo', '3', 3251), 		--TRANSPOR
	   ('Carga Pesada', 'Pesadão Inc.', '75586098429364', 3, 'Rua Frente pra Trás', 'Descida Alta', 'Vale Acima', '4', 1523),			--TRANSPOR
	   ('Posto Gasosa', 'Gasosa Inc.', '02649164837105', 2, 'Rua Acelerada', 'Randandan', 'Petrópolis', '5', 1284),						--POSTO
	   ('Posto Italia', 'Combustalia Inc.', '02658263745917', 2, 'Rua Francesco', 'Virgolini', 'Antônio Prado', '6', 1293)				--POSTO
go


-- Tabela Motoristas --

INSERT INTO Motoristas (NomeMotorista, CPF, CNH)
values ('Cleiton Alejandro de Souza', '11122233390', '12345678903'),
       ('Paulo Fulano de Almeida', '38495465475', '98765432107'),
	   ('Ciclano Ciclone Ferreira', '74582474564', '10293847561'),
	   ('Beltrano Belonio Belon', '53973442986', '67584930184')
go


-- Tabela Locais_Motoristas --

INSERT INTO Locais_Motoristas (CodMotorista, CodLocal)
values (1, 3),
       (2, 3),
	   (3, 4),
	   (4, 4)
go


-- Tabela Conjuntos --

INSERT INTO Conjuntos (CodMotorista, tipoConjunto, PlacaA) values (1, 0, 'ABC1056')
INSERT INTO Conjuntos (CodMotorista, tipoConjunto, PlacaA, PlacaB) values (2, 1, 'POI0284', 'XYZ2348')
INSERT INTO Conjuntos (CodMotorista, tipoConjunto, PlacaA) values (3, 0, 'LOL0214')
INSERT INTO Conjuntos values (4, 1, 'HUE0707', 'GUY1221', 'BEL8005')
go


-- Tabela Contratos --

INSERT INTO Contratos values (1, 1, '2023-08-01', 50, 50, 0, 5.50, 1)
INSERT INTO Contratos values (2, 2, '2023-03-14', 100, 85, 15, 4.90, 1)
INSERT INTO Contratos values (1, 1, '2023-10-30', 150, 130, 20, 3.85, 1)
go


-- Tabela OrdensDeCarregamento


-- Tabela NotaFiscal --


-- Tabela Responsaveis --

INSERT INTO Responsaveis (CodLocal, Responsavel)
values (1, 'Victor Tin'),
       (2, 'Flare Santos'),
	   (3, 'Estranio Doutorino')
go


-- Tabela Telefones --

INSERT INTO Telefones (CodLocal, Telefone)
values (4, '87991746592'),
       (5, '24928368167'),
	   (6, '54928374212')


-- Tabela Emails --

INSERT INTO Emails (CodLocal, Email)
values (1, 'intercommpany@gmail.com'),
       (3, 'teletransporte@hotmail.com'),
	   (5, 'comumouaditivada@gmail.com')
go

-- Criação das Procedures --

CREATE PROCEDURE Sp_Insert_Emails
	@Email	VARCHAR(60),
	@CodLocal		INT,
	@IdEmail INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Emails VALUES (@CodLocal, @Email )

	SELECT @IdEmail  = SCOPE_IDENTITY()

	SELECT @IdEmail  as IdEmail 

	RETURN
END
GO

CREATE PROCEDURE Sp_Insert_Responsaveis
	@Responsavel	VARCHAR(60),
	@CodLocal		INT,
	@IdResponsavel	INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Responsaveis VALUES (@CodLocal, @Responsavel)

	SELECT @IdResponsavel = SCOPE_IDENTITY()

	SELECT @IdResponsavel as IdResponsavel

	RETURN
END
GO

CREATE PROCEDURE Sp_Insert_Telefones
	@Telefones	VARCHAR(60),
	@CodLocal		INT,
	@IdTelefone	INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Telefones VALUES (@CodLocal, @Telefones)

	SELECT @IdTelefone = SCOPE_IDENTITY()

	SELECT @IdTelefone as IdTelefone

	RETURN
END
GO
