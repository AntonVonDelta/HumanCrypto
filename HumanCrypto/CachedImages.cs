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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="genomeProcessing">An empty genome which only stores the structure of the genes</param>
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
                AvatarsOutputDTO outputResult = await service.AvatarsQueryAsync(new AvatarsFunction { ReturnValue1 = bmpIndex });

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
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(wallet.GetWeb3(), Properties.Secret.Default.ContractKey);
            List<Bitmap> results = new List<Bitmap>();
            BigInteger avatarsCount = await service.GetAvatarIdsOfAddressCountQueryAsync();

            for (int i = 0; i < count; i++) {
                int bmpIndex = i + startingIndex;

                // Do not go over the total number of avatars
                if (bmpIndex >= avatarsCount) break;

                if (cache.ContainsKey(bmpIndex)) {
                    results.Add(cache[bmpIndex]);
                    continue;
                }

                var transactionFunction1 = new AvatarIdsOfAddressFunction {
                    ReturnValue1 = wallet.GetWeb3().TransactionManager.Account.Address,
                    ReturnValue2 = bmpIndex
                };
                BigInteger outputResult = await service.AvatarIdsOfAddressQueryAsync(transactionFunction1);

                var transactionFunction2 = new AvatarsFunction {
                    ReturnValue1 = bmpIndex
                };
                AvatarsOutputDTO avatarResult = await service.AvatarsQueryAsync(transactionFunction2);

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
