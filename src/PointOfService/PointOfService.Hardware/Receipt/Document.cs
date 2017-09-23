using System.Collections.Generic;

namespace PointOfService.Hardware.Receipt
{
    public class Document
    {
        public List<ICommand> Commands { get; set; }
    }
}
