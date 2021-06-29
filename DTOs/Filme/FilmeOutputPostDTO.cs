namespace Net5_Api.DTOs.Filme
{
    public class FilmeOutputPostDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public FilmeOutputPostDTO(long id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
}