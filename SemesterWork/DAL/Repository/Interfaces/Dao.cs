using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using SemesterWork.DAL.Dto;
using SemesterWork.DAL.Entities;

namespace SemesterWork.DAL.Repository.Interfaces
{
    public interface DaoBase<T>
    {
        public T FindById(int id);
        public Task<Pageable<T>> Read(PageableArgs args);
        public Task<T> Create(T data);
        public Task<T> Update(T sourceObject, T updatedFields);
        public Task Delete(T sourceObject);
        protected T MapToEntity(NpgsqlDataReader reader);
    }
}
