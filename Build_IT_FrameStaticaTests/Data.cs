namespace Build_IT_FrameStaticaTests
{
    public class Data
    {
        #region Properties
        
        //public const double Offset = 0.0005;
        public double Position { get; set; }
        public double Fx { get; set; }
        public double Fz { get; set; }
        public double My { get; set; }
        public double Ux { get; set; }
        public double Uz { get; set; }

        public double[] MinMaxFx { get; } = new double[2];
        public double[] MinMaxFz { get; } = new double[2];
        public double[] MinMaxMy { get; } = new double[2];
        public double[] MinMaxUx { get; } = new double[2];
        public double[] MinMaxUz { get; } = new double[2];

        public bool HasTwoValues { get; set; }

        public bool LastOne { get; set; }

        #endregion // Properties
    }
}
