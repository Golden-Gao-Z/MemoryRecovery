using NPOI.HPSF;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.Service
{

    public class MemoManager 
    {
        public BaseMemoReader MemoReader { get; private set; }
        public MemoManager(BaseMemoReader memoReader)
        {
            this.MemoReader = memoReader;

        }

        public string ReadDocumentText()
        {
            
            throw new NotImplementedException();
        }
    }
}
