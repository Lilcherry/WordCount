using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 第三次作业代码
{
    //文件位置：D:\a.txt
    class Program
    {
        static void Main(string[] args)
        {
            String path = Console.ReadLine();
            String r, r1 = null;
            StreamReader reader = new StreamReader(path, Encoding.Default);
            try
            {
                while ((r = reader.ReadLine()) != null)
                {
                    r1 += (r + "\n");
                }
                Console.WriteLine(r1);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            reader.Close();
            Program p = new Program();
            p.Lines(r1);
            p.Character(r1);
            string[] str = p.Words(r1);
            p.Times(str, path);
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
        }
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

        }
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
        }
        public void Times(string[] str, string path)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            StreamWriter writer = new StreamWriter(path, true);
            for (int i = 0; i < str.Length - 1; i++)
            {
                int T = 1;
                if (str[i] == "")
                    continue;
                for (int j = i + 1; j < str.Length; j++)
                {
                    if (str[i] == str[j])
                    {
                        T++;
                        str[j] = "";
                    }
                }
                dic.Add(str[i], T);
            }
            var dicSort = dic.OrderByDescending(objDic => objDic.Value).ThenBy(objDic => objDic.Key);//排序
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                Console.WriteLine("<" + kvp.Key + ">:" + kvp.Value);
                writer.WriteLine(kvp.Key);
            }
            writer.Close();
        }
    }
}