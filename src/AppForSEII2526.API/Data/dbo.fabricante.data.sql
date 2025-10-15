SET IDENTITY_INSERT [dbo].[fabricante] ON
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (1, N'Duviso')
INSERT INTO [dbo].[fabricante] ([id], [nombre]) VALUES (2, N'Ferreteria Florencio')
SET IDENTITY_INSERT [dbo].[fabricante] OFF


SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) VALUES (4, N'hierro', N'destornillador', 12, N'12', 1)
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) VALUES (5, N'hierro', N'martillo', 6, N'3', 1)
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) VALUES (6, N'varios', N'motosierra', 150, N'20', 2)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF