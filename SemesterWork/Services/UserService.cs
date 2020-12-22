using System;
using System.Threading.Tasks;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;
using SemesterWork.DAL.Repository;

namespace SemesterWork.Services
{
    public class UserService
    {
        private UserRepo _userRepo;

        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        
        public async Task EditProfile(UserEntity userEntity, EditProfileDto data)
        {
            await _userRepo.Update(userEntity, new UserEntity()
            {
                FirstName = data.FirstName ?? userEntity.FirstName,
                LastName = data.LastName ?? userEntity.LastName,
                Patronymic = data.Patronymic ?? userEntity.Patronymic,
                BirthDate = data.BirthDate != "" ? Convert.ToDateTime(data.BirthDate) : userEntity.BirthDate
            });
        }
    }
}