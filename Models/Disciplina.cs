namespace SmartSchool.WebAPI.Models;

public class Disciplina
{
    public Disciplina() { }

    public Disciplina(int id, string nome, int professorId, int cursoId)
    {
        this.Id = id;
        this.Nome = nome;
        this.ProfessorId = professorId;
        this.CursoId = cursoId;
    }
    public int Id { get; set; }
    public string Nome { get; set; }

    public int CargaHoraria { get; set; }
    //Relacionamento entre diciplina e a propria disciplina para verificar o
    //prerequisito para realizar o curso.
    public int? PrerequisitoId { get; set; } = null;
    public Disciplina Prerequisito { get; set; }
    
    //Relacionemento entre dicuplina e o professor
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }

    //Relacionamento entre diciplina e o curso 
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    //Lista de alunos que estão matriculados na disciplina
    public IEnumerable<AlunoDisciplina> AlunosDiciplinas { get; set; }
}
