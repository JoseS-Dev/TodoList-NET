namespace TodoList_NET.Dtos;

// Defino el Dto de creación de una categoria
public class DtoCategoryCreate
{
    public int Id {get; set;}
    public string NameCategory {get; set;} = string.Empty;
    public string? DescriptionCategory {get; set;} = string.Empty;
}

// Defino el Dto de actualización de una categoria
public class DtoCategoryUpdate
{
    public string? NameCategory {get; set;} = string.Empty;
    public string? DescriptionCategory {get; set;} = string.Empty;
}