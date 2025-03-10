CREATE TABLE [dbo].[Pausa](
	[Id] [uniqueidentifier] NOT NULL,
	[Inicio] [datetime2](7) NOT NULL,
	[Termino] [datetime2](7) NOT NULL,
	[Observacao] [nvarchar](max) NULL,
	[TarefaId] [uniqueidentifier] NOT NULL,
	[Ativo] [bit] NOT NULL,
	[DataDeCriacao] [datetime2](7) NOT NULL,
	[DataDeModificacao] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Pausa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pausa]  WITH CHECK ADD  CONSTRAINT [FK_Pausa_Tarefas_TarefaId] FOREIGN KEY([TarefaId])
REFERENCES [dbo].[Tarefas] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Pausa] CHECK CONSTRAINT [FK_Pausa_Tarefas_TarefaId]
GO