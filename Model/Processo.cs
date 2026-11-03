using System.ComponentModel.DataAnnotations;

namespace AppWebGabriel.Model;

public class Processo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Número é obrigatório.")]
    public string? Numero { get; set; }

    [Required(ErrorMessage = "O campo Data é obrigatório.")]
    public DateOnly? Data { get; set; }

    [Required(ErrorMessage = "O campo Interessado é obrigatório.")]
    public string? Interessado { get; set; }

    [Required(ErrorMessage = "O campo Assunto é obrigatório.")]
    public string? Assunto { get; set; }

    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo Situação é obrigatório.")]
    public string? Situacao { get; set; }
}