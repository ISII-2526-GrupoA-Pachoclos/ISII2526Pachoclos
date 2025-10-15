SET IDENTITY_INSERT [dbo].[fabricante] ON
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (1, N'Paco')
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (2, N'Jose')
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (3, N'Pepe')
SET IDENTITY_INSERT [dbo].[fabricante] OFF


SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) VALUES (4, N'Hierro', N'Llave Inglesa', 10, N'10 dias', 1)
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) VALUES (5, N'Acero', N'Destornillador', 7, N'7 dias', 1)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF