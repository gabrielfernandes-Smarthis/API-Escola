using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    public readonly IRepository _repo;
    public readonly IMapper _mapper;
    public ProfessorController(IRepository repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _repo.GetAllProfessores(true);
        return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var professor = _repo.GetProfessorById(id);
        if (professor == null) return BadRequest("Esse professor não existe");

        var professorDto = _mapper.Map<ProfessorDto>(professor);

        return Ok(professorDto);
    }

    [HttpGet("mapper/{id}")]
    public IActionResult GetSemMapper(int id)
    {
        var professor = _repo.GetProfessorById(id);
        if (professor == null) return BadRequest("Esse professor não existe");

        return Ok(professor);
    }

    [HttpPost]
    public IActionResult Post(ProfessorRegistrarDto model)
    {
        var professor = _mapper.Map<Professor>(model);
        _repo.Add(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }
        
        return BadRequest("Professor não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ProfessorRegistrarDto model)
    {
        var professor = _repo.GetProfessorById(id);
        if (professor == null) return BadRequest("Professor não encontrado");

        _mapper.Map(model, professor);

        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }
        return BadRequest("Professor não foi salvo");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, ProfessorRegistrarDto model)
    {
        var professor = _repo.GetProfessorById(id);
        if (professor == null) return BadRequest("Professor não encontrado");

        _mapper.Map(model, professor);

        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
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
