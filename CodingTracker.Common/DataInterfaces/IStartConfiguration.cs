﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Common.DataInterfaces
{
    public interface IStartConfiguration
    {
        public string ConnectionString { get; }

        void LoadConfiguration();
    }
}