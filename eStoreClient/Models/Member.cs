﻿namespace eStoreClient.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
    }
}