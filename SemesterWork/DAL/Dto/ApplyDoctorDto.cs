using System;
using System.ComponentModel.DataAnnotations;

namespace SemesterWork.DAL.Dto
{
    public class ApplyDoctorDto
    {
        [StringLength(32, MinimumLength = 2)]
        public string Specialization { get; set; }

        [StringLength(32, MinimumLength = 2)] 
        public string Description { get; set; } = "";
        [Timestamp] 
        public DateTime StartTime { get; set; }
    }
}