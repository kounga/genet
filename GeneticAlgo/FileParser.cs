using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo
{
	class FileParser
	{
		public List<List<float>> data = new List<List<float>>();
        public List<List<double>> normalizedData = new List<List<double>>();
		public List<float> maximums = new List<float>();
		public List<float> minimums = new List<float>();
		String fileName = "";

		public FileParser(String fileName)
		{
			this.fileName = fileName;
		}

		public void Parse()
		{
            Console.WriteLine("READING FILE------------------------------------");
            Console.WriteLine("0 lines read.");
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
					line = line.Replace("\"", "");
                    List<String> values = line.Split(',').ToList<String>();
                    List<float> numValues = new List<float>();
                    List<double> normNumValues = new List<double>();
                    bool skipLine = false;
					System.Threading.Thread.Sleep(2);


                    foreach (var val in values)
                    {
                        if (float.TryParse(val, out var num))
                        {
                            numValues.Add(num);
                        }
                        else
                        {
                            skipLine = true;
                        }
                    }
                    if (skipLine) {
						continue;
					}
                    data.Add(numValues);
                    if (data.Count % 100 == 0)
                    {
						Console.SetCursorPosition(0, Console.CursorTop - 1);
						ClearCurrentConsoleLine();
                        Console.WriteLine("{0} lines read.", data.Count);
                    }
                }
            }
            Console.WriteLine("File read, {0} lines read.", data.Count);
		}

		public void normalize()
		{
			Console.WriteLine("NORMALIZING DATA--------------------------------");
            Console.WriteLine("0 lines normalized");
			for (var i = 0; i < data[0].Count; i++)
			{
				minimums.Add(data.Select(x => x[i]).Min());
				maximums.Add(data.Select(x => x[i]).Max());
			}

			foreach (var line in data)
			{
				
				System.Threading.Thread.Sleep(2);
				int lineIndex = data.IndexOf(line);
				List<double> lineToAdd = new List<double>();
				for(var i = 0; i < data[0].Count; i++)
				{	
					lineToAdd.Add((data[lineIndex][i] - minimums[i]) / (maximums[i] - minimums[i]));
				}
				if (normalizedData.Count % 100 == 0)
				{
					Console.SetCursorPosition(0, Console.CursorTop - 1);
					ClearCurrentConsoleLine();
					Console.WriteLine("{0} lines normalized", normalizedData.Count);
				}
				normalizedData.Add(lineToAdd);
			}
			Console.WriteLine("Data normalized");
		}

		public enum DataNames {
			gameId,
			leagueIndex,
			age,
			hoursPerWeek,
			totalHours,
			apm,
			selectByHotkey,
			assignToHotkey,
			uniqueHotkeys,
			minimapAttacks,
			minimapRightClicks,
			numberOfPACs,
			gapBetweenPACs,
			actionLatency,
			actionsInPAC,
			totalMapExplored,
			workersMade,
			uniqueUnitsMade,
			complexUnitsMade,
			complexAbilitiesUsed
		}

		public static void ClearCurrentConsoleLine()
		{
			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth)); 
			Console.SetCursorPosition(0, currentLineCursor);
		}
	}
}
