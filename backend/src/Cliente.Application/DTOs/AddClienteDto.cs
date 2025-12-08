namespace Cliente.Application.DTOs;

public class AddClienteDto
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? RazonSocial { get; set; }
    public string? Cuit { get; set; }
    public DateTime? FechaDeNacimiento { get; set; } 
    public string? Celular { get; set; }
    public string? Email { get; set; }
}
    
