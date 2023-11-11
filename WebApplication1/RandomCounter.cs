namespace WebApplication1
{
	public class RandomCounter: ICounter
	{
		static Random rnd = new Random();
		private int _value;
		public RandomCounter()
		{
			_value = rnd.Next(0, 100);
		}
		public int Value
		{
			get { return _value; }
		}
	}

	public interface ICounter
	{
		int Value { get; }
	}

	public class RandomCounter2 : ICounter
	{

		public int Value
		{
			get { return 25; }
		}
	}
}
