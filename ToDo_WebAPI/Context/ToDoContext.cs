using Microsoft.EntityFrameworkCore;
using ToDo_WebAPI.Entities;

namespace ToDo_WebAPI.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<ToDoEntity> ToDos => Set<ToDoEntity>();
    }
}
