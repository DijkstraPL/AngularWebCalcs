using Build_IT_DeadLoads.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_DeadLoads
{
    public class LayerGroup 
    {
        private readonly List<MaterialLayer> _materialLayers = new();
        public IEnumerable<MaterialLayer> MaterialLayers => _materialLayers;

        public void AddMaterialLayer(MaterialLayer materialLayer)
        {
            _materialLayers.Add(materialLayer);
        }
        public void AddMaterialLayer(int index, MaterialLayer materialLayer)
        {
            _materialLayers.Insert(index, materialLayer);
        }
        public void RemoveMaterialLayer(MaterialLayer materialLayer)
        {
            _materialLayers.Remove(materialLayer);
        }
        public void RemoveMaterialLayer(int index)
        {
            _materialLayers.RemoveAt(index);
        }

        public double GetMinimumLoad()
        {
            if (_materialLayers.Count == 0)
                return 0;
            var loadUnitToCompare = MaterialLayers.First().LoadUnit;
            if (MaterialLayers.All(ml => ml.LoadUnit == loadUnitToCompare))
                return MaterialLayers.Sum(ml => ml.MinLoad);
            throw new InvalidOperationException("Can't calculate for different units");
        }

        public double GetMaximumLoad()
        {
            if (_materialLayers.Count == 0)
                return 0;
            var loadUnitToCompare = MaterialLayers.First().LoadUnit;
            if (MaterialLayers.All(ml => ml.LoadUnit == loadUnitToCompare))
                return MaterialLayers.Sum(ml => ml.MaxLoad);
            throw new InvalidOperationException("Can't calculate for different units");
        }

        public LoadUnit GetLoadUnit()
        {
            var loadUnitToCompare = MaterialLayers.First().LoadUnit;
            if (MaterialLayers.All(ml => ml.LoadUnit == loadUnitToCompare))
                return loadUnitToCompare;
            throw new InvalidOperationException("Can't calculate for different units");
        }
    }
}
