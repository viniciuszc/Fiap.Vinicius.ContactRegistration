USE CADASTRO

--drop table USUARIO
--drop table CONTATO
--drop table DDD


GO

CREATE TABLE USUARIO
(
	CODIGO INT IDENTITY NOT NULL,
	NOME VARCHAR(100) NOT NULL,
	EMAIL VARCHAR(50) NOT NULL UNIQUE,
	SENHA VARCHAR(100) NOT NULL,
	CONSTRAINT PK_USUARIO PRIMARY KEY(CODIGO)
);

CREATE TABLE DDD
(
	[ID] INT PRIMARY KEY IDENTITY (1, 1),
	[CODIGO] VARCHAR(3) NOT NULL, 
    [REGIAO] VARCHAR(100) NOT NULL, 
    [UF] VARCHAR(2) NULL	
);

CREATE TABLE [dbo].[CONTATO] (
    [ID] INT PRIMARY KEY IDENTITY (1, 1),
	[CODIGO_USUARIO] INT NOT NULL,
    [NOME]     VARCHAR (100) NOT NULL,
    [TELEFONE] VARCHAR (20)  NOT NULL,
    [EMAIL]    VARCHAR (100) NOT NULL,
    [DDD_ID] INT NOT NULL,         
	CONSTRAINT [FK_CONTATO_TO_DDD] FOREIGN KEY ([DDD_ID]) REFERENCES [DDD]([ID])
);

INSERT INTO DDD(CODIGO, REGIAO, UF)
VALUES
    ('011', 'Sudeste', 'SP'),
    ('012', 'Sudeste', 'SP'),
    ('013', 'Sudeste', 'SP'),
    ('014', 'Sudeste', 'SP'),
    ('015', 'Sudeste', 'SP'),
    ('016', 'Sudeste', 'SP'),
    ('017', 'Sudeste', 'SP'),
    ('018', 'Sudeste', 'SP'),
    ('019', 'Sudeste', 'SP'),
    ('021', 'Sudeste', 'RJ'),
    ('022', 'Sudeste', 'RJ'),
    ('024', 'Sudeste', 'RJ'),
    ('027', 'Sudeste', 'ES'),
    ('028', 'Sudeste', 'ES'),
    ('031', 'Sudeste', 'MG'),
    ('032', 'Sudeste', 'MG'),
    ('033', 'Sudeste', 'MG'),
    ('034', 'Sudeste', 'MG'),
    ('035', 'Sudeste', 'MG'),
    ('037', 'Sudeste', 'MG'),
    ('038', 'Sudeste', 'MG'),
    ('041', 'Sul', 'PR'),
    ('042', 'Sul', 'PR'),
    ('043', 'Sul', 'PR'),
    ('044', 'Sul', 'PR'),
    ('045', 'Sul', 'PR'),
    ('047', 'Sul', 'SC'),
    ('048', 'Sul', 'SC'),
    ('049', 'Sul', 'SC'),
    ('051', 'Sul', 'RS'),
    ('053', 'Sul', 'RS'),
    ('054', 'Sul', 'RS'),
    ('055', 'Sul', 'RS'),
    ('061', 'Centro-Oeste', 'DF'),
    ('062', 'Centro-Oeste', 'GO'),
    ('063', 'Norte', 'TO'),
    ('064', 'Centro-Oeste', 'GO'),
    ('067', 'Centro-Oeste', 'MS'),
    ('068', 'Norte', 'AC'),
    ('069', 'Norte', 'RO'),
    ('071', 'Nordeste', 'BA'),
    ('073', 'Nordeste', 'BA'),
    ('074', 'Nordeste', 'BA'),
    ('075', 'Nordeste', 'CE'),
    ('077', 'Centro-Oeste', 'MT'),
    ('078', 'Centro-Oeste', 'MT'),
    ('079', 'Nordeste', 'SE'),
    ('081', 'Nordeste', 'PE'),
    ('082', 'Nordeste', 'AL'),
    ('083', 'Nordeste', 'PB'),
    ('084', 'Nordeste', 'RN'),
    ('085', 'Nordeste', 'PB'),
    ('086', 'Nordeste', 'PI'),
    ('087', 'Nordeste', 'PE'),
    ('088', 'Nordeste', 'CE'),
    ('089', 'Nordeste', 'MA'),
    ('091', 'Norte', 'AM'),
    ('092', 'Norte', 'AM'),
    ('093', 'Norte', 'PA'),
    ('094', 'Norte', 'AP'),
    ('095', 'Norte', 'RR'),
    ('096', 'Norte', 'AP'),
    ('097', 'Norte', 'AM'),
    ('098', 'Norte', 'MA'),
    ('099', 'Norte', 'MA') 

	select * from USUARIO
	select * from DDD
	select * from CONTATO



--Docker--
00- entrar na raiz da solução
01- no terminal executar os comandos: 
    1.2 - docker compose build
    1.3 - docker compose up

--Grafana--
localhost:3000
user: admin
senha: admin

01- Menu -> Connections -> Data souces
    1.1 - Adicionar um data source tipo Prometheus.
    1.2 - Inserir da Connection a url a seguinte url:
          http://host.docker.internal:9090
    1.3 - Save              
02 - importar dashborad usando o json "dashboard_metrics_grafana"
     2.1 - Menu Dashboards -> New -> Import
     2.2 - Copiar e colocar o json que está no arquivo "dashboard_metrics_grafana"
     2.3 - Load
     2.4 Acessar o Dashboard "ASP.NET Core - Metrics (Prometheus)".

  
