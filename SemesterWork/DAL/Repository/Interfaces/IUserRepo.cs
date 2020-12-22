using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SemesterWork.DAL.Entities;

namespace SemesterWork.DAL.Repository.Interfaces
{
    public interface IUserRepo
    {
        public  Task<UserEntity> FindByEmail(string email);
    }
}
