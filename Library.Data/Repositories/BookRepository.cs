﻿namespace Library.Data.Repositories;

using Library.Model.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class BookRepository : BaseModelRepository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async override Task<Book?> GetById(Guid id, bool trackChanges)
    {
        return trackChanges ?
            await _context.Books.FirstOrDefaultAsync(x => x.Id == id) :
            await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksOfAuthor(Guid authorId, bool trackChanges)
    {
        var query = _context.BookAuthor
                        .Include(x => x.Book)
                        .Where(x => x.AuthorId == authorId)
                        .Select(x => x.Book);

        return trackChanges ?
            await query.ToListAsync() :
            await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetAllBooksOfPublisher(Guid publisherId, bool trackChanges)
    {
        var query = _context.Books
                        .Where(x => x.PublisherId == publisherId);

        return trackChanges ?
            await query.ToListAsync() :
            await query.AsNoTracking().ToListAsync();
    }

    public async Task UpdateGenresForBook(Guid bookId, List<int> newGenreIds)
    {
        var existingGenres = await _context.BookGenre
                                .Where(bg => bg.BookId == bookId)
                                .ToListAsync();

        var genresToRemove = existingGenres
                                .Where(bg => !newGenreIds.Contains(bg.GenreId))
                                .ToList();

        var genresToAdd = newGenreIds
                                .Except(existingGenres.Select(bg => bg.GenreId))
                                .Select(genreId => new BookGenre { BookId = bookId, GenreId = genreId })
                                .ToList();

        _context.BookGenre.RemoveRange(genresToRemove);
        await _context.BookGenre.AddRangeAsync(genresToAdd);
    }

    public async Task UpdateAuthorsForBook(Guid bookId, List<Guid> newAuthorIds)
    {
        var existingAuthors = await _context.BookAuthor
                                .Where(ba => ba.BookId == bookId)
                                .ToListAsync();

        var authorsToRemove = existingAuthors
                                .Where(ba => !newAuthorIds.Contains(ba.AuthorId))
                                .ToList();

        var authorsToAdd = newAuthorIds
                                .Except(existingAuthors.Select(ba => ba.AuthorId))
                                .Select(authorId => new BookAuthor { BookId = bookId, AuthorId = authorId })
                                .ToList();

        _context.BookAuthor.RemoveRange(authorsToRemove);
        await _context.BookAuthor.AddRangeAsync(authorsToAdd);
    }
}
