# A foray into trying to develop a game with similar mechanics to A Short Hike
> The goal of this project is to, over time, implement the core features that small exploration games like A Short Hike use.
> The following is the list of items that I –most likely– will need to implement in order to achieve this.
> 
>  *My primary focus involves the movement controller and the dialogue systems, since these two are foundational to any such game.*

- [x] Standard Movement Controller capable of handling multiple physics scenarios
- [ ] Toggleable control niceties that will make movement feel responsive (main draw Celeste)
- [x] Camera system capable of either following the player or transitioning between fixed positions (Resident Evil or Super Mario Galaxy)
- [ ] Dialogue System that manages different game states and progression
- [ ] Experimental diegetic and non-diegetic UI that will make this dialogue vivid
- [ ] Implementation of an isometric-friendly shader that is stylized in [t3ssel8r's](https://www.youtube.com/@t3ssel8r) style
- [ ] Other features TBD (Enemies, AI, Graphics)

## Movement
When it comes to the movement system, I have always wanted to properly understand the physics & tricks behind satisfying movement. Part of understanding how to do this involved completing the [Catlike Movement Tutorial](https://catlikecoding.com/unity/tutorials/movement/), which goes into an extensive deep dive into using rigidbodies and physics in this context.

Movement encompasses much more than the actual physics tho, and in creating a satisfying movement system, I will try to learn all the tricks that platformers or other relevant games depend on. A while back I came across [Tarodev's Platformer Controller](https://tarodev.itch.io/ultimate-2d-controller)
which elegantly showcased all the crucial features that platformers use. This is my primary source for learning to add all the niceties after having implemented a robust physics controller. Previously, I had always felt frustrated trying to recreate this tutorial, but with the foundations learned from the aforementioned Catlike tutorial, I am excited to see how I can combine the two.

Finally, since neither of the other two points touch on managing technical debt and overall good setups, I am also using [Toyful Game's](https://www.toyfulgames.com/blog/deep-dive-physics) setup for a 3D character in Unity, which has the idea of sitting the player atop a simulated spring to avoid all the nasty issues with corners, ramps, and ground collisions.

## Camera System
Basing a lot of my enthusiasm for my narrative ideas on video games has made me question how I can keep the same visual grandeur you can achieve on film, which really boils down to having control over a scene's composition. As such, a foundational principle of this project is to experiment with
unconventional camera systems that take away some control from the player to improve the visual impact a scene –or, in this case, level– can achieve. 

The main inspiration for these unconventional cameras, from least to most player control, comes from: Resident Evil, A Short Hike, Super Mario Galaxy/Isometric Games. 

Since I've already set up a robust system using Unity's Cinemachine, I plan to iterate over these as I go along the process to see which one fits the game best as it evolves.

## Dialogue
```
 /\_/\
( o.o )
 > ^ <
```

## Shaders
```
    |\__/,|   (`\
  _.|o o  |_   ) )
-(((---(((--------
```

## Enemies and AI
```
           __..--''``---....___   _..._    __
 /// //_.-'    .-/";  `        ``<._  ``.''_ `. / // /
///_.-' _..--.'_    \                    `( ) ) // //
/ (_..-' // (< _     ;_..__               ; `' / ///
 / // // //  `-._,_)' // / ``--...____..-' /// / //
```

## Level Design
```
  ,-.       _,---._ __  / \
 /  )    .-'       `./ /   \
(  (   ,'            `/    /|
 \  `-"             \'\   / |
  `.              ,  \ \ /  |
   /`.          ,'-`----Y   |
  (            ;        |   '
  |  ,-.    ,-'         |  /
  |  | (   |        hjw | /
  )  |  \  `.___________|/
  `--'   `--'
```

---
## Assets
The following assets have been used in the creation of this project:
- [KayKit Asset Pack](https://kaylousberg.itch.io/kaykit-complete)
- [No Nonsense Textures](https://bluwhitebear.itch.io/no-nonsense-grey-box-textures)

## AI-Disclosure
Unless specified in the code, all work is mine

## No AI Training
>Without in any way limiting the author's exclusive rights under copyright, any use of this repository to "train" generative artificial intelligence (AI) technologies to generate text, code, or other content is expressly prohibited. The author reserves all rights to license uses of this work for generative AI training and development of machine learning models.
>This notice constitutes an express reservation of rights under Article 4(3) of Directive (EU) 2019/790.
