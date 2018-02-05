using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirusApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cell = new Cell();
            var cells = cell.GetCells();
            var distance = 0.0;
            var cycle = 0;
            var isInfected = false;
            var message = string.Empty;
            var bloodCell = new Cell();

            Console.WriteLine("Nano Virus Application in progress, please wait...");
            Console.WriteLine("Writing to a file in \"My Documents\" Folder...");

            foreach (var _cell in cells)
            {
                while (cycle < 5)
                {

                    if (cells.Where(c => c.Type == CellType.RedBloodCell).Count() > 0)
                    {
                        bloodCell = cell.GetBloodCell(cells, CellType.RedBloodCell);
                    }
                    else if (cells.Where(c => c.Type == CellType.WhiteBloodCell).Count() > 0)
                    {
                        bloodCell = cell.GetBloodCell(cells, CellType.WhiteBloodCell);
                    }
                    else
                    {
                        break;
                    }

                    if (!isInfected)
                    {
                        foreach (var tumorous in cells.Where(c => c.Type == CellType.TumorousCell))
                        {

                            distance = cell.CalculateDistanceBetweenCells(bloodCell, tumorous);

                            if (distance <= 5000)
                            {
                                if (bloodCell.Type == CellType.TumorousCell)
                                {
                                    //TODO: The remove of Tumorous cell from the list
                                    // cells.Remove(bloodCell);
                                }
                                else
                                {
                                    bloodCell.Type = CellType.TumorousCell;
                                }

                                isInfected = true;
                            }

                            if (cells.Where(c => c.Type == CellType.TumorousCell).Count() == 0)
                            {
                                break;
                            }
                        }
                    }

                    message = "Tumorous cells = " + cells.Where(c => c.Type == CellType.TumorousCell).Count() + "\n Red Blood Cells = " + cells.Where(c => c.Type == CellType.RedBloodCell).Count() + "\n White Blood Cells = " + cells.Where(c => c.Type == CellType.WhiteBloodCell).Count();

                    cell.WriteToTextFile(message);

                    ++cycle;

                    if (cycle == 5)
                    {
                        cycle = 0;
                        message = "\n";
                        isInfected = false;
                    }
                }

            }

            Console.WriteLine("...End of Nano Virus Application...");
            Console.ReadLine();
        }
    }
}
