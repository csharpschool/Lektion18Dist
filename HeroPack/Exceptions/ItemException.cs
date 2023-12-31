﻿using HeroPack.Interfaces;

namespace HeroPack.Exceptions;

public class ItemException : Exception
{
    public IItem? Item { get; init; }

    public ItemException(string message, IItem? item = null) : base(message)
    {
        Item = item;
    }
}
