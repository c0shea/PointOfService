using System.Text;

namespace PointOfService.Hardware.Receipt.Template
{
    [Renderer("Literal")]
    public class LiteralRenderer : Renderer
    {
        public string Text { get; set; }

        public LiteralRenderer()
        {

        }

        public LiteralRenderer(string text)
        {
            Text = text;
        }

        public override void Append(StringBuilder sb)
        {
            sb.Append(Text);
        }
    }
}
