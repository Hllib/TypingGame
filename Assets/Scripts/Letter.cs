public struct Letter
{
    public char Character { get; set; }
    public bool WasPrinted { get; set; }

    public Letter(char symbol, bool state)
    {
        Character = symbol;
        WasPrinted = state;
    }
}

