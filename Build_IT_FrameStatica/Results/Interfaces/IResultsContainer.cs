using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_FrameStatica.Results.Interfaces
{
    public interface IResultsContainer
    {
        #region Properties

        IGetResult NormalForce { get; }
        IGetResult Shear { get; }
        IGetResult BendingMoment { get; }
        IGetResult NormalDeflection { get; }
        IGetResult ShearDeflection { get; }
        IGetResult Rotation { get; }

        #endregion // Properties

        #region Public_Methods

        IResultValue GetHorizontalDeflection(double position, short spanNumber);
        IResultValue GetVerticalDeflection(double position, short spanNumber);

        #endregion // Public_Methods
    }
}
