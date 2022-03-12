using HumanAvatarContract.Contracts.HumanAvatarOwner;
using HumanAvatarContract.Contracts.HumanAvatarOwner.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HumanCrypto {
    class CachedImages {
        Web3 web3;
        Dictionary<int, Bitmap> cache = new Dictionary<int, Bitmap>();
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
        public async Task<List<Bitmap>> GetAllAvatars(int startingIndex, int count) {
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
            List<Bitmap> results = new List<Bitmap>();
            BigInteger avatarsCount = await service.GetAvatarsCountQueryAsync();

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

                genomeProcessing.ParseGenome(outputResult.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);
                Bitmap generatedBmp = picasso.GetBitmap();
                results.Add(generatedBmp);

                // Add new image to cache
                cache[bmpIndex] = generatedBmp;
            }

            return results;
        }

        public async Task<List<Bitmap>> GetOwnAvatars(int startingIndex, int count) {
            HumanAvatarOwnerService service = new HumanAvatarOwnerService(web3, Properties.Secret.Default.ContractKey);
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
                    ReturnValue1 = web3.TransactionManager.Account.Address,
                    ReturnValue2 = bmpIndex
                };
                BigInteger outputResult = await service.AvatarIdsOfAddressQueryAsync(transactionFunction1);

                var transactionFunction2 = new AvatarsFunction {
                    ReturnValue1 = bmpIndex
                };
                AvatarsOutputDTO avatarResult = await service.AvatarsQueryAsync(transactionFunction2);


                genomeProcessing.ParseGenome(avatarResult.Genome.ToByteArray());

                PicassoConstruction picasso = new PicassoConstruction(genomeProcessing);
                Bitmap generatedBmp = picasso.GetBitmap();
                results.Add(generatedBmp);

                // Add new image to cache
                cache[bmpIndex] = generatedBmp;
            }

            return results;
        }
    }
}
