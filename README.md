# SignalBus
Simple signal bus implementation.

## Samples
### Basic
Subscribe another service:

```csharp
ISignalBus bus; // injected

AnotherService service = new AnotherService();
bus.Subscribe(service);
```

Someone publish the signal:

```csharp
ISignalBus bus; // injected 

bus.Publish(new Signal());
```

Another service handles the signal:

```csharp
public class AnotherService : ISignalListener<Signal>
{
  public void OnListen(Signal signal)
  {
    // do some magic
  }
}
```

### Multiple Signals

Service can handles multiple signals:

```csharp
public class SampleService : ISignalListener<Signal1>, ISignalListener<Signal2>
{
  public void OnListen(Signal1 signal)
  {
    // do some magic 1
  }
  
  public void OnListen(Signal2 signal)
  {
    // do some magic 2
  }
}
```

In this case need subscribe every listener:

```csharp
ISignalBus bus; // injected

SampleService service = new SampleService();
bus.Subscribe<Signal1>(service);
bus.Subscribe<Signal2>(service);
```

### Hierarchical Signals

In example, simple Command signal:

```csharp
public class BaseCommand
{
  private readonly ISignalBus _bus;
  
  public BaseCommand(ISignalBus bus) =>
    _bus = bus;
    
  public void DoSomething() =>
    _bus.Publish(this);
}
``

And another one:

```csharp
public class AnotherCommand : BaseCommand
{
  public AnotherCommand(ISignalBus bus) : base(bus) { }
}
```

Handlers can handle `AnotherCommand`:

```csharp
public class Handler : ISignalListener<AnotherCommand>
{
  public void OnListen(AnotherCommand command)
  {
    Console.WriteLine($"{nameof(Handler)} Listen {command}");
  }
}
```

```csharp
ISignalBus bus; // injected

AnotherCommand command = new AnotherCommand(bus);
Handler handler = new Handler();

bus.Subscribe(handler);
command.DoSomething(); // output: Handler Listen AnotherCommand
```
