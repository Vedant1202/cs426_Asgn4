# cs426_Asgn2Group
 
Team members - Adrian Knight, Hiba Mirza, and Vedant Nandoskar.

Brief game idea: One player is a Cyber Cop, and the others are Hackers (Thieves) attempting to exploit a computer system. Hackers must steal data from cache, RAM, and ALU while avoiding getting caught, and the Cyber Cop must 'shoot' them before they complete their heist.

Player Interaction Pattern: Unilateral competition

Objective: Capture/Chase/Escape
 
Serious Objective: Teach players about memory architecture (cache, RAM, ALU) and how cyber attacks like stack/buffer overflows work. Show how system monitoring and security measures can prevent cyber threats.

Procedures:
Hackers can:
- Move quickly across the map using WASD controls
- Steal data from Cache, RAM, and ALU
- Escape after stealing enough data
Cyber Cop can:
- Patch vulnerabilities to temporarily block access to memory areas
- 'Shoot' hackers to remove them from the game

Rules:
- Hackers must collect a set amount of data before escaping.
- If a hacker steals too much from the same location too quickly, it triggers a stack/buffer overflow, alerting the Cyber Cop.
- Hackers can use fake logs to confuse the Cyber Cop, but using them too often lowers their effectiveness.
- The Cyber Cop wins if all hackers are shot at before they can escape.
- The Hackers win if at least one hacker survives and escapes after collecting the required data.

- Resources:
- Memory Locations: Cache, RAM, ALU (where data can be stolen)
- Patch Tool: The Cyber Cop can temporarily block memory locations

Non-plain-vanilla procedure/rule (see the Hw02 handout for what would qualify as an unusual procedure/rule): Hackers can be multiple and can infect packages.

Test Question for Serious Objective: How does getting data from the cache compare to getting data from memory (RAM)?

Expected Correct Answer for Serious Objective: The cache is smaller but much faster to access, while RAM is larger but slower. Attackers usually target the cache for quick data access or overload RAM to cause stack/buffer overflows.
