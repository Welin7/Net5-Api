namespace Net5_Api.DTOs.Filme
{
    public class FilmeOutputGetAllDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }

        public FilmeOutputGetAllDTO(int id, string titulo, string ano)
        {
            Id = id;
            Titulo = titulo;
            Ano = ano;
        }
    }
}