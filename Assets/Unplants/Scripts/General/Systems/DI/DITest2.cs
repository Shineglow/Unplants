namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DITest2
    {
        public int I { get; private set; }
        
        public DITest2(IDITest diTest)
        {
            I = diTest.I;
        }
    }
}