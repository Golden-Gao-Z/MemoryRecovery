using MR.Service;
using MR.Service.ExtensionUtilities;
using Xunit.Abstractions;

namespace MR.ServiceTests
{
    public class MemoReaderTest
    {
        private ITestOutputHelper outputHelper;
        public MemoReaderTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }
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
            var _=new List<Type>();
            wordReader.ReadAllSingleMemos(out _);

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
        [Fact]
        public void RandomTest2()
        {
            var list = new List<int>();
            var number = 1000;
            var factor = 0.01d;
            for (int i = 0; i < number; i++)
            {
                var rand = RandomExtension.GetRandom();
                list.Add(rand.Next(0, number));
            }
            list.Sort();
            var dis = list.Distinct().ToList();
            var disCount=dis.Count();
            this.outputHelper.WriteLine($"Total: {list.Count}, Distinct Total: {disCount}, Expected: {number * factor}");
            this.outputHelper.WriteLine(string.Join(", ", dis));
            Assert.True(disCount >= number * factor);
        }
    }
}