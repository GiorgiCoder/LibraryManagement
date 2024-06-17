﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Books;

public class CreateBookViewModel
{
    [MaxLength(13)]
    public string ISBN { get; set; }
    public string Title { get; set; }
    public int Edition { get; set; }
    public int PageCount { get; set; }
    public string Description { get; set; }
    public int PublishYear { get; set; }


    [Display(Name = "Publisher")]
    public Guid SelectedPublisherId { get; set; }
    public SelectList? Publishers { get; set; }

    [Display(Name = "Authors")]
    public List<Guid> SelectedAuthorIds { get; set; } = [];
    public SelectList? Authors { get; set; }

    public string? EncodedLocationsString { get; set; }
}