# EventBus
Simple event bus implementation.

## Samples
### Basic
Subscribe another service:

```csharp
IEventBus bus; // injected

AnotherService service = new AnotherService();
bus.Subscribe(service);
```

Someone publish the event:

```csharp
IEventBus bus; // injected 

bus.Publish(new EventA());
```

Another service handles the event:

```csharp
public class AnotherService : IEventListener<EventA>
{
  public void OnListen(EventA args)
  {
    // do some magic
  }
}
```

### Multiple Events

Service can handles multiple events:

```csharp
public class SampleService : IEventListener<EventA>, IEventListener<EventB>
{
  public void OnListen(EventA args)
  {
    // do some magic 1
  }
  
  public void OnListen(EventB args)
  {
    // do some magic 2
  }
}
```

In this case need subscribe every listener:

```csharp
IEventBus bus; // injected

SampleService service = new SampleService();
bus.Subscribe<EventA>(service);
bus.Subscribe<EventB>(service);
```

### Hierarchical Events

In example, simple Command event:

```csharp
public class BaseCommand
{
  private readonly IEventBus _bus;
  
  public BaseCommand(IEventBus bus) =>
    _bus = bus;
    
  public void DoSomething() =>
    _bus.Publish(this);
}
```

And another one:

```csharp
public class AnotherCommand : BaseCommand
{
  public AnotherCommand(IEventBus bus) : base(bus) { }
}
```

Handlers can handle `AnotherCommand`:

```csharp
public class Handler : IEventListener<AnotherCommand>
{
  public void OnListen(AnotherCommand command)
  {
    Console.WriteLine($"{nameof(Handler)} Listen {command}");
  }
}
```

```csharp
IEventBus bus; // injected

AnotherCommand command = new AnotherCommand(bus);
Handler handler = new Handler();

bus.Subscribe(handler);
command.DoSomething(); // output: Handler Listen AnotherCommand
```
