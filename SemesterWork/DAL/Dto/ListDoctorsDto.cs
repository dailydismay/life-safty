using System.ComponentModel.DataAnnotations;

namespace SemesterWork.DAL.Dto
{
    public class ListDoctorsDto : PageableArgs
    {
        public string Specialization { get; set; } = "";
    }
}