﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR53_2020_POP2021.Services
{
    public interface IEntitet
    {
        void UcitajEntitet(string filename);
        void SacuvajEntitet(string filename);
        void IzbrisiEntitet(string jmbg);
    }
}
