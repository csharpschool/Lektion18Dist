using Lektion18Dist.Enums;
using Lektion18Dist.Interfaces;

namespace Lektion18Dist.Classes;

public class DVD : IMovie
{
    public int Id { get; init; }
    public string Title { get; }
    public Genres Genre { get; set; }
    public int Year { get; set; }

    public DVD(int id, string title, Genres genre, int year) =>
        (Id, Title, Genre, Year) = (id, title, genre, year);

    public string Play()
    {
        return "Playing the DVD";
    }

}
