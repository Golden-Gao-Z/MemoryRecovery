using MR.Service;

namespace MR.ServiceTests
{
    public class MemoReaderTest
    {
        [Fact]
        public void Test1()
        {

            var path = Path.Combine(AppContext.BaseDirectory, "TestResources", "midwayTransactionProtocol.docx");
            //var path = Path.Combine(AppContext.BaseDirectory,"TestResources", "midwayTransactionProtocol.doc");
            var wordReader = new WordMemoReader(path);
            wordReader.ReadDocumentText();

        }
    }
}