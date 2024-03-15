namespace LibraryDecorator
{
    public abstract class StreamDecorator : Stream
    {
        /// <summary>
        /// Поле для хранения объекта класса Stream
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// Поле для хранения времени
        /// </summary>
        private TimeSpan _time;

        private DateTime _start = DateTime.Now;

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Конструктор, принимающий объект класса Stream
        /// </summary>
        /// <param name="stream">Поток передаваемый в конструктор</param>
        /// <exception cref="ArgumentNullException"></exception>
        public StreamDecorator(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Метод для получения времени
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTime()
        {
            return _time;
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Метод чтения потока с подсчётом времени неиспользования
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // Записываем текущее время перед вызовом метода Read у объекта класса Stream
            DateTime end = DateTime.Now;

            // Вызываем метод Read у объекта класса Stream
            _stream.Read(buffer, offset, count);

            // Вычисляем время, потраченное на чтение из потока
            _time = end - _start;
            _start = end;

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
            DateTime end = DateTime.Now;
            // Вызываем метод Write у объекта класса Stream
            _stream.Write(buffer, offset, count);

            // Вычисляем время, потраченное на запись в поток
            _time = end - _start;
            _start = end;
        }
    }
}