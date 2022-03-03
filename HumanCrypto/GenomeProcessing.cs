using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class GenomeProcessing {
        byte[] genome;
        int index = 0;

        public GenomeProcessing(byte[] genome) {
            this.genome = genome;
        }

        public int GetNextPartId() {
            int id = genome[index];
            index++;
            return id;
        }
        public Color GetColor() {
            int colorValue = (genome[index] << 16) + (genome[index + 1] << 8) + (genome[index+2]);
            index += 3;
            return Color.FromArgb(colorValue);
        }

    }
}
