using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListBlazorServer.Data;
using ToDoListBlazorServer.Models;

namespace ToDoListBlazorServer.Services
{
    public class TaskService
    {

        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context
                .TaskItems
                .AsNoTracking()
                .ToListAsync();


        }

        public async Task AddAsync(TaskItem model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.IsCompleted = false;

            model.UserId = 1;

            await _context.TaskItems.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TaskItem model)
        {
            _context.TaskItems.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var task = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);

            _context.Remove(task);
            await _context.SaveChangesAsync();

        }

       
    }
}
