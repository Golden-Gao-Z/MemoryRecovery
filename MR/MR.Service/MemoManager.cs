using MR.Model;
using NPOI.HPSF;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.Service
{

    public class MemoManager
    {
        public BaseMemoReader MemoReader { get; private set; }
        public MemoManager(BaseMemoReader memoReader, MemoScheduler scheduler = null)
        {
            this.MemoReader = memoReader;
            this.memos = this.MemoReader.ReadAllSingleMemos();
        }
        private List<SingleMemo> memos { get; set; } = new List<SingleMemo>();
        int rLimit = 100;
        private int preRand = -1;
        public virtual SingleMemo Random()
        {
            var count = this.memos.Count;
            if (count == 0) return null;

            long tick = DateTime.Now.Ticks;
            Random rand = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            var num = rand.Next(0, count);
            var mm = this.memos[num];
            var rcount = 0;
            // 抽到空内容，或者与上次重复的，重抽
            while ((string.IsNullOrEmpty(mm.Text) || preRand == num)
                && rcount++ < this.rLimit)
            {
                num = rand.Next(0, count);
                mm = this.memos[num];
            }
            preRand = num;
            return mm;
        }
        private int levelLimit = 5;
        public virtual List<string> GetOverView()
        {

            var lis = this.memos.Where(tt => tt.Level > 0 && tt.Level < levelLimit).ToList();
            var res = lis.Select(tt =>
            {
                var pattern = "{0," + (tt.Level - 1) * 7 + "}{1}";
                var ff = string.Format(pattern, "", tt.Title.Trim());
                Debug.WriteLine(ff);
                return ff;
            }

            );
            return res.ToList();
        }
    }

    /// <summary>
    /// get memo to use.
    /// </summary>
    public class MemoScheduler
    {

    }
}
