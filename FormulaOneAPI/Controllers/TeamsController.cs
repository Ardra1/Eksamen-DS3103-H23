
namespace FORMULAONEAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FORMULAONEAPI.Contexts;
using FORMULAONEAPI.Models;

[ApiController]
[Route("api/[controller]")]

public class TeamsController : ControllerBase
{
    private readonly FormulaOneContext formulaOneContext;
    public TeamsController(FormulaOneContext _formulaOneContext)
    {
        formulaOneContext = _formulaOneContext;
    }

    //HENTER ALLE TEAMS FRA DB - Lister ut alle team fra DB.
    [HttpGet]
    public async Task<ActionResult<List<Teams>>> Get()
    {
    try
    {
        List<Teams> teams = await formulaOneContext.Teams.ToListAsync();
        return Ok(teams);
    }
    catch {
        return StatusCode(500);
    }
    }

    //HENTER TEAM BASERT PÅ ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Teams>> GetById(int id)
    {
        try
        {
            Teams? teams = await formulaOneContext.Teams.FindAsync(id);
            if( teams != null )
            {
                return Ok(teams);
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

    //HENTER TEAM BASERT PÅ MANUFACTURER
    [HttpGet]
    [Route("[action]/{manufacturer}")]
    public async Task<ActionResult<Teams>> GetTeamByManufacturer(string manufacturer)
    {
        try
        {
            Teams? team = await formulaOneContext.Teams.FirstOrDefaultAsync(_team => _team.Manufacturer == manufacturer);
            if(team != null )
            {
                return Ok(team);
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

    //LEGGE TIL NYTT TEAM I DB
    [HttpPost]
    public async Task<ActionResult<Teams>> PostAddNewTeam(Teams newTeam)
    {
        try{
        //Her adder den det nye Team-objektet til databasen og lagrer. 
        formulaOneContext.Teams.Add(newTeam); //gjør klar for å lagre
        await formulaOneContext.SaveChangesAsync(); // lagrer til databasen

        //Kan også bare returnere NoContent() - Nå returnerer denne det som er oppretta. 
        //CreatedAtAction("Get", new{id = newDriver.Id}, newDriver);
        return Ok();
        }
        catch {
            return NotFound("Feil ved forsøk på å legge til nytt team (HttpPost - TeamController)");
        }
    }

    //GJØRE ENDRINGER PÅ EN EKSISTERENDE TEAM
    [HttpPut]
    [Route("[action]/{manufacturer}")]
    public async Task<IActionResult> PutUpdateTeam(Teams updateTeam)
    {
        try
        {
            formulaOneContext.Entry(updateTeam).State = EntityState.Modified;
            await formulaOneContext.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return StatusCode(500);
        }
    }

     //SLETTE TEAM FRA DB BASERT PÅ MANUFACTURER
    [HttpDelete("[action]/{manufacturer}")]
    public async Task<IActionResult> DeleteTeamByManufacturer(string manufacturer)
    {
        try
        {
            Teams? team = await formulaOneContext.Teams.FirstOrDefaultAsync(_team => _team.Manufacturer == manufacturer);
            //Hvis id stemmer med en id i DB, så skal den slette objektet med gitt Id. 
            if(team != null){
                formulaOneContext.Teams.Remove(team);
                await formulaOneContext.SaveChangesAsync();
            
            //NoContent returnerer "204" - At alt er Ok - ellers vil vi få feil, siden Delete vil ha en verdi i retur. 
            return StatusCode(200, "Kode funker delete controller.");
        }
        else
        {
            return NotFound("Not found fra controller!!!");
        }
        }
        catch
        {
            return StatusCode(500, "Feilkoden kommer fra TeamsController");
        }
    }


}