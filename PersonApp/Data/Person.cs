﻿namespace PersonApp.Data
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public int? Age { get; set; }
    }
}
