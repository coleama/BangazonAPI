using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// User Endpoints
app.MapGet("api/users", (BangazonDbContext db) =>
{
    return db.users.ToList();
});
// get by id 
app.MapGet("api/users/{id}", (BangazonDbContext db, string id) =>
{
    return db.users.Include(u => u.Id).ToList();
});
// create a user
app.MapPost("api/users", (BangazonDbContext db, User user) =>
{
    db.users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/users/{user.Id}", user);
});
// update user
app.MapPut("api/users/{id}", (BangazonDbContext db, string id, User user) =>
{
    User userToUpdate = db.users.SingleOrDefault(u => u.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }
    userToUpdate.FirstName = user.FirstName;
    userToUpdate.LastName = user.LastName;
    userToUpdate.Email = user.Email;
    userToUpdate.Address = user.Address;
    db.SaveChanges();
    return Results.NoContent();
});
// delete a user 
 app.MapDelete("api/users/{id}", (BangazonDbContext db, string id) =>
{
    User userToDelete = db.users.SingleOrDefault(u => u.Id == id);
    if (userToDelete == null)
    {
        return Results.NotFound();
    }
    db.users.Remove(userToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

// Product Endpoints 
// get products
app.MapGet("api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});
//get product by id
app.MapGet("api/products/{id}", (BangazonDbContext db, int id) =>
{
    return db.Products.Include(p => p.Id).ToList();
});
// create a product 
app.MapPost("api/products", (BangazonDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/api/products/{product.Id}", product);
});
//update product 
/* app.MapPut("api/products/{id}", (BangazonDbContext db, int id, Product updatedProduct) =>
{
    if (id != updatedProduct.Id)
    {
        return Results.BadRequest();
    }
    db.Entry(updatedProduct).State = EntityState.Modified;
    try
    {
        db.SaveChanges();
        return Results.NoContent();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!db.Products.Any(p => p.Id == id))
        {
            return Results.BadRequest();
        }
    }
}); */
// Order Endpoints


app.Run();

