using Build_IT_DeadLoads;
using Build_IT_DeadLoads.Enums;
using NUnit.Framework;

namespace Build_IT_DeadLoadsTests
{
    public class LayerGroupTests
    {
        [Test]
        public void MaterialLayer_DifferentUnits()
        {
            var material1 = new Material("", 1, 2, LoadUnit.KiloNewtonPerSquareMeter);
            var material2 = new Material("", 2, 3, LoadUnit.KiloNewtonPerMeter);

            var materialLayer1 = new MaterialLayer(material1);
            var materialLayer2 = new MaterialLayer(material2);

            materialLayer1.Length = 200;
            materialLayer1.Width = 300;

            materialLayer2.Height = 500;

            var layerGroup = new LayerGroup();
            layerGroup.AddMaterialLayer(materialLayer1);
            layerGroup.AddMaterialLayer(materialLayer2);

            Assert.Multiple(() =>
            {
                Assert.That(layerGroup.GetMinimumLoad(), Is.EqualTo(16));
                Assert.That(layerGroup.GetMaximumLoad(), Is.EqualTo(27));
                Assert.That(layerGroup.GetLoadUnit(), Is.EqualTo(LoadUnit.KiloNewton));
            });
        }
    }
}