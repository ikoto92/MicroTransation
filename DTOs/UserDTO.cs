namespace MicroTransation.DTOs
{
    public class UserCreateDTO
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string getName()
        {
            return FristName+LastName;
        }
       
    }
    public class UserAuthDTO 
    { 
        public string Name { get;  set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
