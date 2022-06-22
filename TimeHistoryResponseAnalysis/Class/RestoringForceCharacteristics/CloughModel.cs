//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TimeHistoryResponseAnalysis.Class.RestoringForceCharacteristics;
//public class CloughModel : RestoringForceCharacteristics
//{

//    #region ★★★★★★★★★★★★★★★ プロパティたち

//    public double beta { get; set; }
//    public double Fy { get; set; }

//    private double MaxF = 0d;
//    private double MaxX = 0d;
//    private double MinF = 0d;
//    private double MinX = 0d;

//    #endregion

//    public CloughModel(double K1, double beta, double Fy)
//    {
//        this.K1 = K1;
//        this.beta = beta;
//        this.Fy = Fy;

//        MaxF = Fy;
//        MaxX = MaxF / K1;
//        MinF = -Fy;
//        MinX = MinF / K1;
//    }

//    public override double CalcNextF(double targetX)
//    {
//        if (LastX == targetX)
//            return CurrentF;

//        LastX = CurrentX;
//        LastF = CurrentF;
//        CurrentX = targetX;

//        // シンプルに，K そのまま与えちゃう。
//        var K = GetK(CurrentX);
//        var dX = CurrentX - LastX;

//        CurrentF += dX * K;
//        RenewMinMax();

//        return CurrentF;
//    }

//    private void RenewMinMax()
//    {
//        if (CurrentX > MaxX)
//        {
//            MaxX = CurrentX;
//            MaxF = CurrentF;
//        }
//        else if (CurrentX < MinX)
//        {
//            MinX = CurrentX;
//            MinF = CurrentF;
//        }
//    }

//    private double GetK(double targetX)
//    {
//        // right
//        if (targetX > LastX)
//        {
//            // from positive
//            if (LastF > 0)
//            {
//                // toward max point
//                if (LastX < MaxX)
//                    return (MaxF - LastF) / (MaxX - LastX);

//                // toward new max point
//                else
//                    return K1 * beta;

//            }

//            // from negative
//            else
//                return K1;
//        }

//        // left
//        else
//        {
//            // from negative
//            if (LastF < 0)
//            {
//                // toward min point
//                if (LastX > MinX)
//                    return (MinF - LastF) / (MinX - LastX);

//                // toward new min point
//                else
//                    return K1 * beta;

//            }

//            // from positive
//            else
//                return K1;
//        }
//    }

//    private double Get2ndLinearF(double X)
//    {
//        var Xy = Fy * K1;
//        var dX = X - Xy;
//        var dF = K1 * beta * dX;
//        return Fy + dF;
//    }

//}
