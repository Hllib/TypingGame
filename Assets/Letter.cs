using System;

public struct Letter
{
    public char LetterSymbol { get; set; }
    public bool State { get; set; }

    public Letter(char symbol, bool state)
    {
        LetterSymbol = symbol;
        State = state;
    }
}

