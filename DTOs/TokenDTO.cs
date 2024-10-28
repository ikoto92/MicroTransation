namespace MicroTransation.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime emissionDate { get; set; }
        public DateTime expirationDate { get; set; }

       public UserAuthDTO UserAuth { get; set; }    
    }
}
