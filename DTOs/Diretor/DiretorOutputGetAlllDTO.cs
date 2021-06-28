namespace Net5_Api.DTOs.Diretor
{
    public class DiretorOutputGetAlllDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public DiretorOutputGetAlllDTO(long id, string nome)
        {       
            Id = id;
            Nome = nome;
        }
    }
}