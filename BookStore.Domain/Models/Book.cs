﻿namespace BookStore.Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? Year { get; set; }
    public string? Isbn { get; set; }
    public string? Summary { get; set; }
    public string? Image { get; set; }
    public decimal? Price { get; set; }
    public int? AuthorId { get; set; }
    public ICollection<Genre>? Genres { get; set; }
    public ICollection<Publisher>? Publishers { get; set; }
    public virtual Author? Author { get; set; }
}