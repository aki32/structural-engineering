﻿namespace RDMethod
{
    internal class RDResultHistory
    {
        public double TimeStep { get; set; }
        public VHistoryStep[] Steps { get; set; }

        /// <summary>
        /// Create Empty VHistory
        /// </summary>
        /// <param name="steps"></param>
        public RDResultHistory(int stepSize, double timeStep)
        {
            Steps = new VHistoryStep[stepSize];
            TimeStep = timeStep;
            for (int i = 0; i < Steps.Length; i++)
                Steps[i] = new VHistoryStep() { t = i * TimeStep };
        }


        /// <summary>
        /// Calc attenuation constant from result
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public double CalcAttenuationConstant(int checkPeakCount, bool consoleOutput = false)
        {
            var peaks = new List<double>();
            var attenuations = new List<double>();
            int peakCount = 0;

            AddPeak(Steps[0]);

            for (int i = 1; i < Steps.Length - 1; i++)
            {
                if (checkPeakCount <= peakCount)
                    break;

                var lastStep = Steps[i - 1];
                var currentStep = Steps[i];
                var nextStep = Steps[i + 1];

                // Simple peak detection
                if (lastStep.v < currentStep.v && currentStep.v >= nextStep.v)
                {
                    peakCount++;
                    AddPeak(currentStep);
                }
            }

            if (consoleOutput)
                Console.WriteLine();

            for (int i = 0; i < peaks.Count - 1; i++)
            {
                var currentPeak = peaks[i];
                var nextPeak = peaks[i + 1];
                var attenuation = 1d / 2 / Math.PI * Math.Log(currentPeak / nextPeak);
                if (consoleOutput)
                    Console.WriteLine($"Attenuation {i}: h={attenuation:F4}");
                attenuations.Add(attenuation);
            }

            var attenuationAve = attenuations.Average();
            if (consoleOutput)
            {
                Console.WriteLine();
                Console.WriteLine($"Attenuation Ave: h={attenuationAve:F4}");
                Console.WriteLine();
            }
            return attenuationAve;

            void AddPeak(VHistoryStep addingStep)
            {
                if (consoleOutput)
                    Console.WriteLine($"Peak {peaks.Count}: t={addingStep.t:F2}, v={addingStep.v}");
                peaks.Add(addingStep.v);
            }
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