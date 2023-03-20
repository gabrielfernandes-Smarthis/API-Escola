namespace SmartSchool.WebAPI.Dtos;

public class ProfessorRegistrarDto
{
    public int Id { get; set; }
    public int Registro { get; set; }

    //Informações do professor
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }

    public DateTime DataInicio { get; set; } = DateTime.Now;
    public DateTime? DataFim { get; set; } = null;

    public bool Ativo { get; set; } = true;

}
