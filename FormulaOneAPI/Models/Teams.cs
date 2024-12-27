namespace FORMULAONEAPI.Models;

using FORMULAONEAPI.Interfaces;

public class Teams : ITeam
{
public int Id {get; set;}
public string? Manufacturer {get; set;}
public string? Image {get; set;}
public string? DriverOne {get; set;}
public string? DriverTwo {get; set;}
}