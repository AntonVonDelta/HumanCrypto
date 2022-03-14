// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.8.12;

contract HumanAvatarOwner {
    struct Offer{
        bool active;
        uint256 avatarId;
        uint256 amount;
    }
    struct Human{
        uint256 momId;
        uint256 dadId;

        address avatarOwner;
        uint256 generation;
        uint256 genome;
    }

    address owner;
    uint randomNonce=0;

    Human[] public avatars;
    mapping(uint256 => Offer) public avatarOffer;               // Only the owner can post an ofer for the avatar
    mapping(address => uint256[]) public avatarIdsOfAddress;

    modifier onlyOwner(){
        require(msg.sender == owner);
        _;
    }

    function getAvatarsCount() public view returns(uint256){
        return avatars.length;
    }
    function getAvatarIdsOfAddressCount() public view returns(uint256){
        return avatarIdsOfAddress[msg.sender].length;
    }

    constructor(){
        owner=msg.sender;
    }

    function createOffer(uint256 avatarId,uint256 amount) external{
        require(avatarId<avatars.length,"Avatar not found");
        require(amount!=0,"No zero amount offer allowed");
        require(avatars[avatarId].avatarOwner==msg.sender, "You are not owner of avatar");

        Offer memory newOffer=Offer({
            active:true,
            avatarId:avatarId,
            amount:amount
        });

        avatarOffer[avatarId]=newOffer;
    }
    function cancelOffer(uint256 avatarId) external{
        require(avatarId<avatars.length,"Avatar not found");
        require(avatars[avatarId].avatarOwner==msg.sender, "You are not owner of avatar");

        avatarOffer[avatarId].active=false;
    }

    function acceptOffer(uint256 avatarId) external payable{
        require(avatarId<avatars.length,"Avatar not found");
        require(avatars[avatarId].avatarOwner!=msg.sender, "You cannot accept your own offer");
        require(avatarOffer[avatarId].active, "No active offer for this avatar");
        require(msg.value >= avatarOffer[avatarId].amount, "Not enough coins for the offer");

        address payable prevOwner=payable(avatars[avatarId].avatarOwner);
        avatarOffer[avatarId].active=false;
        avatars[avatarId].avatarOwner=msg.sender;

        // Also modify the mappings
        removeAvatarIdFromMapping(prevOwner,avatarId);
        avatarIdsOfAddress[msg.sender].push(avatarId);

   
        uint256 refund=msg.value-avatarOffer[avatarId].amount;

        prevOwner.transfer(avatarOffer[avatarId].amount);
        payable(msg.sender).transfer(refund);
    }

    function breedBetween(uint256 momAvatarId,uint256 dadAvatarId) external{
        require(avatars[momAvatarId].avatarOwner==msg.sender, "You can only breed your own avatars");
        require(avatars[dadAvatarId].avatarOwner==msg.sender, "You can only breed your own avatars");

        // Calculate new generation numbers as the max between parents +1
        uint256 newGeneration=avatars[momAvatarId].generation;
        if(avatars[dadAvatarId].generation>avatars[momAvatarId].generation){
            newGeneration=avatars[dadAvatarId].generation;
        }
        newGeneration++;

        avatars.push(Human({
            momId:momAvatarId,
            dadId:dadAvatarId,
            avatarOwner: owner,
            generation:newGeneration,
            genome:mixDNA( avatars[momAvatarId].genome, avatars[dadAvatarId].genome   )
        }));
        avatarIdsOfAddress[msg.sender].push(avatars.length-1);
    }
    function mixDNA(uint256 momGenes, uint256 dadGenes) private returns (uint256){
        uint8[16] memory genomeGeneStructure=[1,3, 1,3, 1,3, 1,3, 1,3, 1,3, 1,3, 1,3 ];
        uint8[] memory newGenomeArray;
        uint8 RANDOM_DNA_THRESHOLD=7;
        uint256 randomSeed=random();


        uint256 dnaBytesCount=0;
        for(uint i=0;i<genomeGeneStructure.length;i++){
            dnaBytesCount+=genomeGeneStructure[i];
        }
        
        newGenomeArray=new uint8[](dnaBytesCount);
        uint256 randomDnaValues = uint256(keccak256(abi.encodePacked(randomSeed, dnaBytesCount)));



        // Values used for selecting which parent contributes to the final dna
        uint16 selectParentSeed=uint16(randomSeed % (  2**(genomeGeneStructure.length)  ));
        uint256 mask=1;

        uint newGenomeIndex=0;
        for(uint256 i=0;i<genomeGeneStructure.length;i++){
            uint randomNr=randomSeed%10;

            if(randomNr>RANDOM_DNA_THRESHOLD){
                // This will be a random gene
                for(uint8 j=0;j<genomeGeneStructure[i];j++){
                    newGenomeArray[newGenomeIndex++]=(uint8(randomDnaValues & 0x00FF));
                    randomDnaValues=randomDnaValues>>8;
                }

                // Also advance the mom and dad genomes to align all genes
                momGenes=momGenes >> (genomeGeneStructure[i] * 8);
                dadGenes=dadGenes >> (genomeGeneStructure[i] * 8);
            }else{
                // We must choose whether to pick dad or mom dna
                if(selectParentSeed & mask!=0){
                    // Dad
                    for(uint8 j=0;j<genomeGeneStructure[i];j++){
                        newGenomeArray[newGenomeIndex++]=(uint8(dadGenes & 0xFF));
                        dadGenes=dadGenes>>8;
                    }
                    momGenes=momGenes>> (genomeGeneStructure[i] * 8);
                }else{
                    // Mom
                    for(uint8 j=0;j<genomeGeneStructure[i];j++){
                        newGenomeArray[newGenomeIndex++]=(uint8(momGenes & 0xFF));
                        momGenes=momGenes>>8;
                    }
                    dadGenes=dadGenes >> (genomeGeneStructure[i] * 8);
                }

                // Also advance the artifical dna we created  to align all genes
                randomDnaValues=randomDnaValues >> (genomeGeneStructure[i] * 8);
            }

            randomSeed=randomSeed/10;
            mask=mask<<1;               // Go to next bit
        }

        assert(newGenomeArray.length==dnaBytesCount);

        // Convert to uint256 genome
        uint256 newGenome=0;
        for(uint i=0;i<newGenomeArray.length; i++){
            newGenome = newGenome + (uint256(newGenomeArray[i])<< (i*8));
        }

        return newGenome;
    }

    function createPrimeAvatar() external onlyOwner{
        avatars.push(Human({
            momId:0,
            dadId:0,
            avatarOwner: owner,
            generation:0,
            genome:random()
        }));
        avatarIdsOfAddress[owner].push(avatars.length-1);
    }


    function removeAvatarIdFromMapping(address addr,uint256 avatarId) private{
        bool shift=false;
        for(uint i=0;i<avatarIdsOfAddress[addr].length-1;i++){
            if(avatarIdsOfAddress[addr][i]==avatarId){
                shift=true;
            }

            if(shift) avatarIdsOfAddress[addr][i]=avatarIdsOfAddress[addr][i+1];
        }
        avatarIdsOfAddress[addr].pop();
    }

    function random() private returns (uint) {
        return uint(keccak256(abi.encodePacked(block.difficulty, block.timestamp, randomNonce++)));
    }
    function randomGenome(uint8 size) private returns (uint8[] memory){
        uint8[] memory genome;

        for(uint8 i=0; i<size; i++){
            genome[i]=uint8(random()%256);
        }
        return genome;
    }
}