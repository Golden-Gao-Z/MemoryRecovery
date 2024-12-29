using MR.Model;
using NPOI.HPSF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.XWPF.Extractor;
using NPOI.XWPF.UserModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;

namespace MR.Service
{
    public class BaseMemoReader
    {
        protected MRDocument mrdocumentCache;
        public string MemoSrc { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public BaseMemoReader(string memoSrc)
        {
            this.MemoSrc = memoSrc;
        }
        public virtual MRDocument? ReadDocumentText()
        {
            return default;
        }
        public virtual List<SingleMemo> ReadAllSingleMemos()
        {
            return default;
        }
    }
    public class WordMemoReader : BaseMemoReader
    {
        public WordMemoReader(string memoSrc) : base(memoSrc)
        {
        }

        public override MRDocument ReadDocumentText()
        {
            if (false)// if file or source was not modified, return cache object, base on md5 check.
            {
                return this.mrdocumentCache;
            }
            // 可据此抠出memo片段。前提按要求的格式良好的word文档。
            //            a7 计算机组成原理
            //1  计算机系统体系结构
            //  计算机组成原理与系统结构是计算机科学与技术及相关专业的核心基础内容，其教学效果对于培养学生的计算机系统能力具有很大影响。

            //2  什么是计算机系统体系结构
            //  本章的第1个概念就是计算机系统（computer system）。媒体一致以来将一个微处理器或者甚至是一块芯片称作计算机系统。
            //  实际上，计算机系统包括读取并执行程序的中央处理器单元，保存程序和数据的存储器，以及将芯片转换为实用系统的其他子系统。
            //3  什么是计算机
            //  定义术语“计算机”时必须指明计算机的类型。
            //4  背景
            //  计算机从存储器中读出指令并执行这些指令。
            //2  正文部分
            //3  正文部分
            //  正文部分
            //3  正文部分
            //  翻跟斗广泛
            var mrd = new MRDocument();


            using StreamReader streamReader = new(this.MemoSrc);
            var document = new XWPFDocument(streamReader.BaseStream);
            var items = document.BodyElements.Cast<XWPFParagraph>();
#if DEBUG
            var styles = items.Select(tt => tt?.Style + ": " + tt?.StyleID).Distinct();
            var sss = string.Join("\n", styles);
            Debug.WriteLine(sss);
#endif
            // 1st: load a level tree 
            // 2nd: asign not to mrd
            ///  1st: create nodes
            ///  2nd: make nodes 

            var levelCache = items.Select((tt, index) => new { tt.StyleID, tt.Text, index });

            foreach (var item in items)
            {
#if DEBUG
                Debug.WriteLine(item.Style + "  " + item.Text);
#endif

            }
            this.mrdocumentCache = mrd;
            return mrd;
        }

        public override List<SingleMemo> ReadAllSingleMemos()
        {


            using StreamReader streamReader = new(this.MemoSrc);
            var document = new XWPFDocument(streamReader.BaseStream);
            var items = document.BodyElements.Cast<XWPFParagraph>();
#if DEBUG
            var styles = items.Select(tt => tt?.Style + ": " + tt?.StyleID).Distinct();
            var sss = string.Join("\n", styles);
            Debug.WriteLine(sss);
#endif

            //Heading2: Heading2
            var prefix = "heading";
            var levelCache = items.Select((tt, index) => new { tt.StyleID, tt.Text, index });
            List<SingleMemo> res = new();
            SingleMemo r = null;
            foreach (var item in levelCache)
            {
                int level = -1;
                if ((int.TryParse(item.StyleID, out level))
                    || (item.StyleID != null && item.StyleID.ToLower().StartsWith(prefix) && int.TryParse(item.StyleID.Substring(prefix.Length, item.StyleID.Length - prefix.Length), out level)))
                {
                    var cc = r;
                    if (cc != null)
                    {
                        cc.Text = cc.Text?.Trim();
                        res.Add(cc);
                    }

                    r = new SingleMemo();
                    r.Title += item.Text;
                    r.Level = level;
                }
                else
                {
                    if (r != null)
                        r.Text += "\n" + item.Text;
                }
            }
            return res;
        }
    }
}
