using Students.Application.Common;
using Students.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Application.Interface
{
    public interface IStudentsService
    {
        Task<Result<IEnumerable<StudentDTO>>> GetAllAsync();
        Task<Result<StudentDTO>> GetByIDAsync(int studentID);
        Task<Result<IEnumerable<StudentDTO>>> GetAllWithDateRangeAsync(DateTime? startDate, DateTime? endDate, string filter);
        Task<Result<int>> AddAsync(StudentDTO studentDTO);
        Task<Result<bool>> UpdateAsync(StudentDTO studentDTO);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
