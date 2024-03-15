using LibraryDecorator;

namespace TestDecorators
{
    [TestClass]
    public class TestsStream
    {

        [TestMethod]
        [DataRow("C:\\Users\\User\\Desktop\\учеба\\ќќѕиѕ\\лаб2\\lab2.txt")]
        public void CheckFileStreamDecorator(string filePath)
        {
            using (var _fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                var _decoratorFileStream = new FileStreamDecorator(_fileStream);
                byte[] buffer = { 118, 119, 120, 121, 122 };
                var start = _decoratorFileStream.GetTime(); 
                _decoratorFileStream.Write(buffer, 0, buffer.Length);
                var end = _decoratorFileStream.GetTime();
                Assert.AreNotEqual(start, end);
            }
        }

        [TestMethod]
        [DataRow("C:\\Users\\User\\Desktop\\учеба\\ќќѕиѕ\\лаб2\\lab2.txt")]
        public void CheckMemoryStreamDecorator(string filePath)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fileStream.CopyTo(memoryStream);
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                var _decoratorMemoryStream = new MemoryStreamDecorator(memoryStream);
                byte[] buffer = { 118, 119, 120, 121, 122 };
                var start = _decoratorMemoryStream.GetTime();
                _decoratorMemoryStream.Write(buffer, 0, buffer.Length);
                var end = _decoratorMemoryStream.GetTime();

                Assert.AreNotEqual(start, end);
            }
        }

        [TestMethod]
        [DataRow("C:\\Users\\User\\Desktop\\учеба\\ќќѕиѕ\\лаб2\\lab2.txt")]
        public void CheckBufferedStreamDecorator(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (var bufferStream = new BufferedStream(fileStream))
                {
                    var _decoratorBufferedStream = new BufferedStreamDecorator(bufferStream);
                    byte[] buffer = { 118, 119, 120, 121, 122 };
                    var start = _decoratorBufferedStream.GetTime();
                    _decoratorBufferedStream.Write(buffer, 0, buffer.Length);
                    var end = _decoratorBufferedStream.GetTime();

                    Assert.AreNotEqual(start, end);
                }
            }
        }
    }
}