WITH CategoryCTE AS (
    SELECT 
        CategoryGuid,
        CategoryName,
        ParentCategoryGuid,
        CategoryName AS RootCategoryName
    FROM 
        Category
    WHERE 
        ParentCategoryGuid IS NULL
    UNION ALL
    SELECT 
        C.CategoryGuid,
        C.CategoryName,
        C.ParentCategoryGuid,
        RC.RootCategoryName
    FROM 
        Category AS C
    INNER JOIN 
        CategoryCTE AS RC ON C.ParentCategoryGuid = RC.CategoryGuid
)
SELECT 
    RC.RootCategoryName AS [Root Category Name Of Product],
    C1.CategoryName AS [Immediate Category Name Of Product],
    P.ProductName AS [Product Name]
FROM 
    Product AS P
LEFT JOIN 
    CategoryCTE AS RC ON P.CategoryGuid = RC.CategoryGuid
LEFT JOIN 
    Category AS C1 ON P.CategoryGuid = C1.CategoryGuid
	WHERE  RC.RootCategoryName IS NOT NULL
ORDER BY 
    [Root Category Name Of Product], 
    [Immediate Category Name Of Product], 
    [Product Name];