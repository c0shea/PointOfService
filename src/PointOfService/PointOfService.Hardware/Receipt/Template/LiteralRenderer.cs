namespace PointOfService.Hardware.Receipt.Template
{
    [Renderer("Literal")]
    public class LiteralRenderer : Renderer
    {
        public string Text { get; set; }

        public LiteralRenderer(string text)
        {
            Text = text;
        }

        protected override object Render() => Text;
    }
}
