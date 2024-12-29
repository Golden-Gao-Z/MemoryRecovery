using MR.Service;

namespace MR.ServiceTests
{
    public class MemoReaderTest
    {
        [Fact]
        public void ReadDocumentTextTest()
        {

            var path = Path.Combine(AppContext.BaseDirectory, "TestResources", "midwaySystemDetails.docx");
            //var path = Path.Combine(AppContext.BaseDirectory,"TestResources", "midwayTransactionProtocol.doc");
            var wordReader = new WordMemoReader(path);
            wordReader.ReadDocumentText();

        }
        [Fact]
        public void ReadAllSingleMemosTest()
        {

            var path = Path.Combine(AppContext.BaseDirectory, "TestResources", "midwaySystemDetails.docx");
            //var path = Path.Combine(AppContext.BaseDirectory,"TestResources", "midwayTransactionProtocol.doc");
            var wordReader = new WordMemoReader(path);
            wordReader.ReadAllSingleMemos();

        }
        [Fact]
        public void RandomTest()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "TestResources", "midwaySystemDetails.docx");
            //var path = Path.Combine(AppContext.BaseDirectory,"TestResources", "midwayTransactionProtocol.doc");
            var wordReader = new WordMemoReader(path);
            MemoManager manager = new MemoManager(wordReader);
            var r = manager.Random();

        }
    }
}