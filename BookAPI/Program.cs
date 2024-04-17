﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookAPI.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookAPIContext") 
        ?? throw new InvalidOperationException("Connection string 'BookAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
