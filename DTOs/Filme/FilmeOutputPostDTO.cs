namespace Net5_Api.DTOs.Filme
{
    public class FilmeOutputPostDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public FilmeOutputPostDTO(long id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public FilmeOutputPostDTO(long id, string titulo, string ano)
        {
            this.Id = id;
            Titulo = titulo;
            this.Ano = ano;
        }
    }
}