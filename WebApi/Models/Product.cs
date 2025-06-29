﻿namespace WebApi.Models;

public class Product
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
