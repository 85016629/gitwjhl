﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Auth
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>() {
        new User {  Id=1, Name="alice", Password="alice", Email="alice@gmail.com", PhoneNumber="18800000001" },
        new User {  Id=1, Name="bob", Password="bob", Email="bob@gmail.com", PhoneNumber="18800000002" }
    };

        public User FindUser(string userName, string password)
        {
            return _users.FirstOrDefault(_ => _.Name == userName && _.Password == password);
        }
    }
}