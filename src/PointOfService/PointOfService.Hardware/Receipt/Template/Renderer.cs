using System.Linq;
using System.Text;

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

        public string Format { get; set; }

        public virtual void Append(StringBuilder sb) => sb.AppendFormat("{0:" + Format + "}", Render());

        protected abstract object Render();
    }
}
