namespace LibraryDecorator
{
    public class BufferedStreamDecorator : StreamDecorator
    {
        public BufferedStreamDecorator(Stream stream) : base(stream) { }
    }
}
