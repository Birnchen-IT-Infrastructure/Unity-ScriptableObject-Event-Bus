<p align="center">
  <a href="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/releases">
  <img src="https://img.shields.io/github/v/release/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus?style=flat&label=Release&color=0D1345&logo=github&logoColor=DFFB65" alt="Release">
</a>
  <a href="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/issues">
  <img src="https://img.shields.io/github/issues/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus?style=flat&label=Issues&color=0D1345&logo=github&logoColor=DFFB65" alt="Issues">
</a>
  <a href="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/actions/workflows/ci.yml">
  <img src="https://img.shields.io/github/actions/workflow/status/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/ci.yml?style=flat&label=CI&logo=githubactions&logoColor=DFFB65" alt="CI">
</a>
  <a href="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/stargazers">
  <img src="https://img.shields.io/github/stars/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus?style=flat&label=Stars&color=0D1345&logo=github&logoColor=DFFB65" alt="Stars">
</a>
  <a href="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/network/members">
  <img src="https://img.shields.io/github/forks/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus?style=flat&label=Forks&color=0D1345&logo=github&logoColor=DFFB65" alt="Forks">
</a>
</p>

<div align="center">
  <img src="https://github.com/Birnchen-IT-Infrastructure/Unity-ScriptableObject-Event-Bus/blob/main/Docs/banner2.png" alt="Unity ScriptableObject Event Bus" width="900" />
  <br>
  <h1>🍐 Birnchen Event Bus 🍐</h1>
  <p>
    <strong>A lightweight, type-safe event architecture for Unity based on ScriptableObjects.</strong>
  </p>
  <p align="center">
  <a href="https://unity.com/"><img src="https://img.shields.io/badge/Unity-6%20LTS-0D1345?style=flat&logo=unity&logoColor=DFFB65" alt="Unity Version"></a>
  <a href="LICENSE"><img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-0D1345?style=flat&logoColor=0D1345"></a>
  <a href="https://github.com/sponsors/Birnchen-IT-Infrastructure"><img alt="GitHub Sponsors" src="https://img.shields.io/badge/Sponsor-%F0%9F%92%9B-0D1345.svg"></a>    
  <img alt="Made with ❤️" src="https://img.shields.io/badge/Made%20with-%E2%9D%A4-red.svg">
</p>
</div>

A lightweight **Event Bus system for Unity**, built on **ScriptableObjects** to enable clean and decoupled communication between game systems.

No singletons, no hard references — just a simple **publish / subscribe** approach that works across scenes and keeps your architecture modular, maintainable and scalable.

Perfect for UI events, gameplay signals, managers, and any project that benefits from a clean event-driven workflow. 🦖✨🍐

---

## ✨ Features

- ✅ ScriptableObject-based events *(reusable and scene-independent)*
- ✅ Decoupled architecture *(no direct class dependencies)*
- ✅ Lightweight & easy to integrate
- ✅ Inspector-friendly workflow
- ✅ Works across scenes
- ✅ Extendable *(typed events with parameters)*

---

## 📦 Installation

### Option A — Copy into your project ✅
1. Download or clone this repository
2. Copy the folder into your Unity project, for example:
   ```txt
   Assets/YourProject/EventBus/
   ```

### Option B — Unity Package (optional)
If you provide this as a Unity package later, you can also install it via the Unity Package Manager.

---

## 🚀 Quick Start

### 1) Create an Event Asset
Create a new ScriptableObject event in Unity:

- `Right Click → Create → Event Bus → Game Event` *(example name)*

This asset is your global event channel and can be referenced from any system.

---

### 2) Raise the Event (Publisher)
Example: A gameplay system triggers an event.

```csharp
using UnityEngine;

public class ExamplePublisher : MonoBehaviour
{
    public GameEvent onSomethingHappened;

    public void Trigger()
    {
        onSomethingHappened.Raise();
    }
}
```

---

### 3) Listen to the Event (Subscriber)
Example: UI reacts to an event.

```csharp
using UnityEngine;

public class ExampleListener : MonoBehaviour
{
    public GameEvent onSomethingHappened;

    private void OnEnable()
    {
        onSomethingHappened.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        onSomethingHappened.UnregisterListener(OnEventRaised);
    }

    private void OnEventRaised()
    {
        Debug.Log("Event received!");
    }
}
```

---

## 🧠 Typed Events (with Parameters)

If you want to send data alongside events (e.g. an `int`, `float`, `string`, or a custom object), you can extend the system into **typed event channels**.

### Example: `IntEvent`
```csharp
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Bus/Int Event")]
public class IntEvent : ScriptableObject
{
    public event Action<int> Raised;

    public void Raise(int value)
    {
        Raised?.Invoke(value);
    }
}
```

### Example Listener
```csharp
using UnityEngine;

public class ScoreListener : MonoBehaviour
{
    public IntEvent onScoreChanged;

    private void OnEnable()
    {
        onScoreChanged.Raised += OnScoreChanged;
    }

    private void OnDisable()
    {
        onScoreChanged.Raised -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        Debug.Log($"Score is now: {score}");
    }
}
```

---

## 🧩 Use Cases

This Event Bus works great for:

- 🎮 Gameplay triggers *(death, win, checkpoint, quest updates)*
- 🖥️ UI communication *(open/close panels, notifications, popups)*
- 🎵 Audio events *(play sound, change music layer)*
- 💾 Save/Load hooks
- 🌍 Cross-scene messaging
- 🛠️ Modular systems & plugin-friendly architecture

---

## 🗂️ Suggested Folder Structure

```txt
Assets/
└── YourProject/
    └── EventBus/
        ├── Events/
        │   ├── GameEvent.cs
        │   ├── GameEventListener.cs
        │   └── Typed/
        │       └── IntEvent.cs
        ├── Examples/
        │   ├── ExamplePublisher.cs
        │   ├── ExampleListener.cs
        │   └── ScoreListener.cs
        └── README.md
```

---

## 🤝 Contributing

Pull requests are welcome! 🍐  
If you want to contribute, feel free to:

- open an issue
- suggest improvements
- add more typed events (e.g. `FloatEvent`, `StringEvent`, `Vector3Event`)
- provide examples and templates

---

## 💛 Sponsoring

If this project helps you, consider supporting it via **GitHub Sponsors**:

👉 **https://github.com/sponsors/Birnchen-IT-Infrastructure**

Every sponsor helps keeping this project maintained and improving it over time. 🦖✨🍐

---

## 📜 License

This project is licensed under the **MIT License**.  
See the [LICENSE](LICENSE) file for details.
