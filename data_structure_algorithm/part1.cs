using System;
public class TypeSafeList<T>
{
	T[] innerArray = new T[0];
	int currentSize = 0;
	int capacity = 0;

	public void Add(T item)
	{
		if(currentSize == capacity)
		{
			capacity = capacity == 0 ? 4 : capacity * 2;
			T[] copy = new T[capacity];
			Array.Copy(innerArray, copy, currentSize);
			innerArray = copy;
		}
		innerArray[currentSize] = item;
		currentSize++;
	}
	
	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= currentSize)
				throw new IndexOutOfRangeException();
			return innerArray[index];
		}
		set
		{
			if (index < 0 || index >= currentSize)
				throw new IndexOutOfRangeException();
			innerArray[index] = value;
		}
	}

	public override string ToString()
	{
		string output = string.Empty;
		for (int i=0; i < currentSize - 1; i++)
			output += innerArray[i] + ", ";

		return output + innerArray[currentSize - 1];
	}
	
}

//.exe entry point
public class Part1 
{
	public static void Main(string[] args)
	{
	}
}
