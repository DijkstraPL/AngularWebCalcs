﻿using Build_IT_BeamStatica.Data;

namespace Build_IT_BeamStatica.Builders.PointLoads.Interfaces
{
    public interface INodePointLoadBuilder
    {
        #region Public_Methods

        PointLoadData Build();
        bool Validate();

        #endregion // Public_Methods
    }
}