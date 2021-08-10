namespace Net5_Api.DTOs.Filme
{
    public class FilmeOutputGetByIdDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string NomeDoDiretor {get;set;}

        public FilmeOutputGetByIdDTO(int id, string titulo, string nomedodiretor)
        {   
            Id = id;
            Titulo = titulo;
            NomeDoDiretor = nomedodiretor;
        }
    }
}