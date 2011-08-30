using System;
public class TypeSafeList<T>
{
	T[] innerArray = new T[0];
	int currentSize = 0;
	int capacity = 0;
    private string name;

    public string Name
    {
        get
        {
            return name; 
        }
        set
        {
            if(value == string.Empty)
                name = "Unknow man!";
            else
                name = value;
        }
    }

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
        TypeSafeList<string> stringList = new TypeSafeList<string>();        
        stringList.Add("one");
        stringList.Add("two");
        stringList.Add("three");
        stringList.Add("four");
        
        stringList[0]+=" piggy";
        stringList.Name = ""; // this equals to string.Empty.

        System.Console.WriteLine(stringList);
        System.Console.WriteLine(stringList[0]);
        System.Console.WriteLine(stringList.Name);
	}
}
