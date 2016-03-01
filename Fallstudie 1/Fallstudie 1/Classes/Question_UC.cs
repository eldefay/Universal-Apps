using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fallstudie_1
{
    class Question_UC
    {
        public int id { get; set; }
        public string qText { get; set; }
        public List<Awnser> aList { get; set; }
    }

    public class Awnser
    {
        public bool right { get; set; }
        public String text { get; set; }
    }
}

