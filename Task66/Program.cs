// Задача 66: Задайте значения M и N. Напишите программу, которая найдёт сумму натуральных элементов в промежутке от M до N.
// Выполнить с помощью рекурсии.
// M = 1; N = 15-> 120
// M = 4; N = 8-> 30

do
{
	Console.Clear();
	PrintTitle("Вычисление суммы натуральных чисел в замкнутом промежутке между двумя заданными числами", ConsoleColor.Cyan);

	int m = GetUserInputInt("Введите первое натуральное число: ", 1);
	int n = GetUserInputInt("Введите второе натуральное число: ", 1);

	int sum = SumRange(m, n);

	Console.Write($"\nСумма всех натуральных чисел в диапазоне от {Math.Min(m, n)} до {Math.Max(m, n)} равна ");
	PrintColored(sum.ToString(), ConsoleColor.Yellow);
	Console.WriteLine();

} while (AskForRepeat());

// Methods

static int SumRange(int a, int b)
{
	if (a > b)
		return a + SumRange(a - 1, b);
	else if (a < b)
		return a + SumRange(a + 1, b);

	return a;
}

#region User Interaction Common

static int GetUserInputInt(string inputMessage, int minAllowed = int.MinValue, int maxAllowed = int.MaxValue)
{
	const string errorMessageWrongType = "Некорректный ввод! Требуется целое число. Пожалуйста повторите\n";
	string errorMessageOutOfRange = string.Empty;
	if (minAllowed != int.MinValue && maxAllowed != int.MaxValue)
		errorMessageOutOfRange = $"Число должно быть в интервале от {minAllowed} до {maxAllowed}! Пожалуйста повторите\n";
	else if (minAllowed != int.MinValue)
		errorMessageOutOfRange = $"Число не должно быть меньше {minAllowed}! Пожалуйста повторите\n";
	else
		errorMessageOutOfRange = $"Число не должно быть больше {maxAllowed}! Пожалуйста повторите\n";

	int input;
	bool notANumber = false;
	bool outOfRange = false;
	do
	{
		if (notANumber)
		{
			PrintError(errorMessageWrongType, ConsoleColor.Magenta);
		}
		if (outOfRange)
		{
			PrintError(errorMessageOutOfRange, ConsoleColor.Magenta);
		}
		Console.Write(inputMessage);
		notANumber = !int.TryParse(Console.ReadLine(), out input);
		outOfRange = !notANumber && (input < minAllowed || input > maxAllowed);

	} while (notANumber || outOfRange);

	return input;
}

static void PrintTitle(string title, ConsoleColor foreColor)
{
	int feasibleWidth = Console.BufferWidth;
	string titleDelimiter = new string('\u2550', feasibleWidth);
	PrintColored(titleDelimiter + Environment.NewLine + title + Environment.NewLine + titleDelimiter + Environment.NewLine, foreColor);
}

static void PrintError(string errorMessage, ConsoleColor foreColor)
{
	PrintColored("\u2757 Ошибка: " + errorMessage, foreColor);
}

static void PrintColored(string message, ConsoleColor foreColor)
{
	var bkpColor = Console.ForegroundColor;
	Console.ForegroundColor = foreColor;
	Console.Write(message);
	Console.ForegroundColor = bkpColor;
}

static bool AskForRepeat()
{
	Console.WriteLine();
	Console.WriteLine("Нажмите Enter, чтобы начать сначала или Esc, чтобы завершить...");
	ConsoleKeyInfo key = Console.ReadKey(true);
	return key.Key != ConsoleKey.Escape;
}

#endregion User Interaction Common
