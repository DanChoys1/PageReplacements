
namespace PageReplacements
{
    class FIFO : IPage
    {
        private int countInterruption;
        public int CountInterruption 
        { 
            get 
            { 
                return countInterruption;
            } 
        }
        //
        private int[,] pageBlocks;

        public FIFO(int countPageBlock, int[] startPageBloks = null)
        {
            pageBlocks = new int[countPageBlock, 2];

            for (int i = 0; i  < pageBlocks.Length / 2; i++)
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
            for (int i = 0; i < pageBlocks.Length / 2; i++)
            {
                if (pageBlocks[i, 0] == newPage)
                {
                    return false;
                }
            }

            countInterruption++;

            int indexFirsteNoPageBlock = pageBlocks.Length / 2 - 1;

            while (indexFirsteNoPageBlock >= 0 && pageBlocks[indexFirsteNoPageBlock, 1] == (int)IPage.FLAG.NO_PAGE)
            {
                indexFirsteNoPageBlock--;
            }

            if (indexFirsteNoPageBlock < (pageBlocks.Length / 2 - 1))
            {
                indexFirsteNoPageBlock++;

                pageBlocks[indexFirsteNoPageBlock, 0] = newPage;
                pageBlocks[indexFirsteNoPageBlock, 1] = (int)IPage.FLAG.HAVE_PAGE;
            }
            else
            {
                for (int i = 0; i < pageBlocks.Length / 2 - 1; i++)
                { 
                    pageBlocks[i, 0] = pageBlocks[i + 1, 0];
                    pageBlocks[i, 1] = pageBlocks[i + 1, 1];
                }

                pageBlocks[indexFirsteNoPageBlock, 0] = newPage;
            }

            return true;
        }
    
        public int[,] GetBlocks()
        {
            return pageBlocks;
        }
    }
}
