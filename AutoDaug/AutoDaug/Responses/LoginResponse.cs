﻿namespace AutoDaug.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
