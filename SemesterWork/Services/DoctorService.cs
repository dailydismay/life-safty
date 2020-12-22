using System.Collections.Generic;
using System.Threading.Tasks;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;
using SemesterWork.DAL.Repository;

namespace SemesterWork.Services
{
    public class DoctorService
    {
        private DoctorRepo DoctorRepo { get; set; }

        public DoctorService(DoctorRepo doctorRepo)
        {
            DoctorRepo = doctorRepo;
        }

        public Task<List<string>> ListAllSpecializations()
        {
            return this.DoctorRepo.ListSpecializations();
        }

        public async Task Create(long userId, ApplyDoctorDto dto)
        {
            await DoctorRepo.Create(new DoctorEntity()
            {
                UserId = userId,
                Description = dto.Description,
                Specialization = dto.Specialization,
                StartTime = dto.StartTime.ToString() == "" ? default : dto.StartTime
            });
        }

        public async Task<Pageable<DoctorResponse>> List(ListDoctorsDto dto)
        {
            return await DoctorRepo.Read(dto);
        }
    }
}