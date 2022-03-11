using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class CachedImages {
        Web3 web3;
        List<Bitmap> cache = new List<Bitmap>();
        GenomeProcessing genomeProcessing;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="genomeProcessing">An empty genome which only stores the structure of the genes</param>
        public CachedImages(Web3 web3, GenomeProcessing genomeProcessing) {
            this.web3 = web3;
            this.genomeProcessing = genomeProcessing;
        }

        /// <summary>
        /// Returns a list of requested avatars using cache or on-the-spot-made transaction
        /// </summary>
        /// <returns></returns>
        private async Task<List<Bitmap>> GetAvatars(int startingIndex, int count) {
            HumanAvatarOwnerService service= new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
            List<Bitmap> results = new List<Bitmap>();

            for (int i = 0; i < count; i++) {
                int bmpIndex = i + startingIndex;

                if (cache.Count > bmpIndex) {
                    results.Add(cache[bmpIndex]);
                    continue;
                }

                // We need to generate the image on the spot
                AvatarsOutputDTO outputResult = null;

                try {
                    var transactionFunction = new AvatarsFunction {
                        MaxFeePerGas = 0,           // This should not consume any gas
                        ReturnValue1 = startingIndex + i
                    };
                    outputResult = await service.AvatarsQueryAsync(transactionFunction);
                } catch (Exception ex) {
                }
                if (outputResult == null) {
                    break;
                }

                genomeProcessing.ParseGenome(outputResult.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);
                Bitmap generatedBmp = picasso.GetBitmap();
                results.Add(generatedBmp);

                // Add new image to cache
                cache.Add(generatedBmp);
            }

            return results;
        }
    }
}
