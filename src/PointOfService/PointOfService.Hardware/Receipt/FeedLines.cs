﻿using Microsoft.PointOfService;

namespace PointOfService.Hardware.Receipt
{
    public class FeedLines : ICommand
    {
        public string Name => nameof(FeedLines);
        public short? Lines { get; set; }

        public void Execute(PosPrinter printer, PrinterStation station)
        {
            printer.PrintNormal(station, EscapeSequence.FeedLines(Lines));
        }
    }
}
