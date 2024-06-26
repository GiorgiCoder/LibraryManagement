﻿namespace Library.Service.Dtos.Book;

public record CreateBookDto(
    string ISBN,
    string Title,
    int Edition,
    int PageCount,
    string Description,
    int PublishYear,
    int[] SelectedGenreIds,
    Guid? SelectedPublisherId,
    Guid[] SelectedAuthorIds,
    BookLocationDto[] Locations
    );
