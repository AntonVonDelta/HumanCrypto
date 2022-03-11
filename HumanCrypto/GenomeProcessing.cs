using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class GenomeProcessing {
        byte[] genomeSignature;
        int[] genomeAccumulatedSizes;
        byte[] genome;
        int geneIndex = 0;
        int byteIndex = 0;

        public GenomeProcessing(byte[] genomeSignature) {
            this.genomeSignature = genomeSignature;
            genomeAccumulatedSizes = new int[genomeSignature.Count()];
            genomeAccumulatedSizes[0] = genomeSignature[0];

            for (int i = 1; i < genomeSignature.Count(); i++) {
                genomeAccumulatedSizes[i] = genomeAccumulatedSizes[i - 1] + genomeSignature[i-1];
            }

            genome = new byte[genomeAccumulatedSizes.Last()];
            Randomize();
        }

        public void Reset() {
            byteIndex = 0;
            geneIndex = 0;
        }
        public void Randomize() {
            new Random().NextBytes(genome);
        }

        public void ParseGenome(byte[] newGenome) {
            for(int i = 0; i < genome.Length; i++) {
                if (i < newGenome.Length) genome[i] = newGenome[i];
                else genome[i] = 0;
            }
        }

        public void LoadGene(int geneIndex) {
            this.geneIndex = geneIndex;
        }
        public void AdvanceGene() {
            geneIndex++;
        }


        public int GetNextPartId() {
            CheckValidIndex();

            int id = genome[byteIndex];
            byteIndex++;
            return id;
        }
        public Color GetColor() {
            CheckValidIndex();

            int colorValue = (255<<24) + (genome[byteIndex] << 16) + (genome[byteIndex + 1] << 8) + (genome[byteIndex + 2]);
            byteIndex += 3;
            return Color.FromArgb(colorValue);
        }

        private void CheckValidIndex() {
            if (byteIndex - genomeAccumulatedSizes[geneIndex] >= genomeSignature[geneIndex]) {
                throw new Exception("Gene was accessed in an invalid way");
            }
        }
    }
}
