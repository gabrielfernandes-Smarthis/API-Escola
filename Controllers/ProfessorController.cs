using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly DataContext _context;
    public readonly IRepository _repo;
    public ProfessorController(DataContext context, IRepository repo)
    {
        _repo = repo;
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Professores);
    }

    [HttpGet("byId/{id}")]
    public IActionResult GetById(int id)
    {
        var professor = _context.Professores.FirstOrDefault(P => P.Id == id);
        if (professor == null) return BadRequest("Esse professor não existe");

        return Ok(professor);
    }

    [HttpGet("byNome")]
    public IActionResult GetByNome(string nome)
    {
        var professor = _context.Professores.FirstOrDefault(P =>
            P.Nome.Contains(nome)
        );
        if (professor == null) return BadRequest("Esse professor não existe");

        return Ok(professor);
    }

    [HttpPost]
    public IActionResult Post(Professor professor)
    {
        _repo.Add(professor);
        if (_repo.SaveChanges())
        {
            return Ok(professor);
        }
        
        return BadRequest("Professor não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Professor professor)
    {
        var professorById = _context.Professores.AsNoTracking().FirstOrDefault(P => P.Id == id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        if (professor.Id != id) return BadRequest("Id do professor está incorreto");

        _context.Update(professor);
        _context.SaveChanges();
        return Ok(professor);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Professor professor)
    {
        var professorById = _context.Professores.AsNoTracking().FirstOrDefault(P => P.Id == id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        if (professor.Id != id) return BadRequest("Id do professor está incorreto");

        _context.Update(professor);
        _context.SaveChanges();
        return Ok(professor);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var professorById = _context.Professores.AsNoTracking().FirstOrDefault(P => P.Id == id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        _context.Remove(professorById);
        _context.SaveChanges();
        return Ok();
    }
}
