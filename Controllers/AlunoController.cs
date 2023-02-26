using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    public readonly IRepository _repo;

    public AlunoController(IRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repo.GetAllAlunos(true);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var aluno = _repo.GetAlunoById(id);
        if (aluno == null) return BadRequest("O aluno não foi encontrado.");

        return Ok(aluno);
    }

    //USANDO O REPOSITORY

    [HttpPost]
    public IActionResult Post(Aluno aluno)
    {
        _repo.Add(aluno);
        if (_repo.SaveChanges())
        {
            return Ok(aluno);
        }
        
        return BadRequest("Aluno não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Aluno aluno)
    {
        var alunoById = _repo.GetAlunoById(id);
        if (alunoById == null) return BadRequest("Aluno não encontrado");
        if (alunoById.Id != aluno.Id) return BadRequest("O id do aluno incorreto");

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Ok(aluno);
        }
        
        return BadRequest("Aluno não alterado");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Aluno aluno)
    {
        var alunoById = _repo.GetAlunoById(id);
        if (alunoById == null) return BadRequest("Aluno não encontrado");
        if (alunoById.Id != aluno.Id) return BadRequest("O id do aluno incorreto");

        _repo.Update(aluno);
        if (_repo.SaveChanges())
        {
            return Ok(aluno);
        }
        
        return BadRequest("Aluno não alterado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delet(int id)
    {
        var alunoById = _repo.GetAlunoById(id);
        if (alunoById == null) return BadRequest("Aluno não encontrado");

        _repo.Delete(alunoById);
        if (_repo.SaveChanges())
        {
            return Ok("Aluno Deletado");
        }
        
        return BadRequest("Aluno não deletado");
    }
}