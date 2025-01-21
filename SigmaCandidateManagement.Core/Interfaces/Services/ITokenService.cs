using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(string email, string role);
    }
}
