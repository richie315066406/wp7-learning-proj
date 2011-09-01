//part2.cs
using System.Collections.Generic;
using System;

public class JobProccessing
{
	List<string> jobs = new List<string>();
	int nextJobPos = 0;

	public void AddJob(string str)
	{
		jobs.Add(str);
	}

	public string GetNextJob()
	{
		if (nextJobPos > jobs.Count - 1)
			return "**No job left**";
		else
		{
			return jobs[nextJobPos++];
		}
	}
}

public class Part2
{
	public static void Main(string[] args)
	{
		JobTester();	
	}

	public static void JobTester()
	{
		JobProccessing jp = new JobProccessing();
		jp.AddJob("job1");
		jp.AddJob("job2");
		jp.AddJob("job3");
		jp.AddJob("job4");
		
		for(int i=0; i<6; i++)
		{
			Console.WriteLine(jp.GetNextJob());	
		}

		jp.AddJob("job5");
		Console.WriteLine(jp.GetNextJob());	
	}
}
