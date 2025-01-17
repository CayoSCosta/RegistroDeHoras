namespace RegistroDeHoras.Model;

public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Ativo { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeModificacao { get; set; }
}
