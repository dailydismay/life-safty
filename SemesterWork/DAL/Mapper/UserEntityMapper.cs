using System;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;

namespace SemesterWork.DAL.Mapper
{
    public static class UserEntityMapper
    {
        public static ProfileResponse MapUserToProfileResponse(UserEntity userEntity)
        {
            return new ProfileResponse()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                CreatedAt = userEntity.CreatedAt,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Patronymic = userEntity.Patronymic,
                BirthDate =  userEntity.BirthDate.ToString(),
            };
        }
        
        public static UserEntity MapFromRegisterDto(RegisterDto dto)
        {
            return new UserEntity()
            { 
                CreatedAt = DateTime.Now,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
        }
    }
}