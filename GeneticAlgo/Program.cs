﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo
{
	class Program
	{
		static void Main(string[] args)
		{
			FileParser fileParser = new FileParser("Dataset.csv");
			fileParser.Parse();
			fileParser.normalize();
		}
	}
}
