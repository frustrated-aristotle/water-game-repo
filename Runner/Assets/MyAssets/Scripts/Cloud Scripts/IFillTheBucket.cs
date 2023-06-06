namespace HyperCasual.Runner
{
    public interface IFillTheBucket
    {
        /// <summary>
        /// Fills the bucket's capacity
        /// </summary>
        public void FillTheBucket(Inventory inventory,float rate);

        /// <summary>
        /// Player gets the expected effect
        /// </summary>
        public void TakeTheEffect();
    }
}