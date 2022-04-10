
namespace PageReplacements
{
    class LRU : IPage
    {
        private int countInterruption;
        public int CountInterruption
        {
            get
            {
                return countInterruption;
            }
        }

        private int[,] pageBlocks;

        public LRU(int countPageBlock, int[] startPageBloks = null)
        {
            pageBlocks = new int[countPageBlock, 2];

            for (int i = 0; i < pageBlocks.Length / 2; i++)
            {
                if ((startPageBloks != null) && (i < startPageBloks.Length))
                {
                    pageBlocks[i, 0] = startPageBloks[i];
                    pageBlocks[i, 1] = (int)IPage.FLAG.HAVE_PAGE;
                }
                else
                {
                    pageBlocks[i, 1] = (int)IPage.FLAG.NO_PAGE;
                }
            }
        }

        public bool SetPageReference(int newPage)
        {
            int indexLastBlock = pageBlocks.Length / 2 - 1;

            while (indexLastBlock >= 0 && pageBlocks[indexLastBlock, 1] == (int)IPage.FLAG.NO_PAGE)
            {
                indexLastBlock--;
            }

            if (indexLastBlock < (pageBlocks.Length / 2 - 1))
            {
                indexLastBlock++;
                countInterruption++;
            }

            for (int i = 0; i < pageBlocks.Length / 2; i++)
            {
                if (pageBlocks[i, 0] == newPage)
                {
                    for (int j = i; j < pageBlocks.Length / 2 - 1; j++)
                    {
                        pageBlocks[j, 0] = pageBlocks[j + 1, 0];
                        pageBlocks[j, 1] = pageBlocks[j + 1, 1];
                    }

                    pageBlocks[indexLastBlock, 0] = pageBlocks[i, 0];
                    pageBlocks[indexLastBlock, 1] = pageBlocks[i, 1];

                    return false;
                }
            }

            countInterruption++;

            for (int j = 0; j < pageBlocks.Length / 2 - 1; j++)
            {
                pageBlocks[j, 0] = pageBlocks[j + 1, 0];
                pageBlocks[j, 1] = pageBlocks[j + 1, 1];
            }

            pageBlocks[0, 0] = newPage;
            pageBlocks[0, 1] = (int)IPage.FLAG.HAVE_PAGE;

            return true;
        }

        public int[,] GetBlocks()
        {
            return pageBlocks;
        }
    }
}
