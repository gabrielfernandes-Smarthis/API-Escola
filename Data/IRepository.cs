using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data;

public interface IRepository
{
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    bool SaveChanges();

    //METODOS DO ALUNO
    Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluiProfessor = false);
    Aluno[] GetAllAlunos(bool incluiProfessor = false);    
    Aluno[] GetAllAlunosByDisciplinaId(int alunoId, bool incluiProfessor = false);    
    Aluno GetAlunoById(int disciplinaId, bool incluiProfessor = false);    

    //METODOS DO PROFESSOR
    Professor[] GetAllProfessores(bool incluiAluno = false);    
    Professor[] GetAllProfessoresByDisciplinaId(int professorId, bool incluiAluno = false);    
    Professor GetProfessorById(int disciplinaId, bool incluiAluno = false);    

}
