using Lektion18Dist.Enums;
using Lektion18Dist.Interfaces;
using System;

namespace Lektion18Dist.Classes;

public class Stream : IMovie, IPlayer
{
    public int Id { get; init; }
    public string Title { get; }
    public Genres Genre { get; set; }
    public int Year { get; set; }

    public Stream(int id, string title, Genres genre, int year) =>
        (Id, Title, Genre, Year) = (id, title, genre, year);

    public string Play()
    {
        return "Streaming film";
    }

    public void Stop()
    { 
    }
}
