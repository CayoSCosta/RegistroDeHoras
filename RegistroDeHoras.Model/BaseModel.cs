using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeHoras.Model;

public class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Ativo { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeModificacao { get; set; }
}
