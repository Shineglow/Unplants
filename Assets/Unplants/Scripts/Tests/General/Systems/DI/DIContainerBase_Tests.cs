using NUnit.Framework;
using Unplants.Scripts.General.Systems.DI;

namespace Unplants.Scripts.Tests.General.Systems.DI
{
    public class DIContainerBase_Tests
    {
        [Test]
        public void DIContainerBase_SimpleBind()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<DITest>();
            DITest result = container.Resolve<DITest>();
            Assert.NotNull(result);
        }
        
        [Test]
        public void DIContainerBase_BindToBaseClasses()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<IDITest>().To<DITest>();
            IDITest result = container.Resolve<IDITest>();
            Assert.NotNull(result);
        }

        [Test]
        public void DIContainerBase_InvalidBindingWithoutTypeSpecification()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<IDITest>();
            Assert.Catch(() => container.Resolve<IDITest>());
        }

        [Test]
        public void DIContainerBase_ExceptionOnInvalidResolve()
        {
            DIContainerBase container = new DIContainerBase();
            Assert.Catch(() => container.Resolve<IDITest>());
        }
        
        [Test]
        public void DIContainerBase_DifferentInstancesOnResolve()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<IDITest>().To<DITest>();
            IDITest result1 = container.Resolve<IDITest>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsFalse(ReferenceEquals(result1, result2));
        }
        
        [Test]
        public void DIContainerBase_CacheSingleInstance()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<IDITest>().To<DITest>().AsSingle();
            IDITest result1 = container.Resolve<IDITest>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsTrue(ReferenceEquals(result1, result2));
        }
        
        [Test]
        public void DIContainerBase_ResolveForComplexConstructor()
        {
            DIContainerBase container = new DIContainerBase();
            container.AddBinding<IDITest>().To<DITest>().AsSingle();
            container.AddBinding<DITest2>();
            DITest2 result = container.Resolve<DITest2>();
            IDITest result2 = container.Resolve<IDITest>();
            Assert.IsTrue(result.I == result2.I);
        }
        
        [Test]
        public void DIContainerBase_ResolveForComplexConstructorFromParentContainer()
        {
            DIContainerBase container1 = new DIContainerBase();
            DIContainerBase container2 = new DIContainerBase(container1);
            container1.AddBinding<IDITest>().To<DITest>().AsSingle();
            container2.AddBinding<DITest2>();
            DITest2 result = container2.Resolve<DITest2>();
            Assert.NotNull(result);
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
