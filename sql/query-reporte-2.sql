SELECT 
    r.Nombre AS RecetaNombre,
    COUNT(p.Id) AS NumeroPasos,
    p.Orden AS OrdenPaso,
    p.Descripcion AS DescripcionPaso
FROM 
    dbo.Recetas r
INNER JOIN 
    dbo.PasoPreparacions p ON r.Id = p.RecetaId
GROUP BY 
    r.Nombre, p.Orden, p.Descripcion
ORDER BY 
    r.Nombre, p.Orden;
