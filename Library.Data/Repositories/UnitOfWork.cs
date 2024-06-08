﻿using Library.Model.Interfaces;

namespace Library.Data.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IEmployeeRepository Employees { get; private set; }
    public IRoleMenuRepository RoleMenus { get; private set; }
    public IEmailRepository EmailTemplates { get; private set; }
    public IPublisherRepository Publishers { get; private set; }
    public IAuthorRepository Authors { get; private set; }
    public IBookRepository Books { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Employees = new EmployeeRepository(_context);
        RoleMenus = new RoleMenuRepository(_context);
        EmailTemplates = new EmailRepository(_context);
        Publishers = new PublisherRepository(_context);
        Authors = new AuthorRepository(_context);
        Books = new BookRepository(_context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
