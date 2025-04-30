using NUnit.Framework;
using Unplants.Scripts.General.Systems.DI;

namespace Unplants.Scripts.Tests.General.Systems.DI
{
    public class DIContainerBase_Tests
    {
        [Test]
        public void DIContainerBase_SimpleBind()
        {
            DIContainer container = new DIContainer();
            container.Bind<DITest>();
            DITest result = container.Resolve<DITest>();
            Assert.NotNull(result);
        }
        
        [Test]
        public void DIContainerBase_BindToBaseClasses()
        {
            DIContainer container = new DIContainer();
            container.Bind<IDITest>().To<DITest>();
            IDITest result = container.Resolve<IDITest>();
            Assert.NotNull(result);
        }

        [Test]
        public void DIContainerBase_InvalidBindingWithoutTypeSpecification()
        {
            DIContainer container = new DIContainer();
            container.Bind<IDITest>();
            Assert.Catch(() => container.Resolve<IDITest>());
        }

        [Test]
        public void DIContainerBase_ExceptionOnInvalidResolve()
        {
            DIContainer container = new DIContainer();
            Assert.Catch(() => container.Resolve<IDITest>());
        }
        
        [Test]
        public void DIContainerBase_DifferentInstancesOnResolve()
        {
            DIContainer container = new DIContainer();
            container.Bind<IDITest>().To<DITest>();
            IDITest result1 = container.Resolve<IDITest>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsFalse(ReferenceEquals(result1, result2));
        }
        
        [Test]
        public void DIContainerBase_CacheSingleInstance()
        {
            DIContainer container = new DIContainer();
            container.Bind<IDITest>().To<DITest>().AsSingle();
            IDITest result1 = container.Resolve<IDITest>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsTrue(ReferenceEquals(result1, result2));
        }
        
        [Test]
        public void DIContainerBase_ResolveForComplexConstructor()
        {
            DIContainer container = new DIContainer();
            container.Bind<IDITest>().To<DITest>().AsSingle();
            container.Bind<DITest2>();
            DITest2 result = container.Resolve<DITest2>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsTrue(result.I == result2.I);
        }
        
        [Test]
        public void DIContainerBase_ResolveForComplexConstructorFromParentContainer()
        {
            DIContainer container1 = new DIContainer();
            DIContainer container2 = new DIContainer(container1);
            container1.Bind<IDITest>().To<DITest>().AsSingle();
            container2.Bind<DITest2>();
            DITest2 result = container2.Resolve<DITest2>();
            Assert.NotNull(result);
        }

        [Test]
        public void DIContainerBase_ResolveRfomInstanceBinding()
        {
            DIContainer container = new DIContainer();
            DITest diTest1 = new DITest();
            container.Bind<DITest>().AsInstance(diTest1);
            DITest diTest2 = container.Resolve<DITest>();
            Assert.IsTrue(diTest2 != null);
        }

        [Test]
        public void DIContainerBase_ResolveRfomInstanceBindingCachingInstance()
        {
            DIContainer container = new DIContainer();
            DITest diTest1 = new DITest();
            container.Bind<DITest>().AsInstance(diTest1);
            DITest diTest2 = container.Resolve<DITest>();
            DITest diTest3 = container.Resolve<DITest>();
            Assert.IsTrue(diTest2 == diTest3);
        }

        [Test]
        public void DIContainerBase_ResolveRfomInstanceBindingCachingOfOriginalInstance()
        {
            DIContainer container = new DIContainer();
            DITest diTest1 = new DITest();
            container.Bind<DITest>().AsInstance(diTest1);
            DITest diTest2 = container.Resolve<DITest>();
            Assert.IsTrue(diTest1 == diTest2);
        }


        #region TestEntities

        public class DITest : IDITest
        {
            public int I => 1;
        }
        
        public class DITest2
        {
            public int I { get; private set; }
        
            public DITest2(IDITest diTest)
            {
                I = diTest.I;
            }
        }
        
        public interface IDITest
        {
            int I { get; }
        }

        #endregion
    }
}
