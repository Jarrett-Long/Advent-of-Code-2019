void Main()
{
	var example1 = new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };
	Intcode.Execute(example1);
	// example1.Dump();

	var example2 = new int[] { 1, 0, 0, 0, 99 };
	Intcode.Execute(example2);
	// example2.Dump();

	var example3 = new int[] { 2, 3, 0, 3, 99 };
	Intcode.Execute(example3);
	// example3.Dump();

	var example4 = new int[] { 2, 4, 4, 5, 99, 0 };
	Intcode.Execute(example4);
	// example4.Dump();

	var example5 = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
	Intcode.Execute(example5);
	// example5.Dump();

	var file = System.IO.File.ReadAllText(@"C:\Projects\AOC\Advent-of-Code-2019\Day_02\input.txt");
	
	// part 1
	var part1 = Array.ConvertAll(file.Split(','), int.Parse);
	part1[1] = 12;
	part1[2] = 2;
	Intcode.Execute(part1);
	part1[0].Dump();
	
	// part 2
	var goalOutput = 19690720;
	var currentOutput = 0;
	
	foreach (var noun in Enumerable.Range(0, 99))
	{
		foreach (var verb in Enumerable.Range(0, 99))
		{
			var part2 = Array.ConvertAll(file.Split(','), int.Parse);
			part2[1] = noun;
			part2[2] = verb;
			
			Intcode.Execute(part2);
			
			currentOutput = part2[0];

			if (currentOutput == goalOutput)
			{
				$"Noun: {noun}".Dump();
				$"Verb: {verb}".Dump();
				$"100 * noun + verb = {100 * noun + verb}".Dump();
				break;
			}
		}
		
		if (currentOutput == goalOutput) break;
	}
}

public static class Intcode
{
	public static void Execute(int[] integers)
	{
		for (var i = 0; i < integers.Length; i++)
		{
			if (integers[i] == 1)
			{
				var pos1 = integers[i + 1];
				var pos2 = integers[i + 2];
				var pos3 = integers[i + 3];
				integers[pos3] = integers[pos1] + integers[pos2];
			}
			
			else if (integers[i] == 2)
			{
				var pos1 = integers[i + 1];
				var pos2 = integers[i + 2];
				var pos3 = integers[i + 3];
				integers[pos3] = integers[pos1] * integers[pos2];
			}
			
			else if (integers[i] == 99)
			{
				break;
			}
			
			i += 3;
		}
	}
}