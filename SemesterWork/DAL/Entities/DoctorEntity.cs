using System;

namespace SemesterWork.DAL.Entities
{
    public class DoctorEntity : DbEntity
    {
        public bool IsVerified { get; set; }
        
        public long UserId { get; set; }
        
        public string Specialization { get; set; }
        public string Description { get; set; } = "";
        public DateTime StartTime { get; set; }
    }
}