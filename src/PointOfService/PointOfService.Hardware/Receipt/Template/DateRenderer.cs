using System;

namespace PointOfService.Hardware.Receipt.Template
{
    [Renderer("Date")]
    public class DateRenderer : Renderer
    {
        protected override object Render() => DateTime.Now;
    }
}
