# Event Service

Generic type-based event dispatch service for Unity projects.

## Installation

Add the package via the Unity Package Manager using a git URL:

```
https://github.com/WendellLeao/event-service.git
```

To pin a specific version, append `#v1.0.0` (or any tag) to the URL.

Depends on [WendellLeao.ServiceLocator](https://github.com/WendellLeao/service-locator).

## Usage

1. Create a `GameEvent` subclass for each event type you want to dispatch.
2. Add an `EventService` component to a persistent GameObject.
3. Subscribe, dispatch and unsubscribe through `IEventService`.

```csharp
using WendellLeao.Events;
using WendellLeao.ServiceLocator;

IEventService eventService = Locator.Get<IEventService>();

eventService.AddEventListener<PlayerDiedEvent>(OnPlayerDied);

eventService.DispatchEvent(new PlayerDiedEvent());

eventService.RemoveEventListener<PlayerDiedEvent>(OnPlayerDied);
```

`EventService` registers itself as `IEventService` on `Awake` and unregisters on `OnDestroy`.
