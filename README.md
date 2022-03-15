# HumanCrypto

# Build
Build using Visual Studio 2019 and Nethereum packages.

# How to run
 1. Run the executable
 2. Select an account (initially all accounts are empty)
 3. Go to the settings page and paste all the necesary information - private keys, Infura API Key, etc.
 4. Select your wanted MaxPriorityFee which will be used for all in-app transactions
 5. **Save settings**
 6. Restart program in order to apply the changes

## Layout
- There are 3 tabs: Testing, All Avatars and Settings.
- The `Testing` tab is only used for testing the Picasso Image generator locally.
- The `All Avatars` tab is the main tab of the game.

## Picasso Generator
The Picasso generator uses the `data.xml` file inside the `HumanParts` folder. This is a very powerful generator as it is very customizable, uses svg for defining the shape of the components and allows extensions: any structure can be built only by editing the xml file.

The structure is very simple; it's primarily used to define the structure and functions of the genome. It orders the information in components of the face then for each component there are subcomponents defined with their positions inside the parent structure. By traversing the structure the avatar can be built.

Every component has a centre position defined as well as the graphics element which is a svg path. 

## Game
 - `Window 1` contains all avatars created on the network
 - `Window 2` contains the avatars you bought
 - `Window 3` will show the parents of selected avatar from `Window 1`
 
 By clicking an avatar from `Window 1` you can see:
  - if an offer is available and the amount for which you can buy it.
  - the generation
  - its parents in `Window 3`
  - you can also buy the offer if one is available

By clicking an avatar from `Window 2` you can see:
  - the current offer price
  - you can also create an offer or cancel one
  - **you can breed two of your own avatars by clicking one** , wait for it to be selected, and then Shift-clicking another one. Wait for a notification of confimation

**Note:** Most actions do have a notification system. Wait for a notification to inform you of the change or error before reattempting the transaction aka deploy, createPrimeAvatar, make offer, etc. 

![image](https://user-images.githubusercontent.com/25268629/158376750-e57e98d3-d8ff-4032-86a5-8a57fcaaf2a9.png)


## Settings
In this tab you can change all important settings.

Click save before closing the tab

**Note:** The settings are **not** saved in the local folder but in %APPDATA%\Local. This means you can safely share the project with the compilled executable without leaking private keys or API keys.


![image](https://user-images.githubusercontent.com/25268629/158257005-eb1f0bec-474c-41f4-835f-f830c0fe5486.png)
