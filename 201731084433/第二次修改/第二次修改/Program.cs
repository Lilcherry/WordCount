using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 第三次作业代码
{
    //源文件位置：D:\a.txt;
    //输出文件位置：D\new.txt;
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入文件路径：");
            String path = Console.ReadLine();
            String r, content = null;
            StreamReader reader = new StreamReader(path, Encoding.Default);
            try
            {
                while ((r = reader.ReadLine()) != null)
                {
                    content += (r + "\n");
                }
                Console.WriteLine("文件内容如下：");
                Console.WriteLine(content);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            reader.Close();
            Program p = new Program();
            p.Lines(content);
            p.Character(content);
            string[] str = p.Words(content);
            IOrderedEnumerable<KeyValuePair<String, int>> dicSort = p.Times(str, path);
            p.Times(str, path);
            Console.Write("请输入词组长度：");
            int m = int.Parse(Console.ReadLine());
            p.NewTimes(str, m);
            Console.Write("请输入单词数量：");
            int n = int.Parse(Console.ReadLine());
            p.Newcount(str, dicSort, n);
            Console.Write("请输入输出文件位置：");
            string o = Console.ReadLine();
            p.Output(str, dicSort, o);
            Console.Write("写入成功...");
            Console.ReadKey();

        }
        public void Character(string c)
        {

            int character = 0;
            char[] ch = c.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] > 127)
                    continue;
                else
                    character++;
            }
            Console.WriteLine("character:" + (character - 1));
        }//统计文件的字符数
        public string[] Words(string c)
        {
            int count = 0;
            int num = 0;
            string c1 = c.ToLower();
            char[] content = c1.ToCharArray();
            string[] str = new string[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                str[i] = "";
            }
            for (int i = 0; i < content.Length; i++)
            {
                //找出类似单词的字符串
                if ((content[i] > 47 && content[i] < 58) || (content[i] > 96 && content[i] < 123))
                {
                    num++;
                }
                //判断这个字符串是否为一个单词
                else
                {
                    if (num < 4)
                    {
                        num = 0;
                        break;
                    }
                    else
                    {
                        for (int j = i - num; j < i - num + 4; j++)
                        {
                            if (content[j] < 97 || content[j] > 122)
                            {
                                num = 0;
                                break;
                            }
                        }
                        if (num != 0)
                        {

                            for (int k = i - num; k < i; k++)
                            {
                                str[count] += content[k].ToString();
                            }
                            count++;
                        }
                        num = 0;

                    }
                }
            }
            Console.WriteLine("words:" + count);
            return str;

        }//统计文件的单词总数
        public void Lines(string c)
        {
            char[] ch = c.ToCharArray();
            int line = 0;
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] == '\n')
                {
                    line++;
                }
            }
            Console.WriteLine("lines:" + line);
        }//统计文件的有效行数
        public IOrderedEnumerable<KeyValuePair<String, int>> Times(string[] str, string path)
        {
            int num = 0;
            string[] str1 = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                str1[i] = "";
            }
            for (int i = 0; str[i] != ""; i++)
            {
                str1[i] = str[i];
            }
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int i = 0; i < str1.Length - 1; i++)
            {
                int T = 1;
                if (str1[i] == "")
                    continue;
                for (int j = i + 1; j < str1.Length; j++)
                {
                    if (str1[i] == str1[j])
                    {
                        T++;
                        str1[j] = "";
                    }
                }
                dic.Add(str1[i], T);
            }
            var dicSort = dic.OrderByDescending(objDic => objDic.Value).ThenBy(objDic => objDic.Key);//排序
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                num++;
                if (num <= 10)
                    Console.WriteLine("<" + kvp.Key + ">:" + kvp.Value);
            }
            return dicSort;
        }//统计文件中各单词的出现次数，最终只输出频率最高的10个。
        public void NewTimes(string[] str, int m)
        {
            string[] str2 = new string[str.Length];
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int i = 0; i < str.Length; i++)
            {
                str2[i] = "";
            }
            for (int i = 0; str[i] != ""; i++)
            {
                for (int j = i; j < i + m; j++)
                {
                    str2[i] += (str[j] + " ");
                }
                if (str[i + m] == "")
                {
                    break;
                }
            }
            for (int i = 0; i < str2.Length - 1; i++)
            {
                int T = 1;//次数
                if (str2[i] == "")
                    continue;
                for (int j = i + 1; j < str2.Length; j++)
                {
                    if (str2[i] == str2[j])
                    {
                        T++;
                        str2[j] = "";
                    }
                }
                dic.Add(str2[i], T);
            }
            var dicSort = dic.OrderByDescending(objDic => objDic.Value).ThenBy(objDic => objDic.Key);//排序
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {

                Console.WriteLine("<" + kvp.Key + ">:" + kvp.Value);
            }
        }//新功能：统计文件夹中指定长度的词组的词频
        public void Newcount(string[] str, IOrderedEnumerable<KeyValuePair<String, int>> dicSort, int n)
        {

            int num = 0;
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                num++;
                if (num <= n)
                    Console.WriteLine("<" + kvp.Key + ">:" + kvp.Value);
            }
        }//新功能：能输出用户指定的前n多的单词与其数量
        public void Output(string[] str, IOrderedEnumerable<KeyValuePair<String, int>> dicSort, string o)
        {

            StreamWriter writer = new StreamWriter(o, false);
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                writer.WriteLine(kvp.Key);
            }
            writer.Close();
        }//新功能：将统计的单词按照字典序输出到新文件new.txt
    }
}
