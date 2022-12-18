

namespace MyNamespace

{
	class Program
	{
		public static void Main()
		{
			bool gameLoop = true;
			Product snikers = new Product("Snikers", 45);
			Product alenka = new Product("Шоколад Аленка", 100);
			Product water05 = new Product("Вода 0.5л", 50);
			Product juice = new Product("Сок Здрайверы", 100500);
			Product skeleton = new Product("Йогурт Скелетоны", 2007);
			Product beer = new Product("Пиво Йоболонь", 80);
			Product bread = new Product("Хлiбъ Пшеничный", 30);
			Product motorOil = new Product("Машинное масло", 700);
			Product cyberTruck = new Product("Tesla Cybertruck", 100);
			Product[] newProducts = {snikers, alenka, water05, juice, skeleton, beer, bread, motorOil, cyberTruck};

			VendingMachine machine = new VendingMachine(newProducts);

			Buyer me;
			Console.WriteLine("Сколько взять денег?");
			try
			{
				me = new Buyer(int.Parse(Console.ReadLine()));
			}
			catch
			{
				Console.WriteLine("Видимо остаемся дома))");
				return;
			}

			while (gameLoop)
			{
				machine.ShowProducts();
				me.ShowBalance();
				machine.BuyProduct(me);
			}
		}
	}
	public class Buyer
	{
		public int Money { get; private set; }

		Buyer()
		{

		}
		public Buyer(int money) => Money = money;
		public void ShowBalance()
		{
			Console.WriteLine($"Ваш баланс: {Money} {VendingMachine.PluralizeRubles(Money)}.");
		}
		public bool SpendMoney(int count)
		{
			if (count > Money)
			{
				VendingMachine.ShowWarningNotice("Недостаточно денег");
				return false;
			}
			Money -= count;
			return true;
		}
	}
	public class Product
	{
		public string Name { get; }
		public int Price { get; }

		public Product()
		{
			Name = "UknownProduct";
			Price = 1;
		}
		public Product(string name, int price)
		{
			Name = name;
			Price = price;
			
		}
	}
	public class VendingMachine
	{
		public VendingMachine ()
		{

		}
		public VendingMachine(IEnumerable<Product> products)
		{
			Products = new List<Product>(products);
		}
		public List<Product>? Products { get; }

		public void ShowProducts()
		{
			if (Products == null)
			{
				Console.WriteLine("Автомат пуст");
				return;
			}
			Console.WriteLine("Выберите товар:");
			for (int i = 0; i < Products.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {Products[i].Name} - Цена {Products[i].Price} {PluralizeRubles(Products[i].Price)}.");
			}
		}
		public void BuyProduct(Buyer buyer)
		{
			int request = 0;
			try
			{
				request = int.Parse(Console.ReadLine());
			}
			catch
			{
				ShowWarningNotice("Нужно ввести целое число!");
				return;
			}
			if (request > Products.Count || request <= 0)
			{
				ShowWarningNotice("Товар с таким номером отсутствует");
				return;
			}
			if(buyer.SpendMoney(Products[request - 1].Price))
				ShowWarningNotice($"Вы купили {Products[request - 1].Name}", ConsoleColor.Green);
		}
		public static string PluralizeRubles(int count)
		{
			if ((count % 100) >= 11 && (count % 100) <= 19)
			{
				return "рублей";
			}
			else
			{
				if ((count % 10) == 1)
					return "рубль";

				else if ((count % 10) >= 2 && (count % 10) <= 4)
					return "рубля";
				else
					return "рублей";
			}
		}
		public static void ShowWarningNotice(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		public static void ShowWarningNotice(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}
