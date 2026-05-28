## 1. Core Gameplay Loop
The game is a **2D top-down action RPG** centred around a wizard character. The primary mechanic involves typing sequences of runes to manifest spells.

### Rune Input & Consumption
*   **Input Method:** Players use specific key bindings for each rune (e.g., $Q, W, E, R$ for elements).
*   **Immediate Depletion:** Runes are consumed **immediately upon being typed**, not when the spell is cast. 
    *   *Consequence:* Incorrect sequences result in the permanent loss of those runes until a checkpoint is reached.
*   **Resource Management:** Runes have limited uses. Once exhausted, they cannot be used again until the player rests at a **checkpoint** to refresh all rune counts.

---

## 2. The Spell Casting Logic
The system utilizes a priority-based resolution engine to interpret rune sequences.

### Step 1: Base Spell Identification (The Rule System)
A central `SpellResolver` scans the typed sequence against a set of decoupled **Rules**.
*   **Rule Structure:** Each rule contains a `Priority`, a `Name`, and a `Valid Sequence`.
*   **Resolution:** The system identifies the **highest priority valid rule** that matches the current input to determine the **Base Spell**.

### Step 2: Modifier Application
Once a Base Spell is identified, the system scans the remaining runes in the current sequence for valid modifiers.
*   **Validation:** Every Base Spell has a predefined list of allowed modifiers (e.g., `Fireball` can accept `Wind` for piercing, but `Shield` cannot).
*   **Modular Design:** Spells are constructed dynamically by combining the identified Base Spell with any valid subsequent runes found in the buffer.

---

## 3. Spell Classifications

Spells are categorized into three distinct types based on their behavior and targeting logic:

| Type | Description | Example |
| :--- | :--- | :--- |
| **Internal** | Centered on the caster; affects the player or an immediate aura. | `Heal`, `Ward`, `Detonante` |
| **Projected** | Cast in a specific direction; involves projectiles, beams, or arcs. | `Fireball`, `Laser`, `Wind Blast` |
| **Special** | Unique, high-impact effects that do not follow standard projectile/aura logic. | `Raise Zombies`, `Illuminate` |

---

## 4. Component Breakdown

### Elements & Cores
*   **Elemental Runes:** Fire (Q), Wind (W), Earth (E), Lightning (R)
*   **Core Runes:** Shield (A), Ward (S), Heal (D)
*   **Special Runes:** Dark (Z), Holy (X), Eldritch (C)

### Status Effects & Modifiers
Status effects are managed via an `Update` loop to handle duration and periodic damage (ticks).
*   **Examples of Modifiers:**
    *   **Piercing (+Wind):** Allows projectiles to pass through the first target.
    *   **Burning (+Fire):** Applies a DoT (Damage over Time) effect.
    *   **Bounce (+Ward):** Causes projectiles to reflect off targets.

---

## 5. Technical Architecture Summary
The system is designed for **Data-Driven Scalability**:
1.  **Decoupled Rules:** New spells can be added by creating new `Rule` entries without altering the core parsing logic.
2.  **Dynamic Progression:** The player unlocks new runes (e.g., moving from Fire to Holy) as they progress through the game.
3.  **Event-Driven UI:** The system exposes events for rune usage and depletion, allowing the UI to track `Max Uses` vs. `Current Uses` without tight coupling to the gameplay logic.