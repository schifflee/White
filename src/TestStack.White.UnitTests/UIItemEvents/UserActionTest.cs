using NSubstitute;
using NUnit.Framework;
using TestStack.White.UIItemEvents;
using TestStack.White.UIItems;

namespace TestStack.White.UnitTests.UIItemEvents
{
    [TestFixture]
    public class UserActionTest
    {
        private readonly UserAction userAction;

        public UserActionTest()
        {
            userAction = new UserAction();
        }

        private static T UIItem<T>(string id) where T : class, IUIItem
        {
            var t = Substitute.For<T>();
            t.PrimaryIdentification.Returns(id);
            return t;
        }

        [Test]
        public void IsNotRepeatEventWhenRegisteringFirstEvent()
        {
            userAction.Register(new UIItemClickEvent(UIItem<Button>("cb")));
            Assert.That(userAction.RepeatEvent, Is.False);
        }

        [Test]
        public void IsNotRepeatEventWhenRegisteringDifferentEvent()
        {
            userAction.Register(new UIItemClickEvent(UIItem<Button>("cb")));
            userAction.Register(new UIItemClickEvent(UIItem<Button>("cb")));
            Assert.That(userAction.RepeatEvent, Is.False);
        }

        [Test]
        public void RepeatEventWhenRegisteringSameEvent()
        {
            userAction.Register(new TextBoxEvent(UIItem<TextBox>("cb")));
            userAction.Register(new TextBoxEvent(UIItem<TextBox>("cb")));
            Assert.That(userAction.RepeatEvent, Is.True);
        }
    }
}