using CreateAccFromCsv.Class;

namespace CreateAccFromCsv
{
    public partial class Program
    {
        /// <summary>
        /// B2 以降に加速度データの入った CSV を，ACCに変換。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"★ 処理開始！");
            Console.WriteLine();


            // パスで指定
            {
                //var inputFile = new FileInfo($@"..\..\..\# TestModel\kobe L1.csv");
                //inputFile.CreateAccFromCsv();
            }

            // .exe にドラッグで開始。
            {
                //string[] files = Environment.GetCommandLineArgs();

                //if (files.Length > 1)
                //{
                //    for (int i = 1; i < files.Length; i++)
                //    {
                //        var inputFilePath = files[i];
                //        var inputFile = new FileInfo(inputFilePath);
                //        inputFile.CreateAccFromCsv();
                //    }
                //}
                //else
                //{
                //    //配列の要素数が1の時、コマンドライン引数は存在しない
                //    Console.WriteLine();
                //    Console.WriteLine("2行目からA列に時間・B列に加速度を配置した.csvファイルを.exeファイル上にドラッグ＆ドロップすることで，変換を実行できます。");
                //    Console.WriteLine("今回ドロップされたファイルはありませんでした。");
                //}
            }

            // 対話形式
            {
                Console.WriteLine("2行目からA列に時間・B列に加速度を配置した.csvファイルのパスを入力することで処理を実行できます。");

                while (true)
                {
                    Console.WriteLine("================================================");
                    Console.WriteLine("処理したいデータのパスを入力：");

                    var input = Console.ReadLine();
                    input = input.Trim('\"');
                    try
                    {
                        var inputFile = new FileInfo(input);
                        inputFile.CreateAccFromCsv();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("失敗：入力が正しいか確認してください。");
                    }
                }
            }


            Console.WriteLine();
            Console.WriteLine($"★ 処理終了！");
            Console.WriteLine();

            Console.ReadLine();
        }


    }
}
