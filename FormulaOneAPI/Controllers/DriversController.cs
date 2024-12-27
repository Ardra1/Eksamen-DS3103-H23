
namespace FORMULAONEAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FORMULAONEAPI.Contexts;
using FORMULAONEAPI.Models;

[ApiController]
[Route("api/[controller]")]

public class DriversController : ControllerBase
{
    private readonly FormulaOneContext formulaOneContext;
    public DriversController(FormulaOneContext _formulaOneContext)
    {
        formulaOneContext = _formulaOneContext;
    }

    //HENTER ALLE DRIVERS - viser alle på nettsiden. 
    [HttpGet]
    public async Task<ActionResult<List<Drivers>>> GetAllDrivers()
    {
        try
        {
            List<Drivers> drivers = await formulaOneContext.Drivers.ToListAsync();
            return Ok(drivers);
        }
        catch 
        {
            return StatusCode(500);
        }
    }

    // Henter en sjåfør basert på iden som skrives inn. 
     [HttpGet("{id}")]
    public async Task<ActionResult<Drivers>> GetById(int id)
    {
        //Sjekker om koden fungere - Feiler den Returnerer status 500 - internal server error - Det skjedde noe feil ved HttpKall, hos meg (koden)).
        try
        {
            //Sjekker om det er innhold i DB Driver. Hvis ja, så sjekker den innskrevet Id opp mot idene i DB. 
            Drivers? drivers = await formulaOneContext.Drivers.FindAsync(id);
            //Hvis iden finnes, sp returnerer den objektet med valgt Id. 
            if( drivers != null )
            {
                return Ok(drivers);
            }
            //Hvis ikke id stemmer, får vi beskjed om at den ikke finnes. 
            else
            {
                return NotFound("Id finnes ikke, prøv en annen id");
            }            
        }
        catch
        {
            return StatusCode(500);
        }
    }

    //HENTER DRIVER BASERT PÅ NAVN
    [HttpGet]
    [Route("[action]/{name}")]

    public async Task<ActionResult<Drivers>> GetDriverByName(string name)
    {
        try
        {
            Drivers? driver = await formulaOneContext.Drivers.FirstOrDefaultAsync(_driver => _driver.Name == name);
            if(driver != null )
            {
                return Ok(driver);
            }
            else
            {
                return NotFound();
            }   
        }
        catch
        {
            return StatusCode(500);
        }
    }

    //LEGGE TIL NY DRIVER I DB
    [HttpPost]
    //Basert på Model for sjåføren (driver), så lager vi en ny driver. 
    public async Task<ActionResult<Drivers>> PostAddNewDriver(Drivers newDriver)
    {
        try{
        //Her adder den det nye driver-objektet til databasen og lagrer?. 
        formulaOneContext.Drivers.Add(newDriver); //gjør klar for å lagre
        await formulaOneContext.SaveChangesAsync(); // lagrer til databasen

        //Kan også bare returnere NoContent() - Nå returnerer denne det som er oppretta. 
        //CreatedAtAction("Get", new{id = newDriver.Id}, newDriver);
            return Ok();
        }
        catch {
            return NotFound("Feil ved forsøk på å legge til ny sjåfør (HttpPost - DriversController)");
        }
    }

    //GJØRE ENDRINGER PÅ EN EKSISTERENDE DRIVER
    [HttpPut]
    public async Task<IActionResult> PutUpdatedDriver(Drivers updatedDriver)
    {
        try
        {
            formulaOneContext.Entry(updatedDriver).State = EntityState.Modified;
            await formulaOneContext.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    //SLETTE EN DRIVER FRA DB BASERT PÅ ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriverById(int id)
    {
        try
        {
            Drivers? driver = await formulaOneContext.Drivers.FindAsync(id);
            //Hvis id stemmer med en id i DB, så skal den slette objektet med gitt Id. 
            if(driver != null){
                formulaOneContext.Drivers.Remove(driver);
                await formulaOneContext.SaveChangesAsync();
            
            //NoContent returnerer "204" - At alt er Ok - ellers vil vi få feil, siden Delete vil ha en verdi i retur. 
            return NoContent();
        }
        else
        {
            return NotFound();
        }
        }
        catch
        {
            return StatusCode(500);
        }
    }
}


    

