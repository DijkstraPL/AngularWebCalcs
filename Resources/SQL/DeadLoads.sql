USE [Build_IT_Db]
GO
SET IDENTITY_INSERT [DeadLoads].[Categories] ON 

INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (1, N'Betony')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (2, N'Zaprawy, gładzie')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (3, N'Drewno')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (4, N'Metale')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (5, N'Szkło, tworzywa sztuczne')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (6, N'Ściany')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (7, N'Materiały izolacyjne i inne niesypkie')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (8, N'Pokrycia dachowe')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (9, N'Podłogi, parkiety')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (10, N'Grunty')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (11, N'Materiały mostowe')
INSERT [DeadLoads].[Categories] ([Id], [Name]) VALUES (12, N'Własne')
SET IDENTITY_INSERT [DeadLoads].[Categories] OFF
GO
SET IDENTITY_INSERT [DeadLoads].[Subcategories] ON 

INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (1, N'Lekkie', N'PN-EN', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (2, N'Normalne', N'PN-EN', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (3, N'Jamisty', N'PN-B', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (4, N'Lekki', N'PN-B', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (5, N'Gipsobeton', N'PN-B', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (6, N'Trocinobeton', N'PN-B', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (7, N'Inne', N'PN-B', 1)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (8, N'Zaprawy', N'PN-EN', 2)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (9, N'Zaprawy', N'PN-B', 2)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (10, N'Normalne', N'PN-EN', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (11, N'Drewno klejone', N'PN-EN', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (12, N'Sklejka', N'PN-EN', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (13, N'Płyty prasowane', N'PN-EN', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (14, N'Płyty pilśniowe', N'PN-EN', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (15, N'Drewno', N'PN-B', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (16, N'Metale', N'PN-EN', 4)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (17, N'Metale', N'PN-B', 4)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (18, N'Szkło', N'PN-EN', 5)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (19, N'Tworzywa sztuczne', N'PN-EN', 5)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (20, N'Ceramiczne z gliny', N'PN-EN', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (21, N'Wapienno - silikatowe', N'PN-EN', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (22, N'Beton autoklawizowany napowietrzony', N'PN-EN', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (23, N'Terrakota', N'PN-EN', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (24, N'Kamień naturalny', N'PN-EN', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (25, N'Kamień naturalny', N'PN-B', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (26, N'Cegła budowlana wypalana z gliny', N'PN-B', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (27, N'Glina', N'PN-B', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (28, N'Materiały izolacyjne', N'PN-B', 7)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (29, N'Blacha fałdowa stalowa', N'PN-B', 8)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (30, N'Posadzki podłogowe wraz z klejami i zaprawami wiążącymi (z podkładem)', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (31, N'Deszczułki podłogowe', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (32, N'Nawierzchnie mostów drogowych', N'PN-EN', 11)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (33, N'Balasty mostowe', N'PN-EN', 11)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (34, N'Nawierzchnie mostów kolejowych', N'PN-EN', 11)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (35, N'Konstrukcje z balastem', N'PN-EN', 11)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (36, N'Konstrukcje bez balastu', N'PN-EN', 11)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (37, N'Płyty', N'PN-B', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (38, N'Inne', N'PN-B', 3)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (39, N'Ognioodporna', N'PN-B', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (40, N'Pozostałe', N'PN-B', 6)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (41, N'Jastrych', N'PN-B', 7)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (42, N'Szkło', N'PN-B', 7)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (43, N'Wyroby z waty szklanej', N'PN-B', 7)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (44, N'Wyroby z wełny mineralnej', N'PN-B', 7)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (45, N'Dachówki', N'PN-B', 8)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (46, N'Papa', N'PN-B', 8)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (47, N'Pozostałe', N'PN-B', 8)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (48, N'Płytki', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (49, N'Warstwy izolacji termicznej i akustycznej wraz z podkładem', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (50, N'Płyty', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (51, N'Styropian', N'PN-B', 9)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (52, N'Grunty niespoiste rodzime mineralne - Żwiry pospółki', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (53, N'Grunty niespoiste rodzime mineralne - Piaski grube i średnie', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (54, N'Grunty niespoiste rodzime mineralne - Piaski drobne i pylaste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (55, N'Grunty niespoiste rodzime organiczne - Piaski próchnicze', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (56, N'Mało spoiste - Żwiry, pospółki gliniaste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (57, N'Mało spoiste - Piaski gliniaste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (58, N'Mało spoiste - Pyły piaszczyste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (59, N'Mało spoiste - Pyły', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (60, N'Średnio spoiste - Gliny piaszczyste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (61, N'Średnio spoiste - Gliny', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (62, N'Średnio spoiste - Gliny pylaste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (63, N'Zwięzło spoiste - Gliny piaszczyste zwięzłe', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (64, N'Zwięzło spoiste - Gliny zwięzłe', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (65, N'Zwięzło spoiste - Gliny pylaste zwięzłe', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (66, N'Bardzo spoiste - Iły piaszczyste', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (67, N'Bardzo spoiste - Iły', N'PN-B', 10)
INSERT [DeadLoads].[Subcategories] ([Id], [Name], [DocumentName], [CategoryId]) VALUES (68, N'Bardzo spoiste - Iły pylaste', N'PN-B', 10)
SET IDENTITY_INSERT [DeadLoads].[Subcategories] OFF
GO
SET IDENTITY_INSERT [DeadLoads].[Materials] ON 

INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1273, N'Klasa gęstości LC 1,0', 9, 10, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1274, N'Klasa gęstości LC 1,2', 10, 12, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1275, N'Klasa gęstości LC 1,4', 12, 14, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1276, N'Klasa gęstości LC 1,6', 14, 16, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1277, N'Klasa gęstości LC 1,8', 16, 18, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1278, N'Klasa gęstości LC 2,0', 18, 20, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 1)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1279, N'Żelbet', 24, 24, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 2)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1281, N'Ciężki', 24, 50, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 2)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1283, N'Jamisty na kruszywie keramzytowym', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1284, N'Jamisty na kruszywie pumeksowym', 14, 14, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1285, N'Jamisty na kruszywie żwirowym', 19, 19, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1286, N'Jamisty na tłuczniu piaskowcowym', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1287, N'Jamisty na tłuczniu ceglanym', 16, 16, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1288, N'Jamisty na tłuczniu z wapienia porowatego', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1289, N'Jamisty na tłuczniu z wapienia zbitego', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 3)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1290, N'Lekki - komórkowy izolacyjny', 6, 6, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 4)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1291, N'Lekki - komórkowy konstrukcyjny', 9, 9.5, 3, N'PN-82-B-02001 Tab.Z1-6', N'Maksimum dla betonu zbrojonego.', 4)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1292, N'Lekki - pianobeton', 8, 8, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 4)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1293, N'Gipsobeton piaskowy', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 5)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1294, N'Gipsobeton trocinowy lany', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 5)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1295, N'Gipsobeton trocinowy żużlowy', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 5)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1296, N'Trocinobeton izolacyjny (1:1:8)', 8, 8, 3, N'PN-82-B-02001 Tab.Z1-6', N'Stosunek objętościowy składników - cement; piasek; trociny', 6)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1297, N'Trocinobeton wypełniający (1:2:7)', 10, 10, 3, N'PN-82-B-02001 Tab.Z1-6', N'Stosunek objętościowy składników - cement; piasek; trociny', 6)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1298, N'Trocinobeton konstrukcyjny (1:3:6)', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-6', N'Stosunek objętościowy składników - cement; piasek; trociny', 6)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1299, N'Azbesto-cement', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1300, N'Metalowy ziarnisty układany na gorąco', 22, 22, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1301, N'Metalowy ziarnisty układany na zimno', 20, 20, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1302, N'Keranzytowy', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1303, N'na kruszywie ceglanym', 18, 20, 3, N'PN-82-B-02001 Tab.Z1-6', N'Maksimum dla betonu zagęszczonego.', 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1304, N'na kruszywie glinoporytowym', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1305, N'na kruszywie z grysu bazaltowego', 27, 28, 3, N'PN-82-B-02001 Tab.Z1-6', N'Maksimum dla betonu zagęszczonego.', 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1306, N'na kruszywie z pumeksu hutniczego o składzie (cement, kruszywo) 200kg+1196kg', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1307, N'na kruszywie z pumeksu hutniczego o składzie (cement, kruszywo) 450kg+1270kg', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1308, N'na kruszywie żużlowym paleniskowym bez piasku', 14, 18, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1309, N'na kruszywie żużlowym paleniskowym z piaskiem', 16, 18, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1310, N'Specjalny na kruszywie ciężkim (np. barytowym, magnezytowym)', 25, 27, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1311, N'Sprężony na kruszywie granitowym', 25, 25, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1312, N'Sprężony na kruszywie bazaltowym', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1313, N'Zwykły na kruszywie kamiennym', 23, 25, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1314, N'Siatkobeton', 23, 23, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1315, N'Strużkobeton', 6.5, 6.5, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1316, N'Szkło-żelbet (płyty szklanobetonowe)', 26, 26, 3, N'PN-82-B-02001 Tab.Z1-6', NULL, 7)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1318, N'Cementowa', 19, 23, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 8)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1320, N'Gipsowa', 12, 18, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 8)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1322, N'Wapienno-cementowa', 18, 20, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 8)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1324, N'Wapienna', 12, 18, 3, N'PN-EN 1991-1-1:2004 Tab.A.1', NULL, 8)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1325, N'Barytowa', 32, 32, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1327, N'Cementowa', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1329, N'Cementowo-gliniana', 20, 20, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1331, N'Cementowa i cementowo-wapienna na kruszywie żużlowym (ciepła)', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1333, N'Cementowa na siatce metalowej', 24, 24, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1335, N'Cementowo-wapienna', 19, 19, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1337, N'Cementowo-wapienna na siatce metalowej', 22, 22, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1341, N'Gipsowa z piaskiem', 16, 16, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1343, N'Gliniana', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1345, N'Szpachlówki do tynków', 14, 14, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1347, N'Szpachlówki gipsowe typu "nidalit"', 12, 12, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1349, N'Wapienne i gipsowo-wapienne', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1351, N'Wapienna na trzcinie', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-5', NULL, 9)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1354, N'Klasa C14', 3.5, 3.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1356, N'Klasa C16', 3.7, 3.7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1358, N'Klasa C18', 3.8, 3.8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1360, N'Klasa C22', 4.1, 4.1, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1362, N'Klasa C24', 4.2, 4.2, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1364, N'Klasa C27', 4.5, 4.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1366, N'Klasa C30', 4.6, 4.6, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1368, N'Klasa C35', 4.8, 4.8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1370, N'Klasa C40', 5, 5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1372, N'Klasa D30', 6.4, 6.4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1374, N'Klasa D35', 6.7, 6.7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1376, N'Klasa D40', 7, 7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1378, N'Klasa D50', 7.8, 7.8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1380, N'Klasa D60', 8.4, 8.4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1382, N'Klasa D70', 10.8, 10.8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 10)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1383, N'Klejone jednorodne GL24h', 3.7, 3.7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1384, N'Klejone jednorodne GL28h', 4, 4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1385, N'Klejone jednorodne GL32h', 4.2, 4.2, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1386, N'Klejone jednorodne GL36h', 4.4, 4.4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1387, N'Klejone kombinowane GL24c', 3.5, 3.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1388, N'Klejone kombinowane GL28c', 3.7, 3.7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1389, N'Klejone kombinowane GL32c', 4, 4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1390, N'Klejone kombinowane GL36c', 4.2, 4.2, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 11)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1391, N'z drewna iglastego', 5, 5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 12)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1392, N'z brzozy', 7, 7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 12)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1393, N'płyta warstwowa, płyta stolarska', 4.5, 4.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 12)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1394, N'wiórowe płasko prasowane', 7, 8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 13)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1395, N'cementowo-wiórowe', 12, 12, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 13)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1396, N'o ukierunkowanych włóknach - OSB, warstwowe, płatkowe', 7, 7, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 13)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1397, N'twarde, standardowe i hartowane', 10, 10, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 14)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1398, N'półtwarde (o średniej gęstości)', 8, 8, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 14)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1399, N'porowate', 4, 4, 3, N'PN-EN 1991-1-1:2004 Tab.A.3', NULL, 14)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1400, N'Akacja', 7.7, 8.3, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1401, N'Brzoza, dąb, klon', 7, 7.6, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1402, N'Buk', 7.3, 7.9, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1403, N'Grab', 8.3, 9, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1404, N'Jesion', 7.5, 8.1, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1405, N'Jodła, lipa, olcha, osika, sosna, świerk, topola', 5.5, 6, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1406, N'Modrzew', 6.9, 7.5, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1407, N'Wiąz', 6.8, 7.3, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności 23%', 15)
GO
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1408, N'Twarde gatunki drzew egzotycznych', 10, 10, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1409, N'Korek ekspandowany', 1.5, 1.5, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 15)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1410, N'Skałodrzew mineralny', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 38)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1411, N'Skałodrzew trocinowy (ksylolit)', 13, 13, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 38)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1412, N'Sklejka', 7, 7, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 38)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1413, N'Trociny i wióry luźno usypane', 1.5, 2.5, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności większej niż 23%', 38)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1414, N'Trociny i wióry zleżałe', 2.5, 3.5, 3, N'PN-82-B-02001 Tab.Z1-1', N'Minimalny w stanie powietrznosuchym; maksymalny o wilgotności większej niż 23%', 38)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1415, N'Płyta korkowa na lepiszczu asfaltowym', 2, 2, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1416, N'Płyta korkowa na lepiszczu okrzemkowym', 3.5, 3.5, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1417, N'Płyta pilśniowa izolacyjna', 3, 3, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1418, N'Płyta pilśniowa półtwarda', 5.5, 5.5, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1419, N'Płyta pilśniowa twarda', 8, 8, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1420, N'Płyta pilśniowa bardzo twarda', 10, 10, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1421, N'Płyty wiórowe płasko prasowane', 6.5, 6.5, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1422, N'Płyty wiórowe poprzecznie prasowane', 4, 4, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1423, N'Płyty wiórowo-cementowe', 4.5, 4.5, 3, N'PN-82-B-02001 Tab.Z1-1', NULL, 37)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1424, N'Aluminium', 27, 27, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1426, N'Mosiądz', 83, 85, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1428, N'Brąz', 83, 85, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1430, N'Miedź', 87, 89, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1432, N'Żelazo lane', 71, 72.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1434, N'Żelazo kute', 76, 76, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1436, N'Ołów', 112, 114, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1438, N'Stal', 77, 78.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1440, N'Cynk', 71, 72, 3, N'PN-EN 1991-1-1:2004 Tab.A.4', NULL, 16)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1443, N'Aluminium', 27, 27, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1445, N'Stopy aluminium', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1447, N'Brąz, mosiądz', 86, 86, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1449, N'Cyna, cynk kuty', 72, 72, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1451, N'Cynk w odlewach', 69, 69, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1453, N'Miedź', 89, 89, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1455, N'Ołów', 114, 114, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1457, N'Stal i staliwo', 78.5, 78.5, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1459, N'Żeliwo', 72.5, 72.5, 3, N'PN-82-B-02001 Tab.Z1-2', NULL, 17)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1460, N'łamane', 22, 22, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 18)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1461, N'w arkuszach', 25, 25, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 18)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1462, N'arkusze akrylowe', 12, 12, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 19)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1463, N'polistyren (ekspandowany, granulowany)', 0.3, 0.3, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 19)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1464, N'szkło piankowe', 1.4, 1.4, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 19)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1465, N'łupki', 28, 28, 3, N'PN-EN 1991-1-1:2004 Tab.A.5', NULL, 19)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1466, N'Typu LD', 1, 10, 3, NULL, NULL, 20)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1467, N'Typu HD', 10, 50, 3, NULL, NULL, 20)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1468, N'Klasy gęstości 0,5', 1, 5, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1469, N'Klasy gęstości 0,6', 5, 6, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1470, N'Klasy gęstości 0,7', 6, 7, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1471, N'Klasy gęstości 0,8', 7, 8, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1472, N'Klasy gęstości 0,9', 8, 9, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1473, N'Klasy gęstości 1,0', 9, 10, 3, NULL, NULL, 21)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1474, N'Beton autoklawizowany napowietrzony', 3, 10, 3, NULL, NULL, 22)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1475, N'Terrakota', 21, 21, 3, NULL, NULL, 23)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1476, N'z granitu, sjenitu, porfiru', 27, 30, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1478, N'z bazaltu, diorytu, gabra', 27, 31, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1480, N'z tachylitu', 26, 26, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1482, N'z lawy bazaltowej', 24, 24, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1484, N'z piaskowca', 21, 27, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1486, N'z wapienia zwięzłego', 20, 29, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1488, N'z innych wapieni', 20, 20, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1490, N'z tufów wulkanicznych', 20, 20, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1492, N'z gnejsu', 30, 30, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1494, N'z łupków', 28, 28, 3, N'PN-EN 1991-1-1:2004 Tab.A.2', NULL, 24)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1497, N'Alabaster, anhydryt', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1499, N'Andezyt, dioryt, dolomit, łupek', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1501, N'Bazalt', 33, 33, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1503, N'Diabaz, gabro, gnejs, malafir', 29, 29, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1505, N'Granit, sjenit', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1507, N'Kwarcyt, marmur, porfir, serpentyn', 27, 27, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1509, N'Piaskowiec mięki', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1511, N'Piaskowiec twardy', 25, 25, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1513, N'Trachit', 26, 26, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1515, N'Trawertyn', 24, 24, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1517, N'Tufy wulkaniczne', 12, 12, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1519, N'Wapienie o strkuturze porowatej', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1521, N'Wapienie o strukturze miękkiej', 22, 22, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1523, N'Wapienie o strukturze zbitej', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-3', N'Ciężar objętościowy muru z kamieni naturalnych można obliczać jako sumę
ciężarów objętościowych kamienia i zaprawy przyjmując, że ilość zaprawy wynosi objętościowo:
a) w murze z ciosów 10 %,
b) w murze z kamienia warstwowego 25 %,
c) w murze z kamienia łamanego 35 %.v', 25)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1524, N'Budowlana wypalana z gliny - dziurawka', 14, 14, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1525, N'Budowlana wypalana z gliny - klinkier, kominówka', 19, 19, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1526, N'Budowlana wypalana z gliny - kratówka', 13, 13, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1527, N'Budowlana wypalana z gliny - pełna', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1528, N'Budowlana wypalana z gliny - porowata', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1529, N'Budowlana wypalana z gliny - sitówka', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1530, N'Budowlana wypalana z gliny - szczelinówka', 12.5, 12.5, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 26)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1531, N'Ognioodporna - chromowomagnetyczna', 30, 30, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1532, N'Ognioodporna - dynasowa', 19, 19, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1533, N'Ognioodporna - magnetyzowa', 27, 27, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1534, N'Ognioodporna - magnetyzowa - chromitowa', 28, 28, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1535, N'Ognioodporna - szamotowa', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1536, N'Ognioodporna - termolitowa', 7.5, 7.5, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 39)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1537, N'Cementowa pełna', 22, 22, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 40)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1539, N'Wapienno-piaskowa (silikat) - drążona', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 40)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1541, N'Wapienno-piaskowa (silikat) - pełna', 19, 19, 3, N'PN-82-B-02001 Tab.Z1-4', N'Ciężar objętościowy ścian i ścianek działowych z cegły, w przypadku
stosowania zapraw ciężkich (o ciężarze objętościowym 15,0 kN/m3 i więcej), można przyjmować dla:
a) murów z cegły o ciężarze objętościowym 15,0 kN/m3 i więcej - równą ciężarowi objętościowemu cegły,
b) murów z cegły o ciężarze objętościowym od 12,0 do 14,5 kN/m3 - równą ciężarowi objętościowemu cegły
powiększonemu o 0,5 kN/m3,
c) murów z drobnych elementów z betonu komórkowego, w zależności od odmiany:
05 - 7,5 kN/m3,
06 - 9,0 kN/m3,
07 - 10,0 kN/m3,
09 - 12,0 kN/m3,
d) murów z pustaków gruzo- i żużlobetonowych o ciężarze objętościowym od 12,0 do 16,0 kN/m3 - równą ciężarowi
objętościowemu tych pustaków.', 40)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1543, N'Glina czysta', 20, 20, 3, N'PN-82-B-02001 Tab.Z1-4', NULL, 27)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1544, N'Glina z domieszką materiałów (paździerze, sieczka)', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-4', NULL, 27)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1545, N'Asfalt lany z wypełniaczami z kruszywa', 22.5, 22.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1546, N'Asfalt penaftowy czysty', 10, 10, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1547, N'Azbest', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1548, N'Estrychgips', 20, 20, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1549, N'Filc izolacyjny', 5, 5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1550, N'Gips lany, płyty gipsowe ścisłe', 12, 12, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1551, N'Glina z sieczką (lub trocinami) - 1:1', 13, 13, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1552, N'Glina z sieczką (lub trocinami) - 1:2', 8, 8, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
GO
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1553, N'Gruz ceglany z wapnem (polepa)', 12, 12, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1554, N'Guma', 14, 14, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1555, N'Lastrico (terazzo)', 22, 22, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1556, N'Linoleum', 12, 12, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1557, N'Lepik, papa', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1558, N'Pianizol', 0, 2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1559, N'Poliuretan', 0.5, 0.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1560, N'Płytki fajansowe glazurowane', 25, 25, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1561, N'Płytki podłogowe i materiały rulonowe podłogowe z tworzyw sztucznych (prócz winyleum)', 15, 15, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1562, N'Płyty azbestowo-cementowe - nieprasowane faliste', 17, 17, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1563, N'Płyty azbestowo-cementowe - prasowane płaskie', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1564, N'Płyty paździerzowe - izolacyjne', 5, 5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1565, N'Płyty paździerzowe - konstrukcyjne', 7, 7, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1566, N'Płyty torfowe - lekkie', 3.5, 3.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1567, N'Płyty torfowe - ścisłe', 6, 6, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1568, N'Płyty trzcinowe prasowane', 3, 3, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1569, N'Słoma prasowana (w płytach)', 2.5, 2.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1570, N'Smoła', 11, 11, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1571, N'Styropian', 0.5, 0.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1572, N'Tektura - prasowana', 10, 10, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1573, N'Tektura - zwykła', 7, 7, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1574, N'Ceramiczne płytki podłogowe', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1575, N'Torf - mielony (sproszkowany)', 2.5, 2.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1576, N'Torf - w belach', 3, 3, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1577, N'Trociny z wapnem 1:3', 6, 6, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1578, N'Winyleum', 18, 18, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1579, N'Wojłok', 5, 5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 28)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1580, N'Jastrych - cementowy', 21, 21, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 41)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1581, N'Jastrych - gipsowy', 16, 16, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 41)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1582, N'Jastrych - gliniany', 16.5, 16.5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 41)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1583, N'Jastrych - korkowy', 5, 5, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 41)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1584, N'Szkło - okienne (zwykłe)', 24, 24, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 42)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1585, N'Szkło - piankowe', 4, 4, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 42)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1586, N'Szkło - taflowe', 26, 26, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 42)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1587, N'Szkło - zbrojone', 27, 27, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 42)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1588, N'Wyroby z waty szklanej - maty', 0.9, 0.9, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 43)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1589, N'Wyroby z waty szklanej - welony rodzaju M', 1, 1, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 43)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1590, N'Wyroby z waty szklanej - welony rodzaju F', 1.3, 1.3, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 43)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1591, N'Wyroby z wełny mineralnej - wełna luzem', 1.2, 1.2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1592, N'Wyroby z wełny mineralnej - mata typu BL', 1.2, 1.2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1593, N'Wyroby z wełny mineralnej - mata typu L', 1, 1, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1594, N'Wyroby z wełny mineralnej - płyta twarda', 2, 2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1595, N'Wyroby z wełny mineralnej - płyta półtwarda', 1, 1, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1596, N'Wyroby z wełny mineralnej - płyta miękka i filc', 0.6, 0.6, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1597, N'Wyroby z wełny mineralnej - płyta w oplocie z siatki drucianej', 1.2, 1.2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1598, N'Wyroby z wełny mineralnej - płyta "Lamella"', 2, 2, 3, N'PN-82-B-02001 Tab.Z1-7', NULL, 44)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1599, N'Blacha fałdowa stalowa 43,5 (T-40) - grubość 0,88mm', 0.097, 0.097, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1600, N'Blacha fałdowa stalowa 43,5 (T-40) - grubość 1,00mm', 0.11, 0.11, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1601, N'Blacha fałdowa stalowa 55 (T-55) - grubość 0,75mm', 0.091, 0.091, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1602, N'Blacha fałdowa stalowa 55 (T-55) - grubość 0,88mm', 0.107, 0.107, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1603, N'Blacha fałdowa stalowa 55 (T-55) - grubość 1,00mm', 0.121, 0.121, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1604, N'Blacha fałdowa stalowa 55 (T-55) - grubość 1,25mm', 0.151, 0.151, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1605, N'Blacha fałdowa stalowa 80 (T-80) - grubość 0,75mm', 0.099, 0.099, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1606, N'Blacha fałdowa stalowa 80 (T-80) - grubość 0,88mm', 0.116, 0.116, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1607, N'Blacha fałdowa stalowa 80 (T-80) - grubość 1,00mm', 0.132, 0.132, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1608, N'Blacha fałdowa stalowa 80 (T-80) - grubość 1,25mm', 0.164, 0.164, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1609, N'Blacha fałdowa stalowa 100 (T-100) - grubość 0,75mm', 0.113, 0.113, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1610, N'Blacha fałdowa stalowa 100 (T-100) - grubość 0,88mm', 0.132, 0.132, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1611, N'Blacha fałdowa stalowa 100 (T-100) - grubość 1,00mm', 0.15, 0.15, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1612, N'Blacha fałdowa stalowa 100 (T-100) - grubość 1,25mm', 0.188, 0.188, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 29)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1613, N'Dachówka cementowa - karpiówka (podwójnie)', 0.75, 0.75, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 45)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1614, N'Dachówka cementowa - marsylska żłobkowana', 0.6, 0.6, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 45)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1615, N'Dachówka ceramiczna - holenderska i klasztorna karpiówka (pojedyncza)', 0.9, 0.9, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 45)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1616, N'Dachówka ceramiczna - karpiówka (podwójnie)', 0.95, 0.95, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 45)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1617, N'Dachówka ceramiczna - zakładkowa ciągniona', 0.9, 0.9, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 45)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1618, N'Papa na deskowaniu bez posypania żwirkiem - pojedynczo', 0.3, 0.3, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1619, N'Papa na deskowaniu bez posypania żwirkiem - podwójnie', 0.35, 0.35, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1620, N'Papa na deskowaniu posypana żwirkiem - pojedynczo', 0.35, 0.35, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1621, N'Papa na deskowaniu posypana żwirkiem - podwójnie', 0.4, 0.4, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1622, N'Papa na podłożu betonowym bez posypania żwirkiem - pojedynczo', 0.05, 0.05, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1623, N'Papa na podłożu betonowym bez posypania żwirkiem - podwójnie', 0.1, 0.1, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1624, N'Papa na podłożu betonowym posypana żwirkiem - pojedynczo', 0.1, 0.1, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1625, N'Papa na podłożu betonowym posypana żwirkiem - podwójnie', 0.15, 0.15, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 46)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1627, N'Blacha falista o grubości 0,55mm', 0.2, 0.2, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1629, N'Blacha stalowa, cynkowana lub miedziana o grubości 0,55mm', 0.35, 0.35, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1631, N'Gonty (podwójnie)', 0.4, 0.4, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1633, N'Łupek z podkładem z papy na deskowaniu pełnym', 0.6, 0.6, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1635, N'Łupek z podkładem z papy na łatach', 0.5, 0.5, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1637, N'Płyty azbestocementowe (eternit) - faliste', 0.3, 0.3, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1639, N'Płyty azbestocementowe (eternit) - płaskie', 0.35, 0.35, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1641, N'Szkło płaskie na płatwiach i szczeblinach stalowych - szkło grubości 5mm', 0.25, 0.25, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1643, N'Szkło płaskie na płatwiach i szczeblinach stalowych - szkło zbrojone grubości 6mm', 0.3, 0.3, 2, N'PN-82-B-02001 Tab.Z2-1', NULL, 47)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1644, N'Deski klejone warstwowe lakierowane (na lepiku) o grubości 19mm', 0.2, 0.2, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1645, N'Deski (przybijane do legarów) o grubości 30mm', 0.33, 0.33, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1646, N'Estrichgips bezspoinowy o grubości 30-40mm', 0.6, 0.6, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1647, N'Ksylolit bezspoinowy o grubości 30mm', 0.39, 0.39, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1648, N'Lastriko bezspoinowe o grubości 20mm', 0.44, 0.44, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1649, N'Parkiet mozaikowy lakierowany (na mozolepie polocecie lub butaprenie) o grubości 9mm', 0.09, 0.09, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1650, N'Parkiet mozaikowy lakierowany (na mozolepie polocecie lub butaprenie) o grubości 8mm', 0.08, 0.08, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1651, N'Płyty warstwowe klejone lakierowane o grubości 19mm (na lepiku)', 0.2, 0.2, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1652, N'Winyleum o grubości 2,8mm na butaprenie, polocecie', 0.05, 0.05, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1653, N'Wykładzina gumowa o grubości 4mm (na butaprenie)', 0.08, 0.08, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1654, N'Wykładzina wielowarstwowa z PCW o grubości 1,9mm (na polocecie, butaprenie)', 0.07, 0.07, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 30)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1655, N'Deszczułki podłogowe (na lepiku) o grubości 22mm', 0.23, 0.23, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 31)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1656, N'Deszczułki podłogowe (na lepiku) o grubości 19mm', 0.2, 0.2, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 31)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1657, N'Deszczułki podłogowe (na lepiku) o grubości 16mm', 0.17, 0.17, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 31)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1658, N'Deszczułki podłogowe (przybijane) o grubości 22mm', 0.21, 0.21, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 31)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1659, N'Płytki estrychgipsowe o grubości 25-30mm (na zaprawie cementowej 15-20mm)', 0.92, 0.92, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1660, N'Płytki ksylolitowe o grubości 20mm (na lepiku)', 0.47, 0.47, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1661, N'Płytki lastrikowe o grubości 20mm na zaprawie cementowej 1:3', 0.76, 0.76, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
GO
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1662, N'Płytki PCW o grubości 2 lub 3mm (na lateksie, polocecie, butaprenie)', 0.06, 0.07, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1663, N'Płytki kaminkowe na zaprawie cementowej 1:3 grubości 16-23mm - grubość płytek 14mm', 0.64, 0.64, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1664, N'Płytki kaminkowe na zaprawie cementowej 1:3 grubości 16-23mm - grubość płytek 10mm', 0.44, 0.44, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1665, N'Płytki kaminkowe na zaprawie cementowej 1:3 grubości 16-23mm - grubość płytek 7mm', 0.32, 0.32, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 48)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1666, N'Filc lub płyty z wełny mineralnej o grubości 30mm chronione papą smołową powlekaną, na podkładzie cementowym o grubości 30mm', 0.98, 0.98, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 49)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1667, N'Płyta pilśniowa porowata o grubości 12,6mm chroniona papą smołową powlekaną, na legarach drewnianych (między legarkami suchy piasek)', 0.53, 0.53, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1668, N'Płyta pilśniowa porowata o grubości 12,5mm chroniona papą smołową powlekaną na podkładzie cementowym o grubości 30mm', 0.74, 0.74, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1669, N'Płyta pilśniowa porowata o grubości 12,5mm chroniona papą smołową powlekaną na podkładzie gipsowym lub estrychgipsowym o grubości 35mm', 0.62, 0.62, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1670, N'Płyta pilśniowa porowata o grubości 12,5mm na podkładzie o grubości 40mm z gipsu prefabrykowanego', 0.52, 0.52, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1671, N'Płyty pilśniowe porowate o grubości 2x12,5mm chronione papą smołową powlekaną na podkładzie cementowym o grubości 35mm', 0.91, 0.91, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1672, N'Płyty pilśniowe porowate o grubości 2x12,5mm chronione papą smołową powlekaną na podkładzie gipsowym lub estrychgipsowym o grubości 40mm', 0.74, 0.74, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1673, N'Płyty pilśniowe porowate o grubości 2x12,5mm na podkładzie z gipsu prefabrykowanego o grubości 40mm', 0.56, 0.56, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 50)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1674, N'Styropian o grubości 10 lub 20mm na podkładzie cementowym o grubości 35mm', 0.81, 0.81, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1675, N'Styropian o grubości 10 lub 20mm na podkładzie cementowym o grubości 30mm', 0.7, 0.7, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1676, N'Styropian o grubości 10 lub 20mm na podkładzie gipsowym lub estrychgipsowym o grubości 35mm', 0.57, 0.57, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1677, N'Styropian o grubości 10 lub 20mm na podkładzie gipsowym prefabrykowanym o grubości 40mm', 0.49, 0.49, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1678, N'Styropian o grubości 10 lub 20mm na legarach drewnianych (między legarkami wypełnienie suchym piaskiem, warstwą o grubości 24mm)', 0.47, 0.47, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1679, N'Styropian o grubości 10 lub 20mm na legarach drewnianych (między legarkami wypełnienie suchym piaskiem, warstwą o grubości 24mm)', 0.65, 0.65, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1680, N'Styropian o grubości 20mm na podkładzie gipsowym prefabrykowanym o grubości 40mm', 0.5, 0.5, 2, N'PN-82-B-02001 Tab.Z2-2', NULL, 51)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1681, N'Żwiry pospółki - mało wilgotne - zagęszczone', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1682, N'Żwiry pospółki - mało wilgotne - średnio zagęszczone', 17.5, 17.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1683, N'Żwiry pospółki - mało wilgotne - luźne', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1684, N'Żwiry pospółki - wilgotne - zagęszczone', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1685, N'Żwiry pospółki - wilgotne - średnio zagęszczone', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1686, N'Żwiry pospółki - wilgotne - luźne', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1687, N'Żwiry pospółki - mokre - zagęszczone', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1688, N'Żwiry pospółki - mokre - średnio zagęszczone', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1689, N'Żwiry pospółki - mokre - luźne', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 52)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1690, N'Piaski grube i średnie - mało wilgotne - zagęszczone', 18, 18, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1691, N'Piaski grube i średnie - mało wilgotne - średnio zagęszczone', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1692, N'Piaski grube i średnie - mało wilgotne - luźne', 16.5, 16.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1693, N'Piaski grube i średnie - wilgotne - zagęszczone', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1694, N'Piaski grube i średnie - wilgotne - średnio zagęszczone', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1695, N'Piaski grube i średnie - wilgotne - luźne', 18, 18, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1696, N'Piaski grube i średnie - mokre - zagęszczone', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1697, N'Piaski grube i średnie - mokre - średnio zagęszczone', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1698, N'Piaski grube i średnie - mokre - luźne', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 53)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1699, N'Piaski drobne i pylaste - mało wilgotne - zagęszczone', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1700, N'Piaski drobne i pylaste - mało wilgotne - średnio zagęszczone', 16.5, 16.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1701, N'Piaski drobne i pylaste - mało wilgotne - luźne', 16, 16, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1702, N'Piaski drobne i pylaste - wilgotne - zagęszczone', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1703, N'Piaski drobne i pylaste - wilgotne - średnio zagęszczone', 17.5, 17.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1704, N'Piaski drobne i pylaste - wilgotne - luźne', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1705, N'Piaski drobne i pylaste - mokre - zagęszczone', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1706, N'Piaski drobne i pylaste - mokre - średnio zagęszczone', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1707, N'Piaski drobne i pylaste - mokre - luźne', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 54)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1708, N'Piaski próchnicze - mało wilgotne - zagęszczone', 16, 16, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1709, N'Piaski próchnicze - mało wilgotne - średnio zagęszczone', 15.5, 15.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1710, N'Piaski próchnicze - mało wilgotne - luźne', 15, 15, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1711, N'Piaski próchnicze - wilgotne - zagęszczone', 17.5, 17.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1712, N'Piaski próchnicze - wilgotne - średnio zagęszczone', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1713, N'Piaski próchnicze - wilgotne - luźne', 16.5, 16.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1714, N'Piaski próchnicze - mokre - zagęszczone', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1715, N'Piaski próchnicze - mokre - średnio zagęszczone', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1716, N'Piaski próchnicze - mokre - luźne', 17.5, 17.5, 3, N'PN-82-B-02001 Tab.Z3-1', NULL, 55)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1717, N'Żwiry, pospółki gliniaste - półzwarte', 22.5, 22.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 56)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1718, N'Żwiry, pospółki gliniaste - twardoplastyczne', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 56)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1719, N'Żwiry, pospółki gliniaste - plastyczny', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 56)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1720, N'Żwiry, pospółki gliniaste - miękkoplastyczny', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 56)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1721, N'Piaski gliniaste - półzwarte', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 57)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1722, N'Piaski gliniaste - twardoplastyczne', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 57)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1723, N'Piaski gliniaste - plastyczny', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 57)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1724, N'Piaski gliniaste - miękkoplastyczny', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 57)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1725, N'Pyły piaszczyste - półzwarte', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 58)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1726, N'Pyły piaszczyste - twardoplastyczne', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 58)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1727, N'Pyły piaszczyste - plastyczny', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 58)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1728, N'Pyły piaszczyste - miękkoplastyczny', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 58)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1729, N'Pyły - półzwarte', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 59)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1730, N'Pyły - twardoplastyczne', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 59)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1731, N'Pyły - plastyczny', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 59)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1732, N'Pyły - miękkoplastyczny', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 59)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1733, N'Gliny piaszczyste - półzwarte', 22.5, 22.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 60)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1734, N'Gliny piaszczyste - twardoplastyczne', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 60)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1735, N'Gliny piaszczyste - plastyczny', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 60)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1736, N'Gliny piaszczyste - miękkoplastyczny', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 60)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1737, N'Gliny - półzwarte', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 61)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1738, N'Gliny - twardoplastyczne', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 61)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1739, N'Gliny - plastyczny', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 61)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1740, N'Gliny - miękkoplastyczny', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 61)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1741, N'Gliny pylaste - półzwarte', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 62)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1742, N'Gliny pylaste - twardoplastyczneGliny pylaste - twardoplastyczne', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 62)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1743, N'Gliny pylaste - plastyczny', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 62)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1744, N'Gliny pylaste - miękkoplastyczny', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 62)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1745, N'Gliny piaszczyste zwięzłe - półzwarte', 22.5, 22.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 63)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1746, N'Gliny piaszczyste zwięzłe - twardoplastyczne', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 63)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1747, N'Gliny piaszczyste zwięzłe - plastyczny', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 63)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1748, N'Gliny piaszczyste zwięzłe - miękkoplastyczny', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 63)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1749, N'Gliny zwięzłe - półzwarte', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 64)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1750, N'Gliny zwięzłe - twardoplastyczne', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 64)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1751, N'Gliny zwięzłe - plastyczny', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 64)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1752, N'Gliny zwięzłe - miękkoplastyczny', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 64)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1753, N'Gliny pylaste zwięzłe - półzwarte', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 65)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1754, N'Gliny pylaste zwięzłe - twardoplastyczne', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 65)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1755, N'Gliny pylaste zwięzłe - plastyczny', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 65)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1756, N'Gliny pylaste zwięzłe - miękkoplastyczny', 18, 18, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 65)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1757, N'Iły piaszczyste - półzwarte', 22, 22, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 66)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1758, N'Iły piaszczyste - twardoplastyczne', 21, 21, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 66)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1759, N'Iły piaszczyste - plastyczny', 19.5, 19.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 66)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1760, N'Iły piaszczyste - miękkoplastyczny', 18, 18, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 66)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1761, N'Iły - półzwarte', 21.5, 21.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 67)
GO
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1762, N'Iły - twardoplastyczne', 20, 20, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 67)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1763, N'Iły - plastyczny', 18.5, 18.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 67)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1764, N'Iły - miękkoplastyczny', 17.5, 17.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 67)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1765, N'Iły pylaste - półzwarte', 20.5, 20.5, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 68)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1766, N'Iły pylaste - twardoplastyczne', 19, 19, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 68)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1767, N'Iły pylaste - plastyczny', 18, 18, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 68)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1768, N'Iły pylaste - miękkoplastyczny', 17, 17, 3, N'PN-82-B-02001 Tab.Z3-2', NULL, 68)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1769, N'Asfalt lany i beton asfaltowy', 24, 25, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 32)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1770, N'Kit asfaltowy', 18, 22, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 32)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1771, N'Asfalt wałowany na gorąco', 23, 23, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 32)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1772, N'Piasek (suchy)', 15, 16, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1773, N'Podsypka, żwir (luzem)', 15, 16, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1774, N'Gruz', 18.5, 19.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1775, N'Kruszony żużel', 13, 14.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1776, N'Ubijany tłuczeń kamienny', 20.5, 21.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1777, N'Urobiona glina', 18, 19.5, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 33)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1778, N'Ochronna warstwa betonowa', 25, 25, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 34)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1779, N'Zwykły balast (np.. Granit, gnejs itd..)', 20, 20, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 34)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1780, N'Balast bazaltowy', 26, 26, 3, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 34)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1781, N'Dwie szyny UIC 60', 1.2, 1.2, 1, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 35)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1782, N'Sprężone podkłady betonowe z zamocowaniem toru', 4.8, 4.8, 1, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 35)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1783, N'Podkłady drewniane z zamocowaniem toru', 1.9, 1.9, 1, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 35)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1784, N'2 szyny UIC 60 z zamocowaniem toru', 1.7, 1.7, 1, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 36)
INSERT [DeadLoads].[Materials] ([Id], [Name], [MinimumDensity], [MaximumDensity], [Unit], [DocumentName], [Comments], [SubcategoryId]) VALUES (1785, N'2 szyny UIC 60 z zamocowaniem toru, z mostownicą i z poręczą ochronną', 4.9, 4.9, 1, N'PN-EN 1991-1-1:2004 Tab.A.6', NULL, 36)
SET IDENTITY_INSERT [DeadLoads].[Materials] OFF
GO
SET IDENTITY_INSERT [DeadLoads].[Additions] ON 

INSERT [DeadLoads].[Additions] ([Id], [Name], [Description], [Value]) VALUES (1, N'Zwykły procent zbrojeni i stali sprężającej', NULL, 1)
INSERT [DeadLoads].[Additions] ([Id], [Name], [Description], [Value]) VALUES (2, N'Beton niestwardniały', NULL, 1)
INSERT [DeadLoads].[Additions] ([Id], [Name], [Description], [Value]) VALUES (3, N'Pokrycie bezkrokwiowe', NULL, -0.05)
SET IDENTITY_INSERT [DeadLoads].[Additions] OFF
GO
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1273)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1274)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1275)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1276)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1277)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1278)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1279)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (1, 1281)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1273)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1274)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1275)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1276)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1277)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1278)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1279)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (2, 1281)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1599)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1600)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1601)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1602)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1603)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1604)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1605)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1606)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1607)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1608)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1609)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1610)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1611)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1612)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1613)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1614)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1615)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1616)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1617)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1618)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1619)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1620)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1621)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1622)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1623)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1624)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1625)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1627)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1629)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1631)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1633)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1635)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1637)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1639)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1641)
INSERT [DeadLoads].[MaterialAdditions] ([AdditionId], [MaterialId]) VALUES (3, 1643)
GO
