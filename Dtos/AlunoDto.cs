namespace SmartSchool.WebAPI.Dtos;

public class AlunoDto
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
    /// 
    /// </summary>
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public int Idade { get; set; }

    public DateTime DataInicio { get; set; }

    public bool Ativo { get; set; }
}
