CREATE TABLE [dbo].[Tarefas](
	[Id] [uniqueidentifier] NOT NULL,
	[Inicio] [datetime2](7) NOT NULL,
	[Termino] [datetime2](7) NOT NULL,
	[NumeroDaTarefa] [nvarchar](max) NULL,
	[Titulo] [nvarchar](max) NULL,
	[Cliente] [nvarchar](max) NULL,
	[Descricao] [nvarchar](max) NULL,
	[StatusDaTarefa] [nvarchar](max) NULL,
	[HorasDePausaRaw] [bigint] NOT NULL,
	[HorasUtilizadasRaw] [bigint] NOT NULL,
	[HorasDePausa] [bigint] NOT NULL,
	[Ativo] [bit] NOT NULL,
	[DataDeCriacao] [datetime2](7) NOT NULL,
	[DataDeModificacao] [datetime2](7) NOT NULL,
	[Solucao] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tarefas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
