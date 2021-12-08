using Microsoft.EntityFrameworkCore;
using Students.Core.Entities;

namespace Students.Infrastructure.Data
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options) { }
        public virtual DbSet<StudentEntity> Student { get; set; }

    }
}
