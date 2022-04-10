
namespace PageReplacements
{
    interface IPage
    {
        public enum FLAG
        {
            HAVE_PAGE,
            NO_PAGE,
        }

        public bool SetPageReference(int newPage);
        public int[,] GetBlocks();
    }
}
