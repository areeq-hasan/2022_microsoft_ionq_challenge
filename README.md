# SOULINQ

## The Idea

SOULINQ A two-player 2D platformer where players with entangled souls must navigate a world bombarded by *cosmic rays* attempting to observe their souls. The world is rife with *inner fire* capable of evolving their shared soul state. The goal is to be observed as alive.

### Player Mechanics
The two players spawn in a jungle world, procedurally generated using quantum randomness, where they can move around using `<WASD>`, jump using `<SPACE>`, and attack using the left-mouse button. The souls of the two players are represented as qubits in a 2-qubit quantum circuit, where $|0\rangle$ represents death and $|1\rangle$ represents life. Their shared soul state is initialized to a state $\frac1{N}(\alpha|00\rangle+\beta|11\rangle)$ such that $\alpha\gg\beta$; specifically $\alpha=0.9$ and $\beta=\sqrt{1-0.9^2}\approx0.436$. In this state, being observed by a cosmic ray would collapse both souls to the death state with high probability.

### Inner Fire Mechanics (1QB Gates)
Players can evolve their soul state by consuming inner fire, which are scattered across the map in a distribution generated using quantum randomness. Consuming inner fire acts the quantum gate denoted above the item on the player's soul. There are five rarities of inner fire:
1. Common — Gray (46%) {Rx(theta) where $0 < |\theta| < \frac\pi8$}
3. Uncommon — Green (30%) {Rx(theta) where $\frac\pi8 \leq |\theta| < \frac\pi4$}
4. Rare — Orange (15%) {Rx(theta) where $\frac\pi4 \leq |\theta| < \frac\pi2$}
5. Epic — Purple (8%) {Z, H}
6. Legendary — Gold (1%) {X, Y}

### Combat Mechanics (2QB Gates)
If the players attack each other, an RX gate controlled on the offender's soul and targetted on the defender's soul is enacted on the shared soul system. The damage dealt i.e. the angle of rotation is proportional to the offender's probability of being measured alive, and the defender has a defense modifier proportional to the defender's probability of being measured alive.

An additional weapon exists on the map that can be used once per game to act a SWAP gate on the shared soul system, effectively swapping the souls of the players.

### Cosmics Rays (Measurement)
Cosmic rays spawn from the sky and chase the players. Their frequency is weighted by quantum randomness, and, as the game progresses, they move faster and track the player more accurately. When a cosmic ray comes in contact with a player, their soul is observed and its possible superposition collapses to either dead or alive. The end-game condition is the collapse of both players' souls, and a player wins if their souls is observed as alive. Note that the collapse of a single player's soul is not sufficient to determine their final state since the other player can resurrect/kill them by applying 2QB gates.

