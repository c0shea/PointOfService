namespace PointOfService.Hardware.Receipt.Template
{
    public class StringEnumerator
    {
        private readonly string _text;

        internal int Position { get; private set; }
        
        public StringEnumerator(string text)
        {
            _text = text;
            Position = 0;
        }

        public int Peek()
        {
            if (Position < _text.Length)
            {
                return _text[Position];
            }

            return -1;
        }

        public int Pop()
        {
            if (Position < _text.Length)
            {
                return _text[Position++];
            }

            return -1;
        }

        public string Substring(int startIndex, int endIndex)
        {
            return _text.Substring(startIndex, endIndex - startIndex);
        }
    }
}
