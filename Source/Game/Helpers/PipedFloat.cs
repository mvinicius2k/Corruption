
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    [Serializable]
    public class PipedFloat 
    {
        
        private float baseValue;
        [ShowInEditor]
        private List<float> multiplicators;
        private bool outdated;
        private float totalValue;

        public float TotalValue
        {
            get
            {
                if (outdated)
                {

                    totalValue = baseValue;
                    if(multiplicators != null)
                    {
                        for (int i = 0; i < multiplicators.Count; i++)
                            totalValue *= multiplicators[i];    
                       
                    }
                    outdated = false;

                }
                return totalValue;
            }
        }

        public float BaseValue { 
            get => baseValue;
            set
            {
                baseValue = value;
                outdated = true;
            } 
        }
        public List<float> Multiplicators
        {
            get
            {
                multiplicators ??= new List<float>();
                outdated = true;
                return multiplicators;
            }
            
        }

        public IReadOnlyList<float> ReadMultiplicators()
        {
            multiplicators ??= new List<float>();
            return multiplicators.AsReadOnly();
        }

       
    }
}
