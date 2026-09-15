// Defino el Dto de los parametros de paginacion
namespace TodoList_NET.Dtos;

public class DtoParam
{
    public int Page {get; set;} = 1;
    public int Limit {get; set;} = 10;
    public string? Search {get; set;} = string.Empty;
}