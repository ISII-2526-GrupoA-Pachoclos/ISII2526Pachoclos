SET IDENTITY_INSERT [dbo].[fabricante] ON
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (1, N'Paco')
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (2, N'Jose')
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (3, N'Pepe')
SET IDENTITY_INSERT [dbo].[fabricante] OFF


SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion]) VALUES (2, N'Hierro', N'Llave Inglesa', 20, N'10')
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion]) VALUES (3, N'Madera', N'Martillo', 15, N'10')
SET IDENTITY_INSERT [dbo].[Herramienta] OFF
