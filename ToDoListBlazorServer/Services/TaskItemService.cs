using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListBlazorServer.Data;
using ToDoListBlazorServer.Models;

namespace ToDoListBlazorServer.Services
{
    public class TaskItemService
    {

        private readonly AppDbContext _context;

        public TaskItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync(int userId)
        {
            return await _context
                .TaskItems
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();


        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context
                .TaskItems
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task AddAsync(TaskItem model, int userId)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.IsCompleted = false;

            model.UserId = userId;

            await _context.TaskItems.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TaskItem model)
        {
            
            _context.TaskItems.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem model)
        {

            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == model.Id);

             _context.Remove(taskItem);
            await _context.SaveChangesAsync();

        }

        public async Task ToggleCompletionAsync(int id)
        {
            var taksItem = await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
            if (taksItem != null)
            {
                taksItem.IsCompleted = !taksItem.IsCompleted;
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
