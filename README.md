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
