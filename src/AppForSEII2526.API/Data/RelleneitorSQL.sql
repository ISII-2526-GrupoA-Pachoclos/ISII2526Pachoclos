-- 1. PRIMERO: AspNetUsers (usuarios base)
INSERT INTO [dbo].[AspNetUsers] ([Id], [nombre], [apellido], [correoElectronico], [numTelefono], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
VALUES 
(N'1', N'pibi', N'ronaldo', N'pibi@gmail.com', N'9876543210', N'pibi', N'PIBI', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1, 0, NULL, 0, 2),
(N'2', N'Juan', N'Valdes', N'juanvaldes@gmail.com', N'684574747', N'juanv', N'JUANV', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1, 1, NULL, 1, 1),
(N'3', N'Maria', N'Lopez', N'maria@gmail.com', N'5551234567', N'marial', N'MARIAL', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1, 0, NULL, 0, 0);

-- 2. SEGUNDO: fabricante (necesario para herramientas)
SET IDENTITY_INSERT [dbo].[fabricante] ON;
INSERT INTO [dbo].[fabricante] ([id], [nombre]) 
VALUES 
(1, N'Paco')
(2, N'Jose')
(3, N'Ana')
(4, N'Duviso')
(5, N'Ferreteria Florencio')
SET IDENTITY_INSERT [dbo].[fabricante] OFF;

-- 3. TERCERO: Herramienta (depende de fabricante)
SET IDENTITY_INSERT [dbo].[Herramienta] ON;
INSERT INTO [dbo].[Herramienta] ([id], [material], [nombre], [precio], [tiempoReparacion], [fabricanteid]) 
VALUES 
(1, N'Hierro', N'Llave Inglesa', 12, N'10 dias', 1)
(2, N'Acero', N'Destornillador', 7, N'7 dias', 3)
(3, N'Madera', N'Martillo', 6, N'5 dias', 2)
(4, N'varios', N'Motosierra', 150, N'20 dias', 2)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;

-- 4. CUARTO: Compra (depende de ApplicationUser)
/*
SET IDENTITY_INSERT [dbo].[Compra] ON;
INSERT INTO [dbo].[Compra] ([Id], [direccionEnvio], [fechaCompra], [precioTotal], [metodopago], [ApplicationUserId]) 
VALUES 
(1, N'Calle Principal 123', '2025-01-15 10:00:00', 30, 1, N'1')
(2, N'Avenida Central 456', '2025-02-20 14:30:00', 14, 2, N'2')
(3, N'Plaza Mayor 789', '2025-03-10 09:15:00', 45, 0, N'3')
SET IDENTITY_INSERT [dbo].[Compra] OFF;
*/

-- 5. QUINTO: ComprarItem (depende de Compra y Herramienta)
/*
INSERT INTO [dbo].[ComprarItem] ([compraId], [herramientaid], [cantidad], [descripcion], [precio]) 
VALUES 
(1, 1, 3, N'Llave Inglesa (3 uds) - Compra online', 10),
(2, 2, 2, N'Destornillador (2 uds) - Compra en tienda', 7),
(3, 3, 3, N'Martillo (3 uds) - Oferta especial', 15);
*/

-- 6. SEXTO: Oferta (depende de ApplicationUser)
SET IDENTITY_INSERT [dbo].[Oferta] ON;
INSERT INTO [dbo].[Oferta] ([Id], [fechaInicio], [fechaFin], [fechaOferta], [ApplicationUserId], [metodoPago], [paraSocio]) 
VALUES 
(1, N'2025-11-17 00:00:00', N'2025-12-25 00:00:00', N'2025-11-30 00:00:00', N'1', 2, 1)
(2, N'2025-11-17 00:00:00', N'2025-12-25 00:00:00', N'2025-11-04 18:39:29', N'1', 0, 1)
SET IDENTITY_INSERT [dbo].[Oferta] OFF;

-- 7. SÉPTIMO: OfertaItem (depende de Oferta y Herramienta)
INSERT INTO [dbo].[OfertaItem] ([ofertaId], [herramientaid], [porcentaje], [precioFinal]) 
VALUES 
(1, 2, 50, 2),
(2, 2, 50, 1);

-- 8. OCTAVO: Reparacion (depende de ApplicationUser)
SET IDENTITY_INSERT [dbo].[Reparacion] ON;
INSERT INTO [dbo].[Reparacion] ([id], [fechaEntrega], [fechaRecogida], [precioTotal], [metodoPago], [ApplicationUserId]) 
VALUES 
(1, N'2025-10-20 10:30:00', N'2025-10-25 15:45:00', 15, 1, N'1')
(2, '2025-11-05 09:00:00', '2025-11-12 09:00:00', 14, 2, N'2')
(3, '2025-11-10 14:00:00', '2025-11-20 14:00:00', 45, 0, N'3')
SET IDENTITY_INSERT [dbo].[Reparacion] OFF;

-- 9. NOVENO: ReparacionItem (depende de Reparacion y Herramienta)
INSERT INTO [dbo].[ReparacionItem] ([Herramientaid], [Reparacionid], [cantidad], [descripcion], [precio]) 
VALUES 
(1, 1, 3, N'Mango roto y oxidada', 10),
(2, 2, 2, N'Punta desgastada', 7),
(3, 3, 3, N'Cabeza floja y mango agrietado', 15);

-- 10. DÉCIMO: alquilar (depende de ApplicationUser)
/*
SET IDENTITY_INSERT [dbo].[alquilar] ON;
INSERT INTO [dbo].[alquilar] ([id], [direccionEnvio], [fechaAlquiler], [fechaInicio], [fechaFin], [precioTotal], [metodoPago], [applicationUserId]) 
VALUES 
(1, N'Calle Alquiler 111', '2025-01-10 08:00:00', '2025-01-11 08:00:00', '2025-01-18 08:00:00', 70, 1, N'1')
(2, N'Avenida Rental 222', '2025-02-15 10:00:00', '2025-02-16 10:00:00', '2025-02-23 10:00:00', 49, 2, N'2')
(3, N'Plaza Alquiler 333', '2025-03-20 14:00:00', '2025-03-21 14:00:00', '2025-03-28 14:00:00', 105, 0, N'3')
SET IDENTITY_INSERT [dbo].[alquilar] OFF;
*/

-- 11. UNDÉCIMO: alquilarItem (depende de alquilar y Herramienta)
/*
INSERT INTO [dbo].[alquilarItem] ([alquilarid], [herramientaid], [cantidad], [precio]) 
VALUES 
(1, 4, 1, 150),
(2, 2, 1, 7),
(3, 1, 1, 12);
*/