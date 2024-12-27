
using FORMULAONEAPI.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Setter navn på database og hvor filen skal lagres. 
builder.Services.AddDbContext<FormulaOneContext>(
    options => options.UseSqlite("Data Source=Databases/FormulaOne.db"));

//CORS - Åpner API så det kan benyttes i frontend av andre domener:
builder.Services.AddCors(
    builder => {
        builder.AddPolicy("AllowAll", 
            policies => policies
                //Post-put-get-delete
                .AllowAnyMethod()
                //La hvilket som helst domene bruke det - eks. vg.no, facebook etc.
                .AllowAnyOrigin()
                //tilltater "preflight" for å sjekke at metoder kan kjøres?
                .AllowAnyHeader()
        );
    }
);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseStaticFiles();

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
