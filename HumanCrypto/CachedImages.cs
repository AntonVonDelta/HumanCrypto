using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanCrypto {
    class CachedImages:IDisposable {
        Web3Controller controller;
        Dictionary<int, Bitmap> cache = new Dictionary<int, Bitmap>();


        public CachedImages(Web3Controller controller) {
            this.controller = controller;
        }

        public void Dispose() {
            for(int i=0;i<cache.Count; i++) {
                cache.ElementAt(i).Value.Dispose();
            }
        }

        /// <summary>
        /// Returns a list of requested avatars using cache or on-the-spot-made transaction
        /// </summary>
        /// <returns></returns>
        public async Task<List<Bitmap>> GetAllAvatars(int startingIndex, int count) {
            List<Bitmap> results = new List<Bitmap>();
            BigInteger avatarsCount = await controller.GetAvatarsCountAsync();

            for (int i = 0; i < count; i++) {
                int bmpIndex = i + startingIndex;

                // Do not go over the total number of avatars
                if (bmpIndex >= avatarsCount) break;

                if (cache.ContainsKey(bmpIndex)) {
                    results.Add(cache[bmpIndex]);
                    continue;
                }

                // We need to generate the image on the spot
                AvatarsOutputDTO outputResult = await controller.AvatarsQueryAsync(bmpIndex);

                GenomeProcessing genomeProcessing=new GenomeProcessing();
                genomeProcessing.ParseGenome(outputResult.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction();
                Bitmap generatedBmp = picasso.GetBitmap();
                results.Add(generatedBmp);

                // Add new image to cache
                cache[bmpIndex] = generatedBmp;
            }

            return results;
        }

        public async Task<List<Bitmap>> GetOwnAvatars(int startingIndex, int count) {
            List<Bitmap> results = new List<Bitmap>();
            BigInteger avatarsCount = await controller.GetAvatarIdsOfAddressCountAsync();

            for (int i = 0; i < count; i++) {
                int bmpIndex = i + startingIndex;

                // Do not go over the total number of avatars
                if (bmpIndex >= avatarsCount) break;

                if (cache.ContainsKey(bmpIndex)) {
                    results.Add(cache[bmpIndex]);
                    continue;
                }


                BigInteger outputResult = await controller.AvatarIdsOfAddressQueryAsync(bmpIndex);
                AvatarsOutputDTO avatarResult = await controller.AvatarsQueryAsync(bmpIndex);

                GenomeProcessing genomeProcessing = new GenomeProcessing();
                genomeProcessing.ParseGenome(avatarResult.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction();
                Bitmap generatedBmp = picasso.GetBitmap();
                results.Add(generatedBmp);

                // Add new image to cache
                cache[bmpIndex] = generatedBmp;
            }

            return results;
        }
    }
}
