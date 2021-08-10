using System.Collections.Generic;
namespace Net5_Api.Controllers.Model
{
    public class Diretor
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Filme> Filmes { get; set; }

        public Diretor(string nome) {
            Nome = nome;
            Filmes = new List<Filme>();
       }
   }
}