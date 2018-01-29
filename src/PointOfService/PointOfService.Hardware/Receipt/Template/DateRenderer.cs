using System;
using System.Text;

namespace PointOfService.Hardware.Receipt.Template
{
    [Renderer("Date")]
    public class DateRenderer : Renderer
    {
        public  override void Append(StringBuilder sb)
        {
            sb.AppendFormat("{0:MM/dd/yyyy}", DateTime.Now);
        }
    }
}
