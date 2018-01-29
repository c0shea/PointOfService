using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfService.Hardware.Receipt.Template
{
    [Renderer(nameof(Renderer))]
    public abstract class Renderer
    {
        /// <summary>
        /// The name of this renderer.
        /// </summary>
        public string Name => ((RendererAttribute)GetType().GetCustomAttributes(typeof(RendererAttribute), false)
                                                           .FirstOrDefault())?.Name;

        public string Format { get; set; } = "{0}";
        
        public abstract void Append(StringBuilder sb);
    }
}
