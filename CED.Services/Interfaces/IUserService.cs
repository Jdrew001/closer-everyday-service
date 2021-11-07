using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models;

namespace CED.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> SearchForUser(string param);
    }
}
