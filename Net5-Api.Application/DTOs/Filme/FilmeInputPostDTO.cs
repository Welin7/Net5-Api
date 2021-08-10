namespace Net5_Api.DTOs.Filme
{
    public class FilmeInputPostDTO
    {
        public string Titulo { get; set; }
        public long DiretorId { get; set; }
        public string Ano { get; set; }
        public FilmeInputPostDTO(string titulo, long diretorId, string ano)
        {
            Titulo = titulo;
            DiretorId = diretorId;
            Ano = ano;
        }
    }
}