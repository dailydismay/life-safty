using System;
using System.ComponentModel.DataAnnotations;

namespace SemesterWork.DAL.Dto
{
    public class EditProfileDto
    {
        [StringLength(32, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(32, MinimumLength = 2)]
        public string LastName { get; set; }

        [StringLength(32, MinimumLength = 2)]
        public string Patronymic { get; set; }
        
        public string BirthDate { get; set; }
    }
}