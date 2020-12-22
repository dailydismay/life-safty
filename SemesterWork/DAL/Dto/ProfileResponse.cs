using System;

namespace SemesterWork.DAL.Dto
{
    public class ProfileResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string FirstName
        {
            get;
            set;
        } = "";

        public string LastName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string BirthDate { get; set; }
    }
}