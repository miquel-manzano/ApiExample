﻿namespace T1_APIREST.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
