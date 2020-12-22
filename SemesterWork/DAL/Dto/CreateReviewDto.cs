namespace SemesterWork.DAL.Dto
{
    public class CreateReviewDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}