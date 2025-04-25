using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_Lab2.Command
{
    public class CommandManager
    {
        private List<Frame> ReDoOp= new List<Frame>() ;
        private List<Frame> UnDoOp= new List<Frame>();

        public void Save(Frame f)
        {
            if (UnDoOp.Count < 250)
            {
                UnDoOp.Add(f);
            }
            else
            {
                UnDoOp.RemoveAt(0);
                UnDoOp.Add(f);
            }
            ReDoOp.Clear();
        }
        public string UnDo()
        {
            if(UnDoOp.Count>1)
            {
                Frame tmp = UnDoOp[UnDoOp.Count - 1];
                
                ReDoOp.Add(tmp);
                UnDoOp.RemoveAt(UnDoOp.Count - 1);
                tmp= UnDoOp[UnDoOp.Count - 1];
                tmp.Write();
                return tmp.txt;
            }
            return "";
        }
        public string ReDo()
        {
            if (ReDoOp.Count > 0)
            {
                Frame tmp = ReDoOp[ReDoOp.Count - 1];
                tmp.Write();
                UnDoOp.Add(tmp);
                ReDoOp.RemoveAt(ReDoOp.Count - 1);
                return tmp.txt;
            }
            return "";
        }
    }
}
