using Students.Application.Common;
using Students.Application.DTO;
using Students.Application.Interface;
using Students.Application.Mapper;
using Students.Core.Entities;
using Students.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Application.Services
{

    public class StudentsService : IStudentsService
    {
        private readonly IStudentRepository _studenRepository;

        public StudentsService(IStudentRepository studenRepository)
        {
            _studenRepository = studenRepository;
        }

        public async Task<Result<IEnumerable<StudentDTO>>> GetAllWithDateRangeAsync(DateTime? startDate, DateTime? endDate, string filter)
        {
            try
            {
                var students = await _studenRepository.GetWithRangeAsync(startDate, endDate, filter);

                var result = StudentMapper.Mapper.Map<IEnumerable<StudentEntity>, IEnumerable<StudentDTO>>(students);

                return new Result<IEnumerable<StudentDTO>> { Ok = true, Response = result };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<StudentDTO>> { Ok = false, ExceptionMessage = ex.Message };
            }
        }

        public async Task<Result<StudentDTO>> GetByIDAsync(int studentID)
        {
            try
            {
                var resultFromDb = await _studenRepository.GetByIdAsync(studentID);

                var result = StudentMapper.Mapper.Map<StudentDTO>(resultFromDb);

                return new Result<StudentDTO> { Ok = true, Response = result };
            }
            catch (Exception ex)
            {
                return new Result<StudentDTO> { Ok = false, ExceptionMessage = ex.Message };
            }
        }

        public async Task<Result<int>> AddAsync(StudentDTO studentDTO)
        {
            if (studentDTO == null)
                return new Result<int> { Ok = false, ExceptionMessage = "Value Cannot be Null" };

            var exists = await _studenRepository.ExistsAsync(studentDTO.PersonalNumber);

            if (exists)
                return new Result<int> { Ok = false, ExceptionMessage = "Personal number  duplicated" };

            try
            {
                var result = await _studenRepository.AddAsync(StudentMapper.Mapper.Map<StudentEntity>(studentDTO));

                return new Result<int> { Ok = true, Response = result.StudentID };
            }
            catch (Exception ex)
            {
                return new Result<int> { Ok = false, ExceptionMessage = ex.Message };
            }
        }

        public async Task<Result<IEnumerable<StudentDTO>>> GetAllAsync()
        {
            try
            {
                var students = await _studenRepository.GetAllAsync();

                var result = StudentMapper.Mapper.Map<IEnumerable<StudentEntity>, IEnumerable<StudentDTO>>(students);

                return new Result<IEnumerable<StudentDTO>> { Ok = true, Response = result };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<StudentDTO>> { Ok = false, ExceptionMessage = ex.Message };
            }
        }

        public async Task<Result<bool>> UpdateAsync(StudentDTO studentDTO)
        {
            if (studentDTO == null)
                return new Result<bool> { Ok = false, ExceptionMessage = "Value Cannot be Null" };
            try
            {
                await _studenRepository.UpdateAsync(StudentMapper.Mapper.Map<StudentEntity>(studentDTO));

                return new Result<bool> { Ok = true, Response = true };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Ok = false, ExceptionMessage = ex.Message };
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                await _studenRepository.DeleteAsync(id);

                return new Result<bool> { Ok = true, Response = true };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Ok = false, ExceptionMessage = ex.Message };
            }
        }
    }
}

