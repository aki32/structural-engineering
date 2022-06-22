using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
public class CloughModel_Simple : IRestoringForceCharacteristics
{
    public double K1 { get; set; }
    public double beta { get; set; }
    public double Fy { get; set; }

    private double lastX = 0d;
    private double lastF = 0d;

    private double MaxF = 0d;
    private double MaxX = 0d;
    private double MinF = 0d;
    private double MinX = 0d;


    public CloughModel_Simple(double K1, double beta, double Fy)
    {
        this.K1 = K1;
        this.beta = beta;
        this.Fy = Fy;

        MaxF = Fy;
        MaxX = MaxF / K1;
        MinF = -Fy;
        MinX = MinF / K1;
    }

    double IRestoringForceCharacteristics.CalcNextF(double targetX)
    {
        // TODO: organize
        if (targetX == lastX)
            return lastF;

        // まずはKそのまま与えちゃう。
        var K = GetK(targetX);
        var dX = targetX - lastX;

        lastX = targetX;
        lastF += dX * K;
        RenewMinMax();

        return lastF;
    }

    private void RenewMinMax()
    {
        if (lastX > MaxX)
        {
            MaxX = lastX;
            MaxF = lastF;
        }
        else if (lastX < MinX)
        {
            MinX = lastX;
            MinF = lastF;
        }
    }

    private double GetK(double targetX)
    {
        // right
        if (targetX > lastX)
        {
            // from positive
            if (lastF > 0)
            {
                // toward max point
                if (lastX < MaxX)
                    return (MaxF - lastF) / (MaxX - lastX);

                // toward new max point
                else
                    return K1 * beta;

            }

            // from negative
            else
                return K1;
        }

        // left
        else
        {
            // from negative
            if (lastF < 0)
            {
                // toward min point
                if (lastX > MinX)
                    return (MinF - lastF) / (MinX - lastX);

                // toward new min point
                else
                    return K1 * beta;

            }

            // from positive
            else
                return K1;
        }
    }

    private double Get2ndLinearF(double X)
    {
        var Xy = Fy * K1;
        var dX = X - Xy;
        var dF = K1 * beta * dX;
        return Fy + dF;
    }


}
