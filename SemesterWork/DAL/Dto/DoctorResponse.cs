using SemesterWork.DAL.Entities;

namespace SemesterWork.DAL.Dto
{
    public class DoctorResponse
    {
        public ProfileResponse Profile { get; set; }
        public DoctorEntity Doctor { get; set; }
    }
}