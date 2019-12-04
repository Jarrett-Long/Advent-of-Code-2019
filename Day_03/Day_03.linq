<Query Kind="Program" />

void Main()
{
	var ex1Wire1 = new Line("R8,U5,L5,D3".Split(',')); 
	var ex1Wire2 = new Line("U7,R6,D4,L4".Split(','));
	Solutions.PartOne(ex1Wire1, ex1Wire2);
	Solutions.PartTwo(ex1Wire1, ex1Wire2);

	var file = System.IO.File.ReadAllLines(@"C:\Projects\AOC\Advent-of-Code-2019\Day_03\input.txt");
	var wire1Directions = new Line(file[0].Split(','));
	var wire2Directions = new Line(file[1].Split(','));
	Solutions.PartOne(wire1Directions, wire2Directions);
	Solutions.PartTwo(wire1Directions, wire2Directions);
}

public static class Solutions
{
	public static void PartOne(Line wire1, Line wire2)
	{
		var origin = new Point(0, 0);
		var interections = wire1.Points.Intersect(wire2.Points);
		var closestIntersection = interections.Min(i => Calculator.ManhattanDistance(origin, i));
		$"Closest intersection: {closestIntersection}".Dump();
	}
	
	public static void PartTwo(Line wire1, Line wire2)
	{
		var intersections = wire1.Points.Intersect(wire2.Points);
		var min = intersections.Min(i => wire1.Points.IndexOf(i) + wire2.Points.IndexOf(i) + 2);
		$"Fewest combined steps: {min}".Dump();
	}
}

public class Line
{
	public List<Point> Points { get; set; } = new List<Point>();
	
	public Point CurrentPosition { get; set; } = new Point(0, 0);
	
	public Line(string[] directions)
	{
		foreach (var direction in directions)
		{
			var dir = direction[0];
			var amt = int.Parse(direction.Substring(1));
			switch (dir)
			{
				case 'L':
					this.MoveLeft(amt);
					break;
				case 'R':
					this.MoveRight(amt);
					break;
				case 'U':
					this.MoveUp(amt);
					break;
				case 'D':
					this.MoveDown(amt);
					break;
			}
		}
	}

	public void MoveUp(int amount)
	{
		Enumerable.Range(0, amount).ToList().ForEach(_ => this.Points.Add(new Point(CurrentPosition.X, ++CurrentPosition.Y)));
	}

	public void MoveDown(int amount)
	{
		Enumerable.Range(0, amount).ToList().ForEach(_ => this.Points.Add(new Point(CurrentPosition.X, --CurrentPosition.Y)));
	}

	public void MoveLeft(int amount)
	{
		Enumerable.Range(0, amount).ToList().ForEach(_ => this.Points.Add(new Point(--CurrentPosition.X, CurrentPosition.Y)));
	}

	public void MoveRight(int amount)
	{
		Enumerable.Range(0, amount).ToList().ForEach(_ => this.Points.Add(new Point(++CurrentPosition.X, CurrentPosition.Y)));
	}
}

public class Point
{
	public int X { get; set; }
	
	public int Y { get; set; }
	
	public Point(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public override bool Equals(object obj)
	{
		if (obj is Point other) return this.X.Equals(other.X) && this.Y.Equals(other.Y);
		else return false;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hash = 17;
			hash = hash * 23 + X.GetHashCode();
			hash = hash * 23 + Y.GetHashCode();
			return hash;
		}
	}

	public override string ToString()
	{
		return $"({this.X}, {this.Y})";
	}
}

public static class Calculator
{
	public static int ManhattanDistance(Point p1, Point p2)
	{
		return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
	}
}
