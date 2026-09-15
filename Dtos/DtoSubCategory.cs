namespace TodoList_NET.Dtos;

// Defino el dto de creación de una subcategoria
public class DtoSubCategoryCreate
{
   public int CategoryId {get; set;}
   public string NameSubCategory {get; set;} = string.Empty;
   public string? DescriptionSubCategory {get; set;} = string.Empty; 
}

// Defino el dto de actualización de una subcategoria
public class DtoSubCategoryUpdate
{
   public int CategoryId {get; set;}
   public string? NameSubCategory {get; set;} = string.Empty;
   public string? DescriptionSubCategory {get; set;} = string.Empty; 
}

// Defino el dto de la respuesta de una subcategoria
public class DtoSubCategoryResponse
{
   public int Id {get; set;}
   public int CategoryId {get; set;}
   public string NameSubCategory {get; set;} = string.Empty;
   public string? DescriptionSubCategory {get; set;} = string.Empty; 
}