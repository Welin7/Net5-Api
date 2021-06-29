namespace Net5_Api.Controllers.Model
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }

        public long DiretorId { get; set; }
        public Diretor Diretor { get; set; }
        public Filme(string titulo, long diretorId)
        {
            this.Titulo = titulo;
            DiretorId = diretorId;

        }
    }
}