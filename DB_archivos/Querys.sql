SELECT p.Codigo,p.Nombre,p.Descripcion,m.Nombre,p.PrecioUnitario,p.UrlImange
FROM Productos AS p
JOIN Marcas AS m ON m.Id=p.IdMarca
WHERE p.Activo=1

SELECT * FROM Productos

SELECT p.Codigo,p.Nombre,p.Descripcion,p.IdMarca,m.Nombre,p.PrecioUnitario,p.Activo,p.UrlImange FROM Productos AS p JOIN Marcas AS m ON m.Id=p.IdMarca WHERE p.Activo=1

SELECT Codigo,Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange FROM Productos WHERE Activo=1

INSERT INTO Productos(Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange) VALUES ('Mouse M535','Mouse Logitech M535 | Bluetooth | 1000 DPI',3,'1400.00',1,'https://www.google.com/url?sa=i&url=https%3A%2F%2Fforward-tech.com.ar')

INSERT INTO Marcas(Nombre) VALUES ('Genius'),('HyperX'),('Kingston'),('SanDisk'),('Samsung'),('ViewSonic'),('LG'),('Xiaomi'),('Motorola'),('TRV'),('HP'),('Asus'),('Lenovo'),('Acer'),('Asus'),('Gigabyte'),('Intel'),('AMD')

SELECT TOP 1 * FROM PRODUCTOS ORDER BY PrecioUnitario DESC

SELECT Id,Nombre FROM Marcas ORDER BY Nombre ASC

SELECT COUNT(Nombre) FROM Marcas WHERE Nombre='AMD'