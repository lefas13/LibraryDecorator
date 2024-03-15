namespace LibraryDecorator
{
    public abstract class StreamDecorator : Stream
    {
        // Поле для хранения объекта класса Stream
        private Stream _stream;

        // Поле для хранения времени
        private TimeSpan _time;

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Конструктор, принимающий объект класса Stream
        public StreamDecorator(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        // Метод для получения времени
        public TimeSpan GetTime()
        {
            return _time;
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Записываем текущее время перед вызовом метода Read у объекта класса Stream
            DateTime start = DateTime.Now;

            // Вызываем метод Read у объекта класса Stream
            _stream.Read(buffer, offset, count);

            // Вычисляем время, потраченное на чтение из потока
            _time = DateTime.Now - start;

            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // Записываем текущее время перед вызовом метода Write у объекта класса Stream
            DateTime start = DateTime.Now;
            // Вызываем метод Write у объекта класса Stream
            _stream.Write(buffer, offset, count);

            // Вычисляем время, потраченное на запись в поток
            _time = DateTime.Now - start;
        }
    }
}