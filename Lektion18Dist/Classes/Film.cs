using Lektion18Dist.Enums;
using Lektion18Dist.Interfaces;

namespace Lektion18Dist.Classes;

public class Film : IFilm
{
    public int Id { get; init; }
    public string Title { get; private set; }
    public Genres Genre { get; set; }
    public int Year { get; set; }
    private int Month { get; set; }

    public Film(int id) => Id = id;

    public void Update(string title, Genres genre, int year, int month) =>
        (Title, Genre, Year, Month) = (title, genre, year, month);

    public void AddTitle(string title) => Title = title;

    void ChangeMonth(int month) => Month = month;

    public void AddMonth(int month) => ChangeMonth(month);

    public int GetMonth() => Month;
}
