using Microsoft.EntityFrameworkCore;
using NotebookApi.Models;

namespace NotebookApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<NoteModel> Notes { get; set; }
    public DbSet<NoteItemModel> NoteItems { get; set; }
}