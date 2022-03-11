// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.8.12;

contract HumanAvatarOwner {

    struct Offer{
        bool valid;
        uint256 futureExpirationTime;
        uint256 avatarId;
        uint256 amount;
    }
    struct Human{
        uint32 momId;
        uint32 dadId;

        address avatarOwner;
        uint16 generation;

        uint8[] genome;
    }

    address owner;
    uint randomNonce=0;
    Human[] public avatars;
    mapping(address => Offer[]) public offersMadeByClient;
    mapping(uint256 => Offer[]) public offersForAvatar;
    uint8[] genomeGeneStructure=[4, 4, 4, 4, 4, 4, 4, 4 ];


    modifier onlyOwner(){
        require(msg.sender == owner);
        _;
    }

    constructor(){
        owner=msg.sender;
    }

    function makeAnOffer(uint256 avatarId,uint256 amount) external{
        require(avatarId>=avatars.length,"Avatar not found");
        require(amount==0,"No zero amount offer allowed");

        Offer memory newOffer=Offer({
            valid:true,
            futureExpirationTime:block.timestamp+ (1 days),
            avatarId:avatarId,
            amount:amount
        });

        offersMadeByClient[msg.sender].push(newOffer);
        offersForAvatar[avatarId].push(newOffer);
    }


    function createPrimeAvatar() external onlyOwner{
        avatars.push(Human({
            momId:0,
            dadId:0,
            avatarOwner: owner,
            generation:0,
            genome: randomGenome()
        }));
    }


    function random() private returns (uint) {
        return uint(keccak256(abi.encodePacked(block.difficulty, block.timestamp, randomNonce++)));
    }
    function randomGenome() private returns (uint8[] memory){
        uint8[] memory genome;
        for(uint256 i=0; i<genome.length; i++){
            genome[i]=uint8(random()%256);
        }
        return genome;
    }
}