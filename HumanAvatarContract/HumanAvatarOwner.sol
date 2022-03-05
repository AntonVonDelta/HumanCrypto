// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.8.12;

contract HumanAvatarOwner {
    address owner;

    Human avatars[];

    struct Human{
        uint32 mom;
        uint32 dad;

        address owner;
        uint16 generation;
    }

    modifier onlyOwner(){
        require(msg.sender == owner);
        _;
    }

    constructor(){
        owner=msg.sender;
    }

    function createPrimeAvatar() external onlyOwner{
        avatars.push(Human({
            mom:0,
            dad:0,

            owner: owner,
            generation:0
        }));
    }

    function getAvatar(uint amount, address target) public
    {
        // Only allowed to the owner
        if(msg.sender!=_owner) return;

        // Do not request more money than available
        if(amount>address(this).balance) return;

        (bool success, ) = target.call{value:amount}("");
        require(success, "Transfer failed.");
    }

    function () public payable {
        revert (); 
    }   
}