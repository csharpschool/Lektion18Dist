using Lektion18Dist.Enums;

namespace Lektion18Dist.Interfaces;

public interface IFilm
{
    public int Id { get; init; }
    public string Title { get; }
    public Genres Genre { get; set; }
    public int Year { get; set; }

    void Update(string title, Genres genre, int year, int month);
}
