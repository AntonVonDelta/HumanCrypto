// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.8.12;

contract HumanAvatarOwner {
    struct Offer{
        bool active;
        uint256 avatarId;
        uint256 amount;
    }
    struct Human{
        uint32 momId;
        uint32 dadId;

        address avatarOwner;
        uint16 generation;
        uint256 genome;
    }

    address owner;
    uint randomNonce=0;

    Human[] public avatars;
    mapping(uint256 => Offer) public avatarOffer;               // Only the owner can post an ofer for the avatar
    uint8[] genomeGeneStructure=[4, 4, 4, 4, 4, 4, 4, 4 ];

    modifier onlyOwner(){
        require(msg.sender == owner);
        _;
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


        uint256 refund=msg.value-avatarOffer[avatarId].amount;

        prevOwner.transfer(avatarOffer[avatarId].amount);
        payable(msg.sender).transfer(refund);
    }

    function createPrimeAvatar() external onlyOwner{
        avatars.push(Human({
            momId:0,
            dadId:0,
            avatarOwner: owner,
            generation:0,
            genome:random()
        }));
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