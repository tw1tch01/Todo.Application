﻿using System;

namespace Todo.Persistence.MySQL.Options
{
    public class MySqlOptions
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public MySqlVersion Version { get; set; }
    }
}