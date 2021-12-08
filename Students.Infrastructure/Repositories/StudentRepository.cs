using Microsoft.EntityFrameworkCore;
using Students.Core.Entities;
using Students.Core.Repositories;
using Students.Infrastructure.Data;
using Students.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Infrastructure.Repositories
{
    public class StudentRepository : Repository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(StudentsContext studentsContext) : base(studentsContext)
        {
        }

        public async Task<IEnumerable<StudentEntity>> GetWithRangeAsync(DateTime? startDate, DateTime? endDate, string filter = "")
        {
            if (!string.IsNullOrEmpty(filter) && !startDate.HasValue && !endDate.HasValue)
                return await Where(x => x.PersonalNumber.Contains(filter)).ToListAsync();

            if(string.IsNullOrEmpty(filter) && startDate.HasValue && endDate.HasValue)
                return await Where(x => (x.DateOfBirth < endDate && x.DateOfBirth > startDate)).ToListAsync();

            if (!string.IsNullOrEmpty(filter) && startDate.HasValue && endDate.HasValue)
                return await Where(x => (x.DateOfBirth < endDate && x.DateOfBirth > startDate) && x.PersonalNumber.Contains(filter)).ToListAsync();

            return await GetAllAsync();
        }

        public async Task<bool> ExistsAsync(string personalNumber)
        {
            return await Where(x => x.PersonalNumber.Equals(personalNumber)).AnyAsync();
        }
    }
}
