using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public abstract class BaseLogic
    {
        protected readonly IMusicRepository repository;

        protected BaseLogic(IMusicRepository repository)
        {
            this.repository = repository;
        }

    }
}