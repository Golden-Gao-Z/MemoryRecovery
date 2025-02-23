using MR.Model;
using MR.Service.ExtensionUtilities;
using NPOI.HPSF;
using NPOI.OpenXmlFormats.Wordprocessing;
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
        private readonly int randLimit = 100;

        private List<SingleMemo> Memos { get; set; } = [];
        public int MemosCount => this.Memos.Count;
        public SingleMemo GetOne(int index)
        {
            return this.Memos[index];
        }
        public BaseMemoReader MemoReader { get; }
        public MemoScheduler? Scheduler { get; }

        public MemoManager(BaseMemoReader memoReader, MemoScheduler? scheduler = null)
        {
            this.MemoReader = memoReader;
            this.Scheduler = scheduler;
            var _ = new List<Type>();
            this.Memos = this.MemoReader.ReadAllSingleMemos(out _);
        }
        private int preRand = -1;
        public virtual SingleMemo? Random(int levelLow = 0, int levelHigh = 6)
        {
            var count = this.Memos.Count;
            if (count == 0) return null;

            var rand = RandomExtension.GetRandom();

            var num = rand.Next(0, count);
            var mm = this.Memos[num];
            var rcount = 0;
            // 抽到空内容，或者与上次重复的，重抽
            while (
                (string.IsNullOrEmpty(mm.Title) || preRand == num || mm.Level > levelHigh || mm.Level < levelLow)
                && rcount++ < randLimit)
            {
                num = rand.Next(0, count);
                mm = this.Memos[num];
            }
            preRand = num;
            return mm;
        }
        private int levelLimit = 5;
        private int spaceControl = 3;
        public virtual IEnumerable<string> GetOverView()
        {

            var lis = this.Memos.Where(tt => tt.Level > 0 && tt.Level < levelLimit).ToList();
            var res = lis.Select(tt =>
            {
                var pattern = "{0," + (tt.Level - 1) * spaceControl + "}{1}";
                var ff = string.Format(pattern, "", tt.Title.Trim());
                //Debug.WriteLine(ff);
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


    public class SuperMemoManager
    {
        private List<MemoManager> managers = new List<MemoManager>();
        private List<int> counts = new List<int>();
        public SuperMemoManager() { }
        public void LoadFiles(params string[] paths)
        {
            foreach (var path in paths)
            {
                var wordReader = new WordMemoReader(path);
                var manager = new MemoManager(wordReader);
                this.LoadManager(manager);
            }
        }

        public void LoadManager(MemoManager manager)
        {
            this.managers.Add(manager);
            this.counts.Add(manager.MemosCount);
        }
        public void Clear() { this.managers.Clear(); }

        public IEnumerable<string> GetOverView()
        {
            var res = this.managers.Select(tt => tt.GetOverView()).SelectMany(tt => tt);
            return res;
        }
        public SingleMemo Random(int levelLow = 0, int levelHigh = 6)
        {
            Random rand = RandomExtension.GetRandom();
            var total = this.counts.Sum();
            var r = rand.Next(0, total);
            int idx = 0;
            foreach (var item in this.counts)
            {
                if (r < item)
                {
                    break;
                }
                r -= item;
                idx++;
            }
            var res = this.managers[idx].GetOne(r);//.Random(levelLow, levelHigh);
            return res;
        }

    }
}
