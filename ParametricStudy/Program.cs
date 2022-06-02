using System.Data;
using System.Text;
using ClosedXML.Excel;

namespace ParametricStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Shift-JISを扱うためには必要。
            Console.WriteLine();
            Console.WriteLine($"★ Process Started!");
            Console.WriteLine();



            // Define IO paths and target wave names
            var baseDirPath = @"..\..\..\# TestModel";
            var inputDirPath = $@"{baseDirPath}\input";
            var outputDirPath = $@"{baseDirPath}\output";

            //var inputDirPath = @"F:\e-defenseモデル\model";
            //var outputDirPath = @"C:\Users\aki32\Dropbox\Documents\02 東大関連\0 授業\3 建築学専攻\建築構造・材料演習\# 演習\e-defenseモデル\calc\集計処理";



            // Run for many waves
            var targets = new List<(int column, string waveName)>() { new(1, "PSV40"), new(3, "PSV81"), };
            foreach (var target in targets)
            {
                Console.WriteLine();
                Console.WriteLine(target.waveName);

                // ルートのフォルダ。
                var csvs = Directory.GetFiles(inputDirPath, "*.NAP-AVDQRFMList.csv", SearchOption.TopDirectoryOnly);

                // Excel (※A1セルの座標が(1,1))
                using var workbook = new XLWorkbook();
                var worksheet = workbook.AddWorksheet("一覧");

                // CsvからExcelにコピーさせる関数
                void CopyCsvToExcelColumn(string csvPath, int targetCsvColumn, int targetExcelColumn)
                {
                    var sr = new StreamReader(csvPath, Encoding.GetEncoding("SHIFT_JIS"));
                    var all = sr.ReadToEnd().Split("\r\n").Select(x => { try { return x.Split(',')[targetCsvColumn]; } catch { return ""; } }).ToArray();

                    for (int i = 0; i < all.Count(); i++)
                        worksheet.Cell(i + 1, targetExcelColumn + 1).Value = all[i];
                }

                // 最初の1列を持ってくる。
                CopyCsvToExcelColumn(csvs[0], 0, 0);

                // 他の全てを持ってくる
                foreach (var csv in csvs)
                {
                    Console.WriteLine(csv);
                    int lastColumn = worksheet.LastColumnUsed().ColumnNumber();
                    CopyCsvToExcelColumn(csv, target.column, lastColumn);
                    worksheet.Cell(1, lastColumn + 1).Value = Path.GetFileName(csv).Replace(".NAP-AVDQRFMList.csv", "");
                }

                workbook.SaveAs($@"{outputDirPath}\{target.waveName}.xlsx");
            }



            Console.WriteLine();
            Console.WriteLine($"★ Process Finished!");
            Console.WriteLine();

            Console.ReadLine();
        }

    }
}
