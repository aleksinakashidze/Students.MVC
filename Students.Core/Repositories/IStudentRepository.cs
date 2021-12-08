using Students.Core.Entities;
using Students.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Core.Repositories
{
    public interface IStudentRepository:IRepository<StudentEntity>
    {
        Task<IEnumerable<StudentEntity>> GetWithRangeAsync(DateTime? startDate,DateTime? endDate,string filter);
        Task<bool> ExistsAsync(string personalNumber);
    }
}
