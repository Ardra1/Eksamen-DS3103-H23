namespace FORMULAONEAPI.Models;

using FORMULAONEAPI.Interfaces;

public class Drivers : IDriver
{
public int Id {get; set;}
public string? Name {get; set;}
public int Age {get; set;}
public string? Nationality {get; set;}
public string? Image {get; set;}
};