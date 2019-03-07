using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;

/// <summary>
/// author: YXX
/// date: 2019/2/25
/// 
/// 格式替换和转换工具
/// 作用：用来备份代码的
/// 使用方法， 把生成的exe放于要处理的文件夹下， 会在文件夹的中新建一个文件夹_XXX 来存放转换的代码
/// 操作：
///     转换： 按文件夹位置进行转换后进行存放
///     替换： 用备份的文件替换掉原本的文件
///     恢复文件目录： 根据JsonData.cs 文件的内容还原内容（包含目录结构）
/// 注意：使用工具的时候， 请先将项目请先本地提交一遍, 保存一下， 否则要是出错， 就麻烦了
/// </summary>

namespace Tool
{

    public enum Option
    {
        t,   //转换
        d,   //恢复文件目录
        r,   //替换文件
        
    }


    class Program
    {
        public static string mCurDiretory = "";

        static void Main(string[] args)
        {
            //工作路径设置
            mCurDiretory = Environment.CurrentDirectory;
            mCurDiretory = @"E:\github\Tools\Tool\Tool\Class";


            string tTransform = Option.t.ToString();
            string tReplace = Option.r.ToString();
            string tDiretory = Option.d.ToString();



            Console.WriteLine("转换（{0}） / 恢复文件目录{1} / 替换（{2}）", tTransform, tDiretory, tReplace);

            string tOptionStr = "";
            while (true)
            {
                tOptionStr = Console.ReadLine();
                if (tOptionStr == tTransform || tOptionStr == tReplace || tOptionStr == tDiretory)
                    break;

                Console.WriteLine("请输入{0}/{1}/{2}", tTransform, tReplace, tDiretory);
            }

            Console.WriteLine("请输入目录地址：");
      //      mCurDiretory = Console.ReadLine();

            Option tOption = (Option)Enum.Parse(typeof(Option), tOptionStr);
            switch(tOption)
            {
                case Option.r:
                    Replace();
                    break;


                case Option.t:
                    TransformToOneFile();
                    break;

                case Option.d:
                    TransformWithFildStructure();
                    break;
            }

            Console.Read();
        }

        /// <summary>
        /// 上级文件夹
        /// </summary>
        public static string mPreFolder
        {
            get
            {
                string tCruuretnPath = mCurDiretory;

                int tLastIndex = tCruuretnPath.LastIndexOf("\\");
                string tRootFolder = tCruuretnPath.Remove(tLastIndex, tCruuretnPath.Length - tLastIndex);

                return tRootFolder;
            }
        }

        public static string mTrasformPostfix = "_Transform\\Assets";
        public static string mExtraExtension = ".meta";

        public static string[] mProcessExtension =
        {
            ".cs",
            ".shader",
            ".txt"
        };

        /// <summary>
        /// 将所有内容转换为一个文件
        /// </summary>
        private static void TransformToOneFile()
        {
            AllFileData tAllFileData = new AllFileData();
            tAllFileData.mStartPath = mCurDiretory;

            Console.WriteLine("格式转换开始 ... ... ");
            {
                DirectoryInfo tDierectInfo = new DirectoryInfo(mCurDiretory);
                FileInfo[] tFileInfoArr = tDierectInfo.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < tFileInfoArr.Length; ++i)
                {
                    FileInfo tFileInfo = tFileInfoArr[i];
                    if (mProcessExtension.Contains(tFileInfo.Extension) == false)
                        continue;

                    OneFileData tOneFileData = new OneFileData();
                    tOneFileData.mRelativePath = tFileInfo.FullName.Replace(mCurDiretory, "");
                    tOneFileData.mContent = File.ReadAllText(tFileInfo.FullName);

                    tAllFileData.mOneFileDataList.Add(tOneFileData);
                }
            }

            File.WriteAllText(mCurDiretory + "\\" + mAllFileName, JsonMapper.ToJson(tAllFileData));
            Console.WriteLine("格式转换成功结束 ");

        }


        private static string mAllFileName = "JsonData.cs";
        private static string mStrucFolderPostfixName = "_FileStructure";
        /// <summary>
        /// 将文件内荣转换出文件结构
        /// </summary>
        private static void TransformWithFildStructure()
        {
            Console.WriteLine("恢复结构开始 ... ... ");
            {
                string tDataPath = mCurDiretory + "\\" + mAllFileName;
                string tData = File.ReadAllText(tDataPath);
                if (string.IsNullOrEmpty(tData))
                {
                    Console.WriteLine("错误：读取内容为空， 请确保该文件是否存在 路径 = " + tDataPath);
                    Console.WriteLine("恢复失败");
                    return;
                }

                AllFileData tAllFileData = JsonMapper.ToObject<AllFileData>(tData);

                string tStartFolderPath = mCurDiretory + mStrucFolderPostfixName;

                for (int i = 0; i < tAllFileData.mOneFileDataList.Count; ++i)
                {
                    OneFileData tOneFileData = tAllFileData.mOneFileDataList[i];

                    string tSavePath = tStartFolderPath + "\\" + tOneFileData.mRelativePath;

                    int tLastSperatIndex = tSavePath.LastIndexOf(@"\");
                    string tFolderPath = tSavePath.Remove(tLastSperatIndex, tSavePath.Length - tLastSperatIndex);
                    if (Directory.Exists(tFolderPath) == false)
                        Directory.CreateDirectory(tFolderPath);
                    File.WriteAllText(tSavePath, tOneFileData.mContent);
                }
            }
            Console.WriteLine("恢复结构成功结束 ");

        }

        /// <summary>
        /// 替换原本文件
        /// </summary>
        private static void Replace()
        {
            Console.WriteLine("开始替换文件 ... ... ");
            {
                string tDataPath = mCurDiretory + "\\" + mAllFileName;
                string tData = File.ReadAllText(tDataPath);
                if (string.IsNullOrEmpty(tData))
                {
                    Console.WriteLine("错误：读取内容为空， 请确保该文件是否存在 路径 = " + tDataPath);
                    Console.WriteLine("恢复失败");
                    return;
                }

                AllFileData tAllFileData = JsonMapper.ToObject<AllFileData>(tData);
                string tStartFolderPath = mCurDiretory;

                for (int i = 0; i < tAllFileData.mOneFileDataList.Count; ++i)
                {
                    OneFileData tOneFileData = tAllFileData.mOneFileDataList[i];

                    string tSavePath = tStartFolderPath + "\\" + tOneFileData.mRelativePath;

                    int tLastSperatIndex = tSavePath.LastIndexOf(@"\");
                    string tFolderPath = tSavePath.Remove(tLastSperatIndex, tSavePath.Length - tLastSperatIndex);
                    if (Directory.Exists(tFolderPath) == false)
                        Directory.CreateDirectory(tFolderPath);
                    
                    File.WriteAllText(tSavePath, tOneFileData.mContent);
                }
            }            
            Console.WriteLine("文件替换结束 ");

        }
}
}
