namespace Net5_Api.DTOs.Diretor
{
    public class DiretorOutPutPutDTO
    {
        public long Id {get;set;}
        public string Nome {get;set;}

        public DiretorOutPutPutDTO(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}