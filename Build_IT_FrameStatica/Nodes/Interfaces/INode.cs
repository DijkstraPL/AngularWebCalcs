using Build_IT_FrameStatica.Loads.Interfaces;
using Build_IT_Data.Geometry;
using System.Collections.Generic;

namespace Build_IT_FrameStatica.Nodes.Interfaces
{
    public interface INode : INormalForceProvider, IShearForceProvider, IBendingMomentProvider,
        IDeflectionProvider, IRotationProvider, INumeration
    {
        #region Properties

        Point Position { get; }
        
        short DegreesOfFreedom { get; }
        ICollection<INodeLoad> ConcentratedForces { get; set; }

        #endregion // Properties
    }
}
