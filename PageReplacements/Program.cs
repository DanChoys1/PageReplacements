using System;

namespace PageReplacements
{
    class Program
    {
        static readonly int[] references = { 7, 8, 9, 2, 1, 0, 8, 9, 2, 4, 6, 8, 2, 1, 8, 9};
        static readonly int[] startPageBloks = { 8, 2, 9, 6 };

        static void Main(string[] args)
        {
            FIFO fifo4 = new FIFO(4, startPageBloks);
            Console.WriteLine("FIFO - 4: startPageBlocks - 8, 2, 9, 6");
            PrintFifo(fifo4);

            FIFO fifo5 = new FIFO(5, startPageBloks);
            Console.WriteLine("FIFO - 5: startPageBlocks - 8, 2, 9, 6, -");
            PrintFifo(fifo5);

            LRU lru4 = new LRU(4, startPageBloks);
            Console.WriteLine("LRU - 4: startPageBlocks - 8, 2, 9, 6");
            PrintFifo(lru4);

            LRU lru5 = new LRU(5, startPageBloks);
            Console.WriteLine("LRU - 5: startPageBlocks - 8, 2, 9, 6, -");
            PrintFifo(lru5);
        }

        static void PrintFifo(IPage page)
        {
            for (int j = 0; j < references.Length; j++)
            {
                bool interruption = page.SetPageReference(references[j]);

                Console.Write((j + 1).ToString() + ") ");

                if (interruption)
                {
                    Console.Write("X ");
                }
                else
                {
                    Console.Write("  ");
                }

                int[,] pageBloks = page.GetBlocks();

                for (int i = 0; i < pageBloks.Length / 2; i++)
                {
                    if (pageBloks[i, 1] != (int)IPage.FLAG.NO_PAGE)
                    {
                        Console.Write(pageBloks[i, 0].ToString() + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
