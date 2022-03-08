// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.8.12;

contract HumanAvatarOwner {
    address owner;

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
    }

    Human[] avatars;
    mapping(address => Offer[]) public offersMadeByClient;
    mapping(uint256 => Offer[]) public offersForAvatar;
    
    modifier onlyOwner(){
        require(msg.sender == owner);
        _;
    }

    constructor(){
        owner=msg.sender;
    }

    function makeAnOffer(uint256 avatarId,uint256 amount) external{
        offersMadeByClient[msg.sender].push(Offer({
            valid:true,
            futureExpirationTime:block.timestamp+ (1 days),
            avatarId:avatarId,
            amount:amount
        }));
    }


    function createPrimeAvatar() external onlyOwner{
        avatars.push(Human({
            momId:0,
            dadId:0,
            avatarOwner: owner,
            generation:0
        }));
    }


}