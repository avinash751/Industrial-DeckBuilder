# READ ME

**Submission For IGO 721**  By Avinash Sanjay Prabhu 

## Important Links 
* [GitHub Repository Link](https://github.com/avinash751/Industrial-DeckBuilder)
* [Game Build Link](https://falmouthac-my.sharepoint.com/:u:/g/personal/ap319638_falmouth_ac_uk/Eby9o-R1Vq1DlBEskrcljsgB1AJcNUk89EVeyp22BnICHA?e=fGhRXb)
* [Game Pitch Video Link](https://falmouth.cloud.panopto.eu/Panopto/Pages/Viewer.aspx?id=7d6fab18-ae47-4f66-9db9-b2ba013d3108)
* [Game Design Document](https://falmouthac-my.sharepoint.com/:w:/g/personal/ap319638_falmouth_ac_uk/EYurDuxiT-pAmEuVFJGKlNgBVCdq4rpGg_5Jic9cBZck6Q?e=6ob6MR)
* [Project Submission One Drive Folder Link](https://falmouthac-my.sharepoint.com/:f:/g/personal/ap319638_falmouth_ac_uk/EsxXSjuu1gZMgiONNI0SW1IBBbFXqe-goDnqQTjhWzkBsw?e=FrBu9Y)

## Table of Contents 
* Game Controls Scheme
    * Camera Controls
    * Card Controls
    * Conveyor Connection Controls
    * Other Controls
* Known Bugs
    * Issue 1: Conveyor Mode Soft Lock
    * Issue 2: Inconsistent Colliders for the Conveyor Belts
    * Issue 3: End and Start Conveyor Points Not Responding When Clicked
* Third Party Assets Used For Development
    * 1. Feel
    * 2. Custom Inspector
* External Font Assets Used
    * 1. Evogria
    * 2. Fragile Bombers
* Tools Used For Development
    * 1. Unity Game Engine (6.0.34f1)
    * 2. Visual Studio 2022 Community Edition
    * 3. Canva
    * 4. Gemini 2.5 Pro Preview
    * 5. Audacity (3.0.0)
    * 6. Coolors
    * 7. GitFork
    * 8. GitHub
* External Audio Assets Used

## Game Controls Scheme

### Camera Controls
* **WASD** keys to pan the camera. [cite: 2]
* **Right Mouse Hold** to edge scroll. [cite: 3]
* **Right Mouse Drag** to drag and move the camera in the dragged direction. [cite: 3]
* Use the **Scroll Wheel** to zoom in and out. [cite: 4]

### Card Controls
* Move the **mouse** over a card pack, Production, or Extraction card to hover over them. [cite: 5]
* **Left Click** on any Extraction or Production card to see a tooltip with more details about its current stats (Useful to see whether the card is active or not and the resources required to make it run). [cite: 6]
* **Left Click Hold** on any bought Card Pack, Extraction Card, or Production Card to drag them around. [cite: 7]
* **Left Click** on any card in the Card Shop to buy a Card Pack (This will only work if you have the available money). [cite: 8]
* **Left Click** on any bought Card Pack to get a random card from the pack. [cite: 9]

### Conveyor Connection Controls
* **Left Click** on any (I) or (O) marked connectors to start a card connection. [cite: 10]
* When making a connection, always connect from an (I) Input connector to an (O) Output marked connector, or connect an (O) Output marked Connector to an (I) Input marked connector for the cards to deliver resources properly. [cite: 11]
* **Left Click** to add a point to your conveyor connection when making the connection. [cite: 12]
* To connect from one connector of a card to another, drag the connection to a connector and **Left Click** to confirm the connection. [cite: 13]
* To disconnect a connection from a card, **Left Click** any connector, drag it away from the connected connector, and **Left Click** again to stop the conveyor connection from following the mouse. [cite: 14]
* You can make an already existing conveyor connection that is not connected follow your mouse by **Left Clicking** its end or start points. [cite: 15]
* **Left Click Hold** on any points of a conveyor connection to move it to a desired position. [cite: 16]
* Press **Z** to undo or Press **Y** to redo. [cite: 17]
* This only works when you are constructing a conveyor belt. [cite: 18]

### Other Controls
* Press the **Esc Key** to Pause and Unpause the game.
* To generate money in the game, connect any (O) Output Marked Connector from a card to any (I) Input marked Connector of an export zone to generate money monthly. [cite: 19]

## Known Bugs

### Issue 1: Conveyor Mode Soft Lock
* **Description:** Fiddling with the endpoints of the conveyor belts too much can occasionally soft lock you, making the conveyor never stop following the mouse. [cite: 20]
* **Fix:** Unfortunately, for this, you need to quit and restart the game (though the game only takes 10 to 15 minutes to finish, so reaching where you were previously shouldn't take too long). [cite: 21]

### Issue 2: Inconsistent Colliders for the Conveyor Belts
* **Description:** This occurs when you try to place a conveyor belt, move a conveyor point, or even move a card to a position, but it shows up as red, denying the placement because there is an invisible collider. [cite: 22]
* **Fix:** Just try to move it to different places near where you want it to be until it does not show as red. [cite: 23]
* This is because there is a fundamental flaw in how colliders are made, and there was not enough time to restructure this logic. [cite: 24]

### Issue 3: End and Start Conveyor Points Not Responding When Clicked 

## Third Party Assets Used For Development 

### 1. Feel
* **Description:** Feel is a ready-to-use solution to provide on-demand game feel to your Unity game, with as little friction or setup as possible[cite: 25]. It’s a modular, user-friendly, and very easy-to-extend system you can build upon[cite: 26]. (MoreMountains, 2021) [cite: 27]
* **What I Have Used It For:** I used this package to add more polish and juice to my game, like adding small animations and text color changes[cite: 27]. I have mainly used it to polish my money system and card shop progression system[cite: 28]. I also used this package and extended it to make my own audio system, which is the primary system I have used to implement all my audio in the game[cite: 29].
* **Reference/Link:** MoreMountains (2021). [Feel | Particles/Effects | Unity Asset Store](https://assetstore.unity.com/packages/tools/particles-effects/feel-183370). [online] assetstore.unity.com. [cite: 30]

### 2. Custom Inspector
* **Description:** Custom Inspector utilizes Attributes (flags) that you can integrate into your C# code to display your properties differently in the Unity Inspector than usual[cite: 31]. (mb services, 2023) [cite: 32]
* **What I Have Used It For:** I used this package to make the inspector for various scripts created by me more easy to read, use, and faster to iterate within the Unity inspector. [cite: 32]
* **Reference/Link:** mb services (2023). [Custom Inspector](https://assetstore.unity.com/packages/tools/utilities/custom-inspector-241058#reviews). [online] UnityAssetStore. [Accessed 8 Apr. 2025] [cite: 33]

## External Font Assets Used

### 1. Evogria
* **Reference/Link:** 7NTypes (2014). [Evogria Font | dafont.com](https://www.dafont.com/evogria.font). [online] Dafont.com. [Accessed 8 Apr. 2025] [cite: 34]

### 2. Fragile Bombers
* **Reference/Link:** Typodermic Fonts (2005). [Fragile Bombers Font | dafont.com](https://www.dafont.com/fragile-bombers.font). [online] Dafont.com. [Accessed 8 Apr. 2025] [cite: 35]

## Tools Used For Development

### 1. Unity Game Engine (6.0.34f1)
* **Description:** This is a cross-platform game engine used to make interactive media that is either 2D or 3D[cite: 36]. It has dedicated tools to allow the development of various games[cite: 37]. (Unity Technologies, 2005) [cite: 38]
* **What I Have Used It For:** This is the main software that I have used to store, develop, and package my game. [cite: 38]
* **Reference/Link:** Technologies, U. (2024). [Real-Time 3D Development Platform & Editor | Unity](https://unity.com/products/unity-engine). [online] unity.com. [cite: 39]

### 2. Visual Studio 2022 Community Edition
* **Description:** It is a fully-featured, extensible, free IDE for creating modern applications for Android, iOS, Windows, as well as web applications and cloud services[cite: 40]. It is also used extensively for game development. (Microsoft, 2014) 
* **What I Have Used It For:** This is the only software I have used for writing all my game code and scripts[cite: 41]. It is also the main software I have used for debugging code[cite: 42].
* **Reference/Link:** Microsoft (2022). [Free IDE and Developer Tools | Visual Studio Community](https://visualstudio.microsoft.com/vs/community/). [online] Visual Studio. [cite: 43]

### 3. Canva
* **Description:** It is a lightweight graphic design editor that has pre-made art elements, graphics, and lightweight tools to make graphic design, presentations, social media posts, and posters easier. [cite: 44]
* **What I Have Used It For:** This is the primary software I have used to make 90% of my art for the game[cite: 45]. These include all the cards, card packs, background art, UI, and some icons present in the game[cite: 46].
* **Reference/Link:** Canva (2013). [Canva](https://www.canva.com/). [online] Canva. [cite: 47]

### 4. Gemini 2.5 Pro Preview
* **Description:** It is a large language model, trained by Google, using the version Gemini 2.5 Pro preview[cite: 48]. As part of the Gemini family, it excels at understanding and generating human-like text for answering questions, summarizing, translating, creative writing, and assisting with various complex language tasks[cite: 49]. (Gemini 2.5 pro preview, 2025) [cite: 50]
* **What I Have Used It For:** I have primarily used it for planning out code for various systems in my game and used it to get better ideas for solutions to complex bugs in the game[cite: 50]. I have also used it for validating code architecture. I have used it heavily for validating my conveyor connection system in the game[cite: 51].
* **Reference/Link:** Google (2025). [Gemini Pro](https://deepmind.google/technologies/gemini/pro/). [online] Google DeepMind. [cite: 52]

### 5. Audacity (3.0.0)
* **Description:** It is a free, open-source audio editing and audio recording DAW, used to edit music, sound effects, and anything audio-related[cite: 53]. It has a bunch of audio tools and pre-made effects to get the desired output needed.
* **What I Have Used It For:** This is the only software I have used to edit audio and make modified sound effects out of pre-existing ones[cite: 55]. So all sounds that you hear in the game have been edited and modified by me using the Audacity software
* **Reference/Link:** Audacity (2022). [Audacity | free, open source, cross-platform audio software for multi-track recording and editing.](https://www.audacityteam.org/). [online] Audacityteam.org. [Accessed 8 Apr. 2025] [cite: 57]

### 6. Coolors
* **Description:** It is a free tool to generate various color palettes and store them for future use. [cite: 58]
* **What I Have Used It For:** I used it to generate a global color palette for my game to use for all my art and UI in the game. [cite: 59]
* **Reference/Link:** Bianchi, F. (2018). [Coolors](https://coolors.co/). [online] Coolors.co. [cite: 60]

### 7. GitFork
* **Description:** It is a free tool to use the Git client in an easy-to-use, friendly way, instead of using the Git terminal[cite: 61]. Very good for managing merge conflicts[cite: 62].
* **What I Have Used It For:** I have used it as a GUI Git client for my entire project[cite: 62]. It has been very useful to make backup branches easily and merge my work easily[cite: 63]. It also makes it easy to see all my commits in a nice tree format[cite: 64]. I also used it often to go back to previous commits to see how working logic could work in a code refactor[cite: 65].
* **Reference/Link:** Pristupov, D. and Pristupova, T. (n.d.). [Fork - a fast and friendly git client for Mac and Windows](https://git-fork.com/). [online] Fork - a fast and friendly git client for Mac and Windows. [cite: 66]

### 8. GitHub
* **Description:** It is a place where developers store their repositories with code and track commits using the Git client under the hood. [cite: 68]
* **What I Have Used It For:** I have used it for my entire project to back it up in the cloud via GitHub[cite: 69]. Used Git Fork to interact with GitHub and to push and pull local commits to GitHub[cite: 70].
* **Reference/Link:** GitHub (2008). [GitHub](https://github.com/). [online] GitHub. [cite: 70]

## External Audio Assets Used
* Nelan, B. (2015). [Door Locked by BenjaminNelan](https://freesound.org/people/BenjaminNelan/sounds/321087/). [online] Freesound. [cite: 71]
* phvm, J. (2023). [Blocked Page](https://freesound.org/people/josephvm/sounds/670024). [online] Freesound.org. [Accessed 8 Apr. 2025] [cite: 72]
* Stage, N. (2013). [Steam 21 by nicStage](https://freesound.org/people/nicStage/sounds/197352). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 72]
* RobinHood76 (2016). [06531 percussive blocked ding.wav by Robinhood76](https://freesound.org/people/Robinhood76/sounds/342131). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 73]
* falcospizaetus (2019). [TearingPaper04 by falcospizaetus](https://freesound.org/people/falcospizaetus/sounds/489961). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 74]
* williamornelas (2020). [Coins / Selling sound by williamornelas](https://freesound.org/people/williamornelas/sounds/525147). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 75]
* Nita6 (2022). [Bus card recharge machine by Nita6](https://freesound.org/people/Nita6/sounds/620189). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 76]
* theplax. (2023). [Cash Machine.wav](https://freesound.org/people/theplax/sounds/692746). [Online Audio]. Freesound. [Accessed 8 April 2025] [cite: 77]
* Fupicat. (2020). [WinMutedGuitar.wav](https://freesound.org/people/Fupicat/sounds/521646/). [Online Audio]. Freesound. [Accessed 8 April 2025] [cite: 77]
* florianreichelt. (2017). [Fail Sound Effect - Accoustic Guitar](https://freesound.org/people/florianreichelt/sounds/412427/). [Online Audio]. Freesound. [Accessed 8 April 2025] [cite: 78]
* Fupicat (2020). [WinFretless.wav by Fupicat](https://freesound.org/people/Fupicat/sounds/521644/). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 79]
* qubodup (2024). [Mechanism Activation Sequence by qubodup](https://freesound.org/people/qubodup/sounds/752067/). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 80]
* Michael Ghelfi Studios (2020). [Engineer’s Office | Steampunk Ambience | 1 Hour](https://youtu.be/8ceMOedFfbw). [online] Youtu.be. [Accessed 8 Apr. 2025] [cite: 81]
* Garuda1982 (2024). [Old industrial district with river city atmosphere by Garuda1982](https://freesound.org/people/Garuda1982/sounds/732130). [online] Freesound. [Accessed 8 Apr. 2025] [cite: 82]
* ChillSoul (2025). [Combination Between Funk & Jazz Creates Vibrant Melody 🎵 Top Best Funky Jazz Albums Worth Listening](https://youtu.be/1JcPNGZmzuM). [online] Youtube. [Accessed 8 Apr. 2025] [cite: 83, 84]

END
