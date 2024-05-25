using Build_IT_DeadLoads.Enums;
using System;

namespace Build_IT_DeadLoads
{
    public class MaterialLayer
    {
        public Material Material { get; }

        public MaterialLayer(Material material)
        {
            Material = material ?? throw new ArgumentNullException(nameof(material));
        }

        private double? _length;
        /// <summary>
        /// In centimeters
        /// </summary>
        public double? Length
        {
            get => _length;
            set
            {
                _length = value;
                OnSizeChange?.Invoke(this, EventArgs.Empty);
            }
        }
        private double? _width;
        /// <summary>
        /// In centimeters
        /// </summary>
        public double? Width
        {
            get => _width;
            set
            {
                _width = value;
                OnSizeChange?.Invoke(this, EventArgs.Empty);
            }
        }
        private double? _height;
        /// <summary>
        /// In centimeters
        /// </summary>
        public double? Height
        {
            get => _height;
            set
            {
                _height = value;
                OnSizeChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnSizeChange;

        public LoadUnit LoadUnit
        {
            get
            {
                int loadUnit = (int)Material.LoadUnit;
                if (Length.HasValue)
                    loadUnit--;
                if (Width.HasValue)
                    loadUnit--;
                if (Height.HasValue)
                    loadUnit--;
                if (loadUnit >= 0)
                    return (LoadUnit)loadUnit;
                return LoadUnit.WrongUnit;
            }
        }

        public double MinLoad
        {
            get
            {
                double load = Material.MinimumDensity;
                if (Length.HasValue)
                    load *= (double)Length / 100;
                if (Width.HasValue)
                    load *= (double)Width / 100;
                if (Height.HasValue)
                    load *= (double)Height / 100;
                return load;
            }
        }
        public double MaxLoad
        {
            get
            {
                double load = Material.MaximumDensity;
                if (Length.HasValue)
                    load *= (double)Length / 100;
                if (Width.HasValue)
                    load *= (double)Width / 100;
                if (Height.HasValue)
                    load *= (double)Height / 100;
                return load;
            }
        }
    }
}
