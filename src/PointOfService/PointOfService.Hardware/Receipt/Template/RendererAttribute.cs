using System;

namespace PointOfService.Hardware.Receipt.Template
{
    /// <summary>
    /// Marks a class as a renderer and assigns a name to it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RendererAttribute : Attribute
    {
        public string Name { get; }

        public RendererAttribute(string name)
        {
            Name = name;
        }
    }
}
