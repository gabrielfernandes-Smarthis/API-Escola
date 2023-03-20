namespace SmartSchool.WebAPI.Dtos;

/// <summary>
/// Esse e o DTO do aluno para registar seus dados.
/// </summary>
public class AlunoRegistrarDto
{
    /// <summary>
    /// Identificador chave do banco de dados
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Numero da matricula que o aluno possui
    /// </summary>
    public int Matricula { get; set; }

    //Informações do aluno
    /// <summary>
    /// Nome do aluno retorna seu primeiro nome e seu sobrenome registrados
    /// </summary>
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }

    public DateTime DataInicio { get; set; } = DateTime.Now;
    public DateTime? DataFim { get; set; } = null;

    public bool Ativo { get; set; } = true;
}
