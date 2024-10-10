SELECT 
    r.Nombre AS RecetaNombre,
    r.Descripcion AS RecetaDescripcion,
    r.TiempoEstimadoPreparacion AS TiempoPreparacion,
    i.Nombre AS IngredienteNombre,
    i.Cantidad,
    i.UnidadMedida
FROM 
    dbo.Recetas r
INNER JOIN 
    dbo.Ingredientes i ON r.Id = i.RecetaId
-- WHERE 
--    r.TiempoEstimadoPreparacion <= @TiempoMaximo
ORDER BY 
    r.Nombre, i.Nombre;
