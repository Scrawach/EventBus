using EventBus.Abstract;
using Moq;
using NUnit.Framework;

namespace EventBus.Tests
{
    public class EventBusTests
    {
        public class TestEvent { }

        public class ChildTestEvent : TestEvent { }

        [Test]
        public void WhenSubscribeService_ThenShouldListenPublishedEvent()
        {
            // arrange
            var eventBus = new EventBus();
            var service = Mock.Of<IEventListener<TestEvent>>();

            // act
            eventBus.Subscribe(service);
            eventBus.Publish(new TestEvent());

            // answer
            Mock.Get(service).Verify(mock => mock.OnListen(It.IsAny<TestEvent>()));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void WhenPublishEvent_ThenListenerShouldListen_ExactlyAllEvents(int numberOfPublications)
        {
            // arrange
            var eventBus = new EventBus();
            var service = Mock.Of<IEventListener<TestEvent>>();

            // act
            eventBus.Subscribe(service);
            for (var i = 0; i < numberOfPublications; i++)
                eventBus.Publish(new TestEvent());

            // answer
            Mock.Get(service).Verify(mock => mock.OnListen(It.IsAny<TestEvent>()), Times.Exactly(numberOfPublications));
        }

        [Test]
        public void WhenUnsubscribeService_ThenListenerShouldStopListenEvents()
        {
            // arrange
            var eventBus = new EventBus();
            var service = Mock.Of<IEventListener<TestEvent>>();

            // act
            eventBus.Subscribe(service);
            eventBus.Unsubscribe(service);
            eventBus.Publish(new TestEvent());
            eventBus.Publish(new TestEvent());

            // answer
            Mock.Get(service).Verify(mock => mock.OnListen(It.IsAny<TestEvent>()), Times.Never);
        }

        [Test]
        public void WhenServiceListenConcreteEvent_ThenShouldIgnore_EventParentType()
        {
            // arrange
            var eventBus = new EventBus();
            var service = Mock.Of<IEventListener<ChildTestEvent>>();

            // act
            eventBus.Subscribe(service);
            eventBus.Publish(new TestEvent());

            // answer
            Mock.Get(service).Verify(mock => mock.OnListen(It.IsAny<ChildTestEvent>()), Times.Never);
        }

        [Test]
        public void WhenServiceListenConcreteEvent_AndInPublishItCastedToBase_ThenServiceShouldListen()
        {
            // arrange
            var eventBus = new EventBus();
            var service = Mock.Of<IEventListener<ChildTestEvent>>();

            // act
            eventBus.Subscribe(service);
            TestEvent child = new ChildTestEvent();
            eventBus.Publish(child);

            // answer
            Mock.Get(service).Verify(mock => mock.OnListen(It.IsAny<ChildTestEvent>()));
        }
    }
}