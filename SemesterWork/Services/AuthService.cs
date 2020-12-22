using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SemesterWork.DAL.Repository.Interfaces;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Domain;
using SemesterWork.DAL.Entities;

using System.Threading.Tasks;
using SemesterWork.DAL.Mapper;
using SemesterWork.DAL.Repository;

namespace SemesterWork.Services
{
    public class AuthService
    {
        private UserRepo userRepo { get; set; }
        private TokenService tokenService { get; set; }

        public AuthService(UserRepo userRepo, TokenService tokenService)
        {
            this.userRepo = userRepo;
            this.tokenService = tokenService;
        }

        public async Task<long> IdentifyUser(string token)
        {
            var id = tokenService.GetUserIdFromJwt(token);
            return id;
        }

        public async Task<UserEntity> GetUserById(long userId) => await userRepo.FindById(userId);
        
        public async Task<string> Login(LoginDto dto)
        {
            var user = await userRepo.FindByEmail(dto.Email);

            if (user == null)
            {
                //
            }

            var isPasswordCorrect = ComparePassword(dto.Password, user.Password);

            if (!isPasswordCorrect)
            {
                //
            }

            var payload = new JwtAuthPayload(user.Id);
            
            return tokenService.GenerateToken(payload);
        }

        public async Task Register(RegisterDto registerDto)
        {
            var user = await userRepo.FindByEmail(registerDto.Email);

            if (user != null)
            {
                //
            }

            var pwd = registerDto.Password;

            registerDto.Password = hashPassword(pwd);
    
            await userRepo.Create(UserEntityMapper.MapFromRegisterDto(registerDto));
        }

        private bool ComparePassword(string pwd, string hashed) => hashPassword(pwd).Equals(hashed);

        private string hashPassword(string pwd)
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pwd));  
  
            var result = md5.Hash;  

            var strBuilder = new StringBuilder();  
            
            foreach (var t in result)
            {
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();  
        }
    }
}
