using NPOI.HPSF;
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
        public string MemoSrc { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public BaseMemoReader(string memoSrc)
        {
            this.MemoSrc = memoSrc;
        }
        public virtual string ReadDocumentText()
        {
            return string.Empty;
        }
    }
    public class WordMemoReader : BaseMemoReader
    {
        public WordMemoReader(string memoSrc) : base(memoSrc)
        {
        }
        public override string ReadDocumentText()
        {
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



            using (StreamReader streamReader = new StreamReader(this.MemoSrc))
            {
                var document = new XWPFDocument(streamReader.BaseStream);
                var wordExtractor = new XWPFWordExtractor(document);
                var text = wordExtractor.Document;
                var items = document.BodyElements;
                foreach (var item in items)
                {
                    if (item is XWPFParagraph p)
                    {
                        Debug.WriteLine(p.Style + "  " + p.Text);

                    }
                }
            }
            //FileStream fs = new FileStream(this.MemoSrc, FileMode.Open);
            //XWPFDocument doc = new XWPFDocument(fs);
            //IList<XWPFParagraph> paragraphs = doc.Paragraphs;
            //foreach (XWPFParagraph paragraph in paragraphs)
            //{
            //    File.AppendAllText("⽂本1.txt", paragraph.Text);
            //}
            return "";
        }
    }
}
