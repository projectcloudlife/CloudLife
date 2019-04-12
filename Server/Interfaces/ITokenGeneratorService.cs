using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface ITokenGeneratorService
    {
        int UserId { get; set; }

        Task<string> CreateToken();
    }
}
