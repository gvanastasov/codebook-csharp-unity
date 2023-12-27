# Unity CSharp Playbook

## Overview

Welcome to the Unity CSharp Playbook! This project is a hands-on guide designed to help beginners explore C# programming within the Unity engine. It's not a standalone game, but an interactive playbook that provides a practical, engaging way to learn C# and Unity - directly inside Unity Editor itself. Expect to have interaction with console, code and inspector. A good idea is to navigate a bit around Unity before starting this up (aka at least know that Unity is game engine, click around read intro from their website - I might add an interactive guide around the editor since i believe we have interface to tap into the rendering of different windows).

The project uses a single scene, with a GameManager to navigate between different chapters and their subdivisions, referred to as chapter parts. A helpful editor is included in the SceneView to assist with managing CRUD operations. Why single scene? Why not :)

## Getting Started

### Prerequisites

- Unity 2022.3 or later installed on your machine.

### Installation

1. Clone the repo: `git clone https://github.com/gvanastasov/UnityCSharpPlaybook.git`
2. Open the project in Unity.
3. Run the game in editor mode.

### Usage

1. Hit Play!
2. Navigate between chapters and parts using the game navigation.
3. Explore the console, scene, and scripts to learn about C# and Unity.

> **NOTE**: There is some hardcoded logic behind the names of chapters and parts. If you decide to edit them, you risk breaking something. However, experimentation is a great way to learn. To manage chapters and parts, use the `Chapter Manager` which can be toggled via `Window/Custom Tools/Chapter Manager`.

## Content

1. Unity C#
    1.1 Intro
2. Members
    2.1 Variables
    2.2 Operations
    2.3 Methods
3. Control Flow
    3.1 if
    3.2 Switch
4. Collections
    4.1 Arrays
    4.2 Lists
    4.3 Dictionaries
    4.4 Loops
5. Objects
    5.1 Classes
    5.2 Structs
    5.3 Instances
6. Unity Model
    6.1 Object
    6.2 Component
    6.3 Behaviour
    6.4 MonoBehaviour
    6.5 GameObject
    6.6 Extended Behaviour
7. Unity C#
    7.1 The End

## Contributing

We welcome contributions from the community. If you'd like to contribute, please fork the repository and make changes as you'd like. Pull requests are warmly welcome, especially if something seems off, or completely missing it out.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- Unity, for the amazing game engine and development tools.
- The C# language, for being a versatile and powerful language for game development.