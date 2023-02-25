using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data;

public class Repository : IRepository
{
    private readonly DataContext _context;
    public Repository(DataContext context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() > 0);
    }

    //METODOS DO ALUNO
#region Aluno
    public Aluno[] GetAllAlunos(bool incluiProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (incluiProfessor)
        {
            query = query.Include(a => a.AlunosDiciplinas)
                         .ThenInclude(ad => ad.Disciplina)
                         .ThenInclude(d => d.Professor);
        }

        query = query.AsNoTracking().OrderBy(a => a.Id);
        return query.ToArray();
    }


    public Aluno GetAlunoById(int alunoId, bool incluiProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (incluiProfessor)
        {
            query = query.Include(a => a.AlunosDiciplinas)
                         .ThenInclude(ad => ad.Disciplina)
                         .ThenInclude(d => d.Professor);
        }

        query = query.AsNoTracking()
                     .OrderBy(a => a.Id)
                     .Where(aluno => aluno.Id == alunoId);

        return query.FirstOrDefault();
    }
    public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluiProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (incluiProfessor)
        {
            query = query.Include(a => a.AlunosDiciplinas)
                         .ThenInclude(ad => ad.Disciplina)
                         .ThenInclude(d => d.Professor);
        }
        query = query.AsNoTracking()
                     .OrderBy(a => a.Id)
                     .Where(aluno => aluno.AlunosDiciplinas.Any(ad => ad.DisciplinaId == disciplinaId));
        return query.ToArray();
    }
#endregion

    //METODOS DO PROFESSOR
#region Professor
    
    public Professor[] GetAllProfessores(bool incluiAluno = false)
    {
        IQueryable<Professor> query = _context.Professores;

        if (incluiAluno)
        {
            query = query.Include(p => p.Disciplinas)
                         .ThenInclude(pd => pd.AlunosDiciplinas)
                         .ThenInclude(a => a.Aluno);
        }

        query = query.AsNoTracking().OrderBy(p => p.Id);
        return query.ToArray();
    }

    public Professor GetProfessorById(int professorId, bool incluiAluno = false)
    {
        IQueryable<Professor> query = _context.Professores;

        if (incluiAluno)
        {
            query = query.Include(p => p.Disciplinas)
                         .ThenInclude(pd => pd.AlunosDiciplinas)
                         .ThenInclude(a => a.Aluno);
        }

        query = query.AsNoTracking()
                     .OrderBy(p => p.Id)
                     .Where(professor => professor.Id == professorId);

        return query.FirstOrDefault();
    }

    public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluiAluno = false)
    {
        IQueryable<Professor> query = _context.Professores;

        if (incluiAluno)
        {
            query = query.Include(p => p.Disciplinas)
                         .ThenInclude(pd => pd.AlunosDiciplinas)
                         .ThenInclude(a => a.Aluno);
        }

        query = query.AsNoTracking()
                     .OrderBy(p => p.Id)
                     .Where(professor => professor.Disciplinas.Any(pd => pd.Id == disciplinaId));

        return query.ToArray();
    }
#endregion
}
