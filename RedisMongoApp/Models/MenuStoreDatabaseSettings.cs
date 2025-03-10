﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisMongoApp.Models
{


    public class MenuStoreDatabaseSettings : IMenuStoreDatabaseSettings
    {
        public string MenuCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMenuStoreDatabaseSettings
    {
        string MenuCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
