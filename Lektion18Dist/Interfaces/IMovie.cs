﻿using Lektion18Dist.Enums;

namespace Lektion18Dist.Interfaces
{
    public interface IMovie// : IPlayer
    {
        public int Id { get; init; }
        public string Title { get; }
        public Genres Genre { get; set; }
        public int Year { get; set; }
        string Play();
    }

    public interface IPlayer
    {
        public void Stop();
    }
}
