using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreateAccFromCsv
{
    public partial class Program
    {
        /// <summary>
        /// B2 以降に加速度データの入った CSV を，ACCに変換。
        /// .exe に .csv をドロップすることで起動。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ 処理開始！");
            Console.WriteLine();



            // Define IO paths and target wave names
            string baseDirPath = "", inputFilePath = "", outputFilePath = "";

            // パスで指定
            if (true)
            {
                baseDirPath = @"..\..\..\# TestModel";
                inputFilePath = $@"{baseDirPath}\kobe L1.csv";
                outputFilePath = inputFilePath.Replace(".csv", ".acc");
                HandleOne(inputFilePath, outputFilePath);
            }

            // .exe にドラッグで開始。
            if (true)
            {
                string[] files = Environment.GetCommandLineArgs();

                if (files.Length > 1)
                {
                    for (int i = 1; i < files.Length; i++)
                    {
                        inputFilePath = files[i];
                        outputFilePath = inputFilePath.Replace(".csv", ".acc");
                        HandleOne(inputFilePath, outputFilePath);
                    }
                }
                else
                {
                    //配列の要素数が1の時、コマンドライン引数は存在しない
                    Console.WriteLine();
                    Console.WriteLine("2行目からA列に時間・B列に加速度を配置した.csvファイルを.exeファイル上にドラッグ＆ドロップすることで，変換を実行できます。");
                    Console.WriteLine("今回ドロップされたファイルはありませんでした。");
                }
            }


            Console.WriteLine();
            Console.WriteLine($"★ 処理終了！");
            Console.WriteLine();

            Console.ReadLine();
        }


    }
}
