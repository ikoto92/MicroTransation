﻿namespace MicroTransation.DTOs
{
    public class UserCreateDTO
    {       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }     
    }
    public class UserAuthDTO 
    { 
        public string Name { get;  set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
