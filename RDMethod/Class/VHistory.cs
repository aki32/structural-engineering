namespace RDMethod
{
    internal class VHistory
    {
        public double TimeStep { get; set; }
        public VHistoryStep[] Steps { get; set; }


        /// <summary>
        /// Forbidden public instanciate
        /// </summary>
        private VHistory()
        {
        }

        /// <summary>
        /// Create Empty VHistory
        /// </summary>
        /// <param name="steps"></param>
        public VHistory(int stepSize, double timeStep)
        {
            Steps = new VHistoryStep[stepSize];
            TimeStep = timeStep;
            for (int i = 0; i < Steps.Length; i++)
                Steps[i] = new VHistoryStep() { t = i * TimeStep };
        }

        /// <summary>
        /// CSV入力パスを指定することで，VHistoryを作成。
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <exception cref="Exception"></exception>
        public VHistory(string inputFilePath)
        {

        }


        /// <summary>
        /// Construct from csv
        /// </summary>
        /// <param name="inputCsvPath"></param>
        /// <exception cref="Exception"></exception>
        public static VHistory FromCsv(string inputCsvPath)
        {
            var vHistory = new VHistory();

            try
            {
                var input = File.ReadLines(inputCsvPath, System.Text.Encoding.UTF8).ToArray();

                // Get TimeStep
                var temp = input
                    .Skip(1)
                    .Take(2)
                    .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None)[0])
                    .Select(x => float.Parse(x))
                    .ToArray();

                vHistory.TimeStep = temp[1] - temp[0];

                // Get Steps
                vHistory.Steps = input
                    .Select(x => x.Split(new string[] { "," }, StringSplitOptions.None))
                    .Skip(1)
                    .Select(x => new VHistoryStep() { t = double.Parse(x[0]), v = double.Parse(x[1]) })
                    .ToArray();

            }
            catch (Exception e)
            {
                throw new Exception("Failed to read input csv");
            }

            return vHistory;

        }

        /// <summary>
        /// Calculate RD Method
        /// </summary>
        /// <returns></returns>
        public VHistory CalcRD(int resultLength)
        {
            var resultHistory = new VHistory(resultLength, TimeStep);

            // ごり押し。極大を認識したら結果に足し合わせる。
            for (int i = 1; i < Steps.Length - resultLength - 1; i++)
            {
                var lastStep = Steps[i - 1];
                var currentStep = Steps[i];
                var nextStep = Steps[i + 1];

                // 今回上がって次下がるなら，極大とみなす。
                if (lastStep.v < currentStep.v && currentStep.v >= nextStep.v)
                {
                    // 極大なら足し合わせる
                    for (int j = 0; j < resultLength; j++)
                        resultHistory.Steps[j].v += Steps[i + j].v;
                }
            }

            return resultHistory;
        }

        /// <summary>
        /// Output result to csv
        /// </summary>
        /// <param name="outputFilePath"></param>
        public void OutputResultToCsv(string outputFilePath)
        {
            using var sw = new StreamWriter(outputFilePath);
            sw.WriteLine(VHistoryStep.ToStringHeader);
            foreach (var step in Steps)
                sw.WriteLine(step.ToString());
        }

    }
}
