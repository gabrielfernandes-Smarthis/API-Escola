using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor() { }

        public Professor(int id, int registro, string nome, string sobrenome) 
        {
            this.Id = id;
            this.Registro = registro;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
        }
        public int Id { get; set; }
        public int Registro { get; set; }

        //Informações do professor
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;

        //Lista de diciplinas que o professor mestra
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}