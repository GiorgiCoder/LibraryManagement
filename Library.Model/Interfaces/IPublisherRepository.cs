﻿using Library.Model.Models;

namespace Library.Model.Interfaces;

public interface IPublisherRepository : IGenericRepository<Publisher>
{
    Task<Publisher?> GetByEmail(string email);
    Task<Publisher?> GetByName(string name);
    Task<Publisher?> PublisherExists(string email, string name);
}