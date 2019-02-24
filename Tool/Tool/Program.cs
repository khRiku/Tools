using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{

    public enum Option
    {
        t,   //转换
        r,   //tihuan 
    }
    class Program
    {
        static void Main(string[] args)
        {
            string tTransform = Option.t.ToString();
            string tReplace = Option.r.ToString();
     
            Console.WriteLine("转换（{0}） / 替换（{1}）", tTransform, tReplace);

            string tOptionStr = "";
            while (true)
            {
                tOptionStr = Console.ReadLine();
                if (tOptionStr == tTransform || tOptionStr == tReplace)
                    break;

                Console.WriteLine("请输入{0}/{1}", tTransform, tReplace);
            }

            Option tOption = (Option)Enum.Parse(typeof(Option), tOptionStr);
            switch(tOption)
            {
                case Option.r:
                    Replace();
                    break;


                case Option.t:
                    TransformFormat();
                    break;
            }

            Console.Read();
        }

        private static void TransformFormat()
        {
            Console.WriteLine("转换格式");
        }

        private static void Replace()
        {
            Console.WriteLine("替换");

        }
}
}
