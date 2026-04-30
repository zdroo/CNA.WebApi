SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
-- Fix user names
UPDATE Users SET FirstName = 'Admin', LastName = 'CNA' WHERE Email = 'admin@cna.com';
UPDATE Users SET FirstName = 'Maria', LastName = 'Ionescu' WHERE Id = '246A00B8-99B7-4B97-AA23-CBB3C793C708';

-- Update product descriptions
UPDATE Products SET Description = 'Colectie de manusi profesionale pentru saloane. Rezistente la substante chimice, fara pudra, testate dermatologic.' WHERE Id = 'C76295A8-7B1A-420F-AF30-260446BF05F4';
UPDATE Products SET Description = 'Uleiuri naturale pentru ingrijirea cuticulelor si hidratarea unghiilor. Formule concentrate cu extracte botanice.' WHERE Id = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';
UPDATE Products SET Description = 'Freze electrice profesionale pentru manichiura si pedichiura. Motor puternic, silentios, viteza reglabila.' WHERE Id = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';
UPDATE Products SET Description = 'Lampi UV/LED profesionale pentru polimerizarea ojelor semipermanente si gelurilor. Uscare rapida, distributie uniforma.' WHERE Id = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';
UPDATE Products SET Description = 'Oje semipermanente cu pigmentare intensa, rezistenta la cojire pana la 3 saptamani. Formula fara substante nocive.' WHERE Id = 'D312BA6B-D3D7-406D-8BDA-D298F314173A';
UPDATE Products SET Description = 'Capete de freza din materiale premium: carbura de tungsten, ceramica si diamant. Compatibile cu toate tipurile de freze.' WHERE Id = 'DA3EE901-078F-4313-AB70-663A78F3B1B8';

-- ================================================================
-- MANUSI PROFESIONALE variants
-- ================================================================
INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), 'C76295A8-7B1A-420F-AF30-260446BF05F4', 'Manusi Nitril Negre - Marime S', 'Kodi Professional', 18.99, 'MAN-NIT-NEG-S', 'manusi-nitril-negre-s', 'Manusi din nitril fara pudra, negre, 100 buc/cutie. Rezistente la produse chimice si solventi.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'C76295A8-7B1A-420F-AF30-260446BF05F4', 'Manusi Nitril Negre - Marime M', 'Kodi Professional', 18.99, 'MAN-NIT-NEG-M', 'manusi-nitril-negre-m', 'Manusi din nitril fara pudra, negre, 100 buc/cutie. Rezistente la produse chimice si solventi.', 1, 1, GETUTCDATE(), 0),
(NEWID(), 'C76295A8-7B1A-420F-AF30-260446BF05F4', 'Manusi Nitril Negre - Marime L', 'Kodi Professional', 18.99, 'MAN-NIT-NEG-L', 'manusi-nitril-negre-l', 'Manusi din nitril fara pudra, negre, 100 buc/cutie. Rezistente la produse chimice si solventi.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'C76295A8-7B1A-420F-AF30-260446BF05F4', 'Manusi Nitril Transparente - Marime M', 'Kodi Professional', 16.99, 'MAN-NIT-TRP-M', 'manusi-nitril-transparente-m', 'Manusi din nitril transparente, fara pudra, 100 buc/cutie. Confort ridicat pentru utilizare indelungata.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'C76295A8-7B1A-420F-AF30-260446BF05F4', 'Manusi Nitril Roz - Marime M', 'Kodi Professional', 19.99, 'MAN-NIT-ROZ-M', 'manusi-nitril-roz-m', 'Manusi din nitril roz deschis, fara pudra, 100 buc/cutie. Design elegant pentru salonul tau.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'MAN-NIT-NEG-S' THEN 80 WHEN 'MAN-NIT-NEG-M' THEN 150 WHEN 'MAN-NIT-NEG-L' THEN 90 WHEN 'MAN-NIT-TRP-M' THEN 120 WHEN 'MAN-NIT-ROZ-M' THEN 60 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'C76295A8-7B1A-420F-AF30-260446BF05F4';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Material','Nitril'),('Continut','100 buc/cutie'),('Tip','Fara pudra')) AS a(Name,Value)
WHERE pv.ProductId = 'C76295A8-7B1A-420F-AF30-260446BF05F4';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Marime', CASE Sku WHEN 'MAN-NIT-NEG-S' THEN 'S' WHEN 'MAN-NIT-NEG-M' THEN 'M' WHEN 'MAN-NIT-NEG-L' THEN 'L' WHEN 'MAN-NIT-TRP-M' THEN 'M' WHEN 'MAN-NIT-ROZ-M' THEN 'M' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'C76295A8-7B1A-420F-AF30-260446BF05F4';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Culoare', CASE Sku WHEN 'MAN-NIT-NEG-S' THEN 'Negru' WHEN 'MAN-NIT-NEG-M' THEN 'Negru' WHEN 'MAN-NIT-NEG-L' THEN 'Negru' WHEN 'MAN-NIT-TRP-M' THEN 'Transparent' WHEN 'MAN-NIT-ROZ-M' THEN 'Roz' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'C76295A8-7B1A-420F-AF30-260446BF05F4';

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/6290713/pexels-photo-6290713.jpeg',0),
  ('https://images.pexels.com/photos/5214413/pexels-photo-5214413.jpeg',1),
  ('https://images.pexels.com/photos/6290712/pexels-photo-6290712.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = 'C76295A8-7B1A-420F-AF30-260446BF05F4';

-- ================================================================
-- ULEIURI PENTRU CUTICULE variants
-- ================================================================
INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687', 'Ulei Cuticule Lavanda 15ml', 'Kodi Professional', 29.99, 'UCU-LAV-15', 'ulei-cuticule-lavanda-15ml', 'Ulei intensiv pentru cuticule cu extract de lavanda. Hidratare profunda, miros placut, absorbtie rapida.', 1, 1, GETUTCDATE(), 0),
(NEWID(), 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687', 'Ulei Cuticule Trandafir 15ml', 'Kodi Professional', 29.99, 'UCU-TRA-15', 'ulei-cuticule-trandafir-15ml', 'Ulei intensiv pentru cuticule cu ulei de trandafir. Regenerant si nutritiv, ideal pentru cuticule uscate.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687', 'Ulei Cuticule Piersica 15ml', 'Kodi Professional', 29.99, 'UCU-PIE-15', 'ulei-cuticule-piersica-15ml', 'Ulei cuticule cu aroma de piersica. Vitamina E si uleiuri naturale pentru unghii sanatoase.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687', 'Ulei Cuticule Vanilie 30ml', 'Victoria Vynn', 44.99, 'UCU-VAN-30', 'ulei-cuticule-vanilie-30ml', 'Ulei premium pentru cuticule cu extract de vanilie. Format 30ml economic pentru saloane profesionale.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687', 'Ulei Cuticule Menta 30ml', 'Victoria Vynn', 44.99, 'UCU-MEN-30', 'ulei-cuticule-menta-30ml', 'Ulei racoritor pentru cuticule cu extract de menta. Efect calmant, ideal pentru piele sensibila.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'UCU-LAV-15' THEN 200 WHEN 'UCU-TRA-15' THEN 150 WHEN 'UCU-PIE-15' THEN 130 WHEN 'UCU-VAN-30' THEN 80 WHEN 'UCU-MEN-30' THEN 70 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Tip produs','Ulei cuticule'),('Ingrediente','Ulei de jojoba, Vitamina E'),('Utilizare','Cuticule si unghii')) AS a(Name,Value)
WHERE pv.ProductId = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Volum', CASE Sku WHEN 'UCU-LAV-15' THEN '15 ml' WHEN 'UCU-TRA-15' THEN '15 ml' WHEN 'UCU-PIE-15' THEN '15 ml' WHEN 'UCU-VAN-30' THEN '30 ml' WHEN 'UCU-MEN-30' THEN '30 ml' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Aroma', CASE Sku WHEN 'UCU-LAV-15' THEN 'Lavanda' WHEN 'UCU-TRA-15' THEN 'Trandafir' WHEN 'UCU-PIE-15' THEN 'Piersica' WHEN 'UCU-VAN-30' THEN 'Vanilie' WHEN 'UCU-MEN-30' THEN 'Menta' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/7619379/pexels-photo-7619379.jpeg',0),
  ('https://images.pexels.com/photos/4041392/pexels-photo-4041392.jpeg',1),
  ('https://images.pexels.com/photos/3737591/pexels-photo-3737591.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = 'FB2EFC9B-91BD-4BC1-BB64-2CA72CFEF687';

-- ================================================================
-- FREZE ELECTRICE PROFESIONALE variants
-- ================================================================
INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), '17FE5F51-7EFE-4188-817E-2E09F0EA37F5', 'Freza Electrica 35000 rpm - Silver', 'NailPerfect', 249.99, 'FEL-35K-SLV', 'freza-electrica-35000rpm-silver', 'Freza electrica profesionala 35000 rpm, motor silentios, display digital, 6 viteze, include 5 piese freza.', 1, 1, GETUTCDATE(), 0),
(NEWID(), '17FE5F51-7EFE-4188-817E-2E09F0EA37F5', 'Freza Electrica 35000 rpm - Black', 'NailPerfect', 249.99, 'FEL-35K-BLK', 'freza-electrica-35000rpm-black', 'Freza electrica profesionala 35000 rpm, motor silentios, display digital, 6 viteze, include 5 piese freza.', 0, 1, GETUTCDATE(), 0),
(NEWID(), '17FE5F51-7EFE-4188-817E-2E09F0EA37F5', 'Freza Electrica 45000 rpm PRO - Rose Gold', 'Kodi Professional', 389.99, 'FEL-45K-RGD', 'freza-electrica-45000rpm-pro-rose-gold', 'Freza electrica premium 45000 rpm, sistem de racire activ, compatibila cu toate tipurile de freze.', 0, 1, GETUTCDATE(), 0),
(NEWID(), '17FE5F51-7EFE-4188-817E-2E09F0EA37F5', 'Freza Electrica 20000 rpm - Starter', 'NailPerfect', 149.99, 'FEL-20K-STR', 'freza-electrica-20000rpm-starter', 'Freza electrica pentru incepatori 20000 rpm, usoara si compacta, perfecta pentru manichiura acasa.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'FEL-35K-SLV' THEN 25 WHEN 'FEL-35K-BLK' THEN 20 WHEN 'FEL-45K-RGD' THEN 10 WHEN 'FEL-20K-STR' THEN 40 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Nivel zgomot','< 45 dB'),('Mandrina','2.35 mm universal'),('Alimentare','12V DC')) AS a(Name,Value)
WHERE pv.ProductId = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Turatie maxima', CASE Sku WHEN 'FEL-35K-SLV' THEN '35.000 rpm' WHEN 'FEL-35K-BLK' THEN '35.000 rpm' WHEN 'FEL-45K-RGD' THEN '45.000 rpm' WHEN 'FEL-20K-STR' THEN '20.000 rpm' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Culoare', CASE Sku WHEN 'FEL-35K-SLV' THEN 'Silver' WHEN 'FEL-35K-BLK' THEN 'Negru' WHEN 'FEL-45K-RGD' THEN 'Rose Gold' WHEN 'FEL-20K-STR' THEN 'Alb' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/4046316/pexels-photo-4046316.jpeg',0),
  ('https://images.pexels.com/photos/4149326/pexels-photo-4149326.jpeg',1),
  ('https://images.pexels.com/photos/4046313/pexels-photo-4046313.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = '17FE5F51-7EFE-4188-817E-2E09F0EA37F5';

-- ================================================================
-- LAMPI UV variants
-- ================================================================
INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA', 'Lampa UV/LED 36W - Alba', 'Semilac', 129.99, 'LUV-36W-ALB', 'lampa-uv-led-36w-alba', 'Lampa UV/LED 36W cu 12 leduri duale. Uscare in 30-60 secunde, senzor automat, timer 3 moduri.', 1, 1, GETUTCDATE(), 0),
(NEWID(), '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA', 'Lampa UV/LED 36W - Roz', 'Semilac', 129.99, 'LUV-36W-ROZ', 'lampa-uv-led-36w-roz', 'Lampa UV/LED 36W cu 12 leduri duale. Uscare in 30-60 secunde, senzor automat, timer 3 moduri.', 0, 1, GETUTCDATE(), 0),
(NEWID(), '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA', 'Lampa UV/LED 48W PRO - Alba', 'Kodi Professional', 199.99, 'LUV-48W-PRO', 'lampa-uv-led-48w-pro', 'Lampa UV/LED profesionala 48W, 24 leduri, uscare uniforma, compatibila cu toate gelurile si ojele semipermanente.', 0, 1, GETUTCDATE(), 0),
(NEWID(), '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA', 'Lampa UV/LED 54W Smart - Neagra', 'Victoria Vynn', 279.99, 'LUV-54W-SMA', 'lampa-uv-led-54w-smart', 'Lampa UV/LED smart 54W cu display, memorie setari, design premium. Uzura minima a ledurilor, garantie 2 ani.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'LUV-36W-ALB' THEN 50 WHEN 'LUV-36W-ROZ' THEN 35 WHEN 'LUV-48W-PRO' THEN 20 WHEN 'LUV-54W-SMA' THEN 8 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Timp uscare','30-60 secunde'),('Senzor miscare','Da'),('Compatibilitate','UV + LED')) AS a(Name,Value)
WHERE pv.ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Putere', CASE Sku WHEN 'LUV-36W-ALB' THEN '36W' WHEN 'LUV-36W-ROZ' THEN '36W' WHEN 'LUV-48W-PRO' THEN '48W' WHEN 'LUV-54W-SMA' THEN '54W' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Nr. leduri', CASE Sku WHEN 'LUV-36W-ALB' THEN '12 leduri duale' WHEN 'LUV-36W-ROZ' THEN '12 leduri duale' WHEN 'LUV-48W-PRO' THEN '24 leduri duale' WHEN 'LUV-54W-SMA' THEN '36 leduri duale' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Culoare', CASE Sku WHEN 'LUV-36W-ALB' THEN 'Alb' WHEN 'LUV-36W-ROZ' THEN 'Roz' WHEN 'LUV-48W-PRO' THEN 'Alb' WHEN 'LUV-54W-SMA' THEN 'Negru' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/3997373/pexels-photo-3997373.jpeg',0),
  ('https://images.pexels.com/photos/3997379/pexels-photo-3997379.jpeg',1),
  ('https://images.pexels.com/photos/3997381/pexels-photo-3997381.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = '29DA007D-0FC6-4C3F-89C2-58161CBBBDFA';

-- ================================================================
-- OJE SEMIPERMANENTE PROFESIONALE variants
-- ================================================================
INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Rosu Clasic 8ml', 'Semilac', 34.99, 'OJA-SMP-ROS-8', 'oja-semipermanenta-rosu-clasic-8ml', 'Oja semipermanenta rosu clasic intens. Rezistenta 3+ saptamani, fara fisuri sau cojire.', 1, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Roz Nude 8ml', 'Semilac', 34.99, 'OJA-SMP-RNS-8', 'oja-semipermanenta-roz-nude-8ml', 'Oja semipermanenta roz nude discret. Finish lucios, culoare eleganta si versatila.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Bordeaux 8ml', 'Semilac', 34.99, 'OJA-SMP-BOR-8', 'oja-semipermanenta-bordeaux-8ml', 'Oja semipermanenta bordeaux profund. Pigmentare intensa, culoare sofisticata pentru orice anotimp.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Alb Lapte 8ml', 'Semilac', 34.99, 'OJA-SMP-ALB-8', 'oja-semipermanenta-alb-lapte-8ml', 'Oja semipermanenta alb lapte crem. Perfect pentru manichiura french sau look minimalist.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Negru 8ml', 'Semilac', 34.99, 'OJA-SMP-NEG-8', 'oja-semipermanenta-negru-8ml', 'Oja semipermanenta negru mat/lucios. Pigmentare opaca, acoperire intr-un singur strat.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Coral 8ml', 'Victoria Vynn', 37.99, 'OJA-SMP-COR-8', 'oja-semipermanenta-coral-8ml', 'Oja semipermanenta coral vibrant Victoria Vynn. Formula premium 5-free, rezistenta superioara.', 0, 1, GETUTCDATE(), 0),
(NEWID(), 'D312BA6B-D3D7-406D-8BDA-D298F314173A', 'Oja Semipermanenta Lila 8ml', 'Victoria Vynn', 37.99, 'OJA-SMP-LIL-8', 'oja-semipermanenta-lila-8ml', 'Oja semipermanenta lila deschis Victoria Vynn. Nuanta moderna si feminina, ideal primavara-vara.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'OJA-SMP-ROS-8' THEN 300 WHEN 'OJA-SMP-RNS-8' THEN 250 WHEN 'OJA-SMP-BOR-8' THEN 200 WHEN 'OJA-SMP-ALB-8' THEN 180 WHEN 'OJA-SMP-NEG-8' THEN 220 WHEN 'OJA-SMP-COR-8' THEN 150 WHEN 'OJA-SMP-LIL-8' THEN 130 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'D312BA6B-D3D7-406D-8BDA-D298F314173A';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Volum','8 ml'),('Formula','5-Free'),('Rezistenta','3+ saptamani'),('Finish','Lucios')) AS a(Name,Value)
WHERE pv.ProductId = 'D312BA6B-D3D7-406D-8BDA-D298F314173A';

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), Id, 'Culoare', CASE Sku WHEN 'OJA-SMP-ROS-8' THEN 'Rosu clasic' WHEN 'OJA-SMP-RNS-8' THEN 'Roz nude' WHEN 'OJA-SMP-BOR-8' THEN 'Bordeaux' WHEN 'OJA-SMP-ALB-8' THEN 'Alb lapte' WHEN 'OJA-SMP-NEG-8' THEN 'Negru' WHEN 'OJA-SMP-COR-8' THEN 'Coral' WHEN 'OJA-SMP-LIL-8' THEN 'Lila' END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = 'D312BA6B-D3D7-406D-8BDA-D298F314173A';

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/1926620/pexels-photo-1926620.jpeg',0),
  ('https://images.pexels.com/photos/3997374/pexels-photo-3997374.jpeg',1),
  ('https://images.pexels.com/photos/3065209/pexels-photo-3065209.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = 'D312BA6B-D3D7-406D-8BDA-D298F314173A';

-- ================================================================
-- NEW PRODUCT: Pile si Instrumente (Forfecute si Pile category: B22599F9)
-- ================================================================
DECLARE @prodPile UNIQUEIDENTIFIER = NEWID();
INSERT INTO Products (Id, Name, Description, CategoryId, IsShippable, IsDigital, IsReturnable, IsActive, Slug, CreatedAt, IsDeleted)
VALUES (@prodPile, 'Pile si Instrumente Manichiura', 'Set complet de pile, spatule si instrumente profesionale pentru ingrijirea unghiilor naturale si artificiale.', 'B22599F9-7F08-4882-9883-D5AA71085D57', 1, 0, 1, 1, 'pile-instrumente-manichiura', GETUTCDATE(), 0);

INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), @prodPile, 'Pila Unghii Buffer 100/180 Grit', 'Moyra', 8.99, 'PIL-BUF-100', 'pila-unghii-buffer-100-180', 'Pila dubla fata 100/180 grit pentru modelarea si finisarea unghiilor. Abrazivitate optima.', 0, 1, GETUTCDATE(), 0),
(NEWID(), @prodPile, 'Pile Banana 100/180 - Set 10 buc', 'Moyra', 19.99, 'PIL-BAN-SET10', 'pile-banana-100-180-set-10', 'Set 10 pile banana profesionale 100/180 grit. Forma ergonomica, usor de utilizat.', 1, 1, GETUTCDATE(), 0),
(NEWID(), @prodPile, 'Buffer Lustruire 4 Fete', 'Moyra', 12.99, 'PIL-BUF-4F', 'buffer-lustruire-4-fete', 'Buffer cu 4 fete abrazive: pilire, nivelare, finisare si lustruire. Suprafata perfecta garantata.', 0, 1, GETUTCDATE(), 0),
(NEWID(), @prodPile, 'Forfecuta Cuticule Inox Profesionala', 'Kodi Professional', 49.99, 'FOR-CUT-INX', 'forfecuta-cuticule-inox-profesionala', 'Forfecuta din inox chirurgical pentru cuticule. Lame de precizie, grip ergonomic anti-alunecare.', 0, 1, GETUTCDATE(), 0),
(NEWID(), @prodPile, 'Impingator Cuticule Dublu Metal', 'Kodi Professional', 24.99, 'IMP-CUT-DUB', 'impingator-cuticule-dublu-metal', 'Impingator cuticule cu 2 capete: spatula plata si impingator rotund, din metal rezistent.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'PIL-BUF-100' THEN 500 WHEN 'PIL-BAN-SET10' THEN 300 WHEN 'PIL-BUF-4F' THEN 200 WHEN 'FOR-CUT-INX' THEN 60 WHEN 'IMP-CUT-DUB' THEN 80 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = @prodPile;

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Material','Profesional'),('Utilizare','Salon si acasa')) AS a(Name,Value)
WHERE pv.ProductId = @prodPile;

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/4041393/pexels-photo-4041393.jpeg',0),
  ('https://images.pexels.com/photos/4149330/pexels-photo-4149330.jpeg',1),
  ('https://images.pexels.com/photos/4149332/pexels-photo-4149332.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = @prodPile;

-- ================================================================
-- NEW PRODUCT: Manusi Protectie UV (Manusi si protectie: 08B37C22)
-- ================================================================
DECLARE @prodManProt UNIQUEIDENTIFIER = NEWID();
INSERT INTO Products (Id, Name, Description, CategoryId, IsShippable, IsDigital, IsReturnable, IsActive, Slug, CreatedAt, IsDeleted)
VALUES (@prodManProt, 'Manusi Protectie UV si Salon', 'Manusi speciale pentru protectie in salon: protectie UV pentru lampi, anti-statice si termorezistente.', '08B37C22-0846-4CED-97D8-E3F7C3BA21A9', 1, 0, 1, 1, 'manusi-protectie-uv-salon', GETUTCDATE(), 0);

INSERT INTO ProductVariants (Id, ProductId, Name, Brand, Price, Sku, Slug, Description, IsFeatured, IsActive, CreatedAt, IsDeleted) VALUES
(NEWID(), @prodManProt, 'Manusi Protectie UV Anti-imbatranire', 'Semilac', 39.99, 'MAN-UV-ANTI', 'manusi-protectie-uv-anti-imbatranire', 'Manusi din lycra pentru protejarea mainilor la lampi UV/LED. Protectie SPF50+, lavabile.', 1, 1, GETUTCDATE(), 0),
(NEWID(), @prodManProt, 'Manusi Anti-statice ESD Profesionale', 'Kodi Professional', 29.99, 'MAN-ESD-PRO', 'manusi-anti-statice-esd-profesionale', 'Manusi anti-electrostatice pentru lucrul cu dispozitive electronice. 5 degete libere pentru precizie.', 0, 1, GETUTCDATE(), 0),
(NEWID(), @prodManProt, 'Manusi Termorezistente Salon', 'Kodi Professional', 44.99, 'MAN-TERM-SAL', 'manusi-termorezistente-salon', 'Manusi termorezistente pentru manipularea in procese termice. Rezistenta pana la 230 grade C.', 0, 1, GETUTCDATE(), 0);

INSERT INTO Stocks (Id, ProductVariantId, Quantity, CreatedAt, IsDeleted)
SELECT NEWID(), Id, CASE Sku WHEN 'MAN-UV-ANTI' THEN 100 WHEN 'MAN-ESD-PRO' THEN 75 WHEN 'MAN-TERM-SAL' THEN 50 END, GETUTCDATE(), 0
FROM ProductVariants WHERE ProductId = @prodManProt;

INSERT INTO VariantAttributes (Id, ProductVariantId, Name, Value, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, a.Name, a.Value, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES ('Spalare','Lavabil la 30 grade'),('Marime','Universala')) AS a(Name,Value)
WHERE pv.ProductId = @prodManProt;

INSERT INTO VariantImages (Id, ProductVariantId, Url, SortOrder, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, img.Url, img.SortOrder, GETUTCDATE(), 0
FROM ProductVariants pv CROSS JOIN (VALUES
  ('https://images.pexels.com/photos/6290713/pexels-photo-6290713.jpeg',0),
  ('https://images.pexels.com/photos/5214413/pexels-photo-5214413.jpeg',1),
  ('https://images.pexels.com/photos/5215017/pexels-photo-5215017.jpeg',2)
) AS img(Url,SortOrder) WHERE pv.ProductId = @prodManProt;

-- ================================================================
-- COUNTRIES
-- ================================================================
INSERT INTO Countries (Id, Name, CountryCode, CurrencyCode, PhonePrefix, IsShippingAvailable, CreatedAt, IsDeleted) VALUES
(NEWID(), 'Romania', 'RO', 'RON', '+40', 1, GETUTCDATE(), 0),
(NEWID(), 'Moldova', 'MD', 'MDL', '+373', 1, GETUTCDATE(), 0),
(NEWID(), 'Bulgaria', 'BG', 'BGN', '+359', 1, GETUTCDATE(), 0),
(NEWID(), 'Ungaria', 'HU', 'HUF', '+36', 1, GETUTCDATE(), 0),
(NEWID(), 'Germania', 'DE', 'EUR', '+49', 1, GETUTCDATE(), 0),
(NEWID(), 'Austria', 'AT', 'EUR', '+43', 1, GETUTCDATE(), 0),
(NEWID(), 'Italia', 'IT', 'EUR', '+39', 1, GETUTCDATE(), 0),
(NEWID(), 'Spania', 'ES', 'EUR', '+34', 1, GETUTCDATE(), 0),
(NEWID(), 'Franta', 'FR', 'EUR', '+33', 1, GETUTCDATE(), 0),
(NEWID(), 'Olanda', 'NL', 'EUR', '+31', 1, GETUTCDATE(), 0),
(NEWID(), 'Belgia', 'BE', 'EUR', '+32', 1, GETUTCDATE(), 0),
(NEWID(), 'Grecia', 'GR', 'EUR', '+30', 1, GETUTCDATE(), 0);

-- ================================================================
-- REVIEWS
-- ================================================================
DECLARE @u1 UNIQUEIDENTIFIER = 'B7A4966E-B3BF-43F9-88D5-67F0D08E20D6';
DECLARE @u2 UNIQUEIDENTIFIER = '246A00B8-99B7-4B97-AA23-CBB3C793C708';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Produs excelent! Calitate superioara, exact cum m-am asteptat. Le comand mereu.', DATEADD(day, -5, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'MAN-NIT-NEG-M';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u1, 4, 'Calitate buna, manusile sunt rezistente si confortabile. Recomand!', DATEADD(day, -12, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'MAN-NIT-NEG-M';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Freza silentioasa, nu incalzeste unghia, clientele nu simt disconfort. Excelenta!', DATEADD(day, -3, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'FEL-35K-SLV';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u1, 5, 'Cea mai buna freza pe care am folosit-o. Merita fiecare leu!', DATEADD(day, -8, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'FEL-45K-RGD';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Lampa usuca rapid si uniform. Nu am mai avut probleme cu unghii nefinisate.', DATEADD(day, -10, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'LUV-36W-ALB';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u1, 4, 'Buna lampa, dar cablul e putin scurt. Altfel, usuca perfect in 30 secunde.', DATEADD(day, -20, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'LUV-48W-PRO';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Culoarea e exacta ca in poza, tine minunat 3+ saptamani fara cojire!', DATEADD(day, -7, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'OJA-SMP-ROS-8';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u1, 5, 'Nuanta superba, pigmentare intensa, se aplica usor. Clientele sunt incantate!', DATEADD(day, -15, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'OJA-SMP-RNS-8';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Uleiul e minunat, miroase frumos si se absoarbe rapid. Il recomand cu caldura.', DATEADD(day, -4, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'UCU-LAV-15';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u1, 4, 'Produs bun, cuticula se inmoaie vizibil dupa cateva aplicari. Raport pret/calitate excelent.', DATEADD(day, -18, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'UCU-TRA-15';

INSERT INTO Reviews (Id, ProductVariantId, UserId, Rating, Comment, CreatedAt, IsDeleted)
SELECT NEWID(), pv.Id, @u2, 5, 'Pilele sunt de calitate, nu se uzeaza repede. Setul de 10 este super economic.', DATEADD(day, -9, GETUTCDATE()), 0
FROM ProductVariants pv WHERE pv.Sku = 'PIL-BAN-SET10';
