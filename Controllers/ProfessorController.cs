using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    public readonly IRepository _repo;
    public ProfessorController(IRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repo.GetAllProfessores(true);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var professor = _repo.GetProfessorById(id);
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
        var professorById = _repo.GetProfessorById(id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        if (professor.Id != id) return BadRequest("Id do professor está incorreto");

        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Ok(professor);
        }
        return BadRequest("Professor não foi salvo");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Professor professor)
    {
        var professorById = _repo.GetProfessorById(id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        if (professor.Id != id) return BadRequest("Id do professor está incorreto");

        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Ok(professor);
        }
        return BadRequest("Professor não foi salvo");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var professorById = _repo.GetProfessorById(id);
        if (professorById == null) return BadRequest("Professor não encontrado");
        
        _repo.Delete(professorById);
        if (_repo.SaveChanges())
        {
            return Ok(professorById);
        }
        return BadRequest("Não foi possviel escluir o professor");
    }
}
