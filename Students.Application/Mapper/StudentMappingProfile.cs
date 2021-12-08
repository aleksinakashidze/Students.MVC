using AutoMapper;
using Students.Application.DTO;

using Students.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Application.Mapper
{
   public class StudentMappingProfile:Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Students.Core.Entities.StudentEntity, Students.Application.DTO.StudentDTO>().ReverseMap();
        }
    }
}
