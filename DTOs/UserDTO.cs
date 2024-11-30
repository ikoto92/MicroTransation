namespace MicroTransation.DTOs
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UserCreateDTO
    {       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }     
    }
    public class UserAuthDTO 
    {      
        public string Email { get;  set; }
        public string Password { get; set; }
    }
    public class UserUpdateName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserUpdateEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
    public class UserUpdatePassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
