// Задача 64: Напишите программу, которая выведет все натуральные числа в промежуке от N до 1. Выполнить с помощью рекурсии.
// N = 5 -> "5, 4, 3, 2, 1"
// N = 8 -> "8, 7, 6, 5, 4, 3, 2, 1"

do
{
	Console.Clear();
	PrintTitle("Вывод всех натуральных чисел в промежуке от заданного до 1", ConsoleColor.Cyan);

	int n = GetUserInputInt("Введите натуральное число: ", 1);

	PrintColored("\nВывод: ", ConsoleColor.DarkGray);
	PrintNaturalNumbersDescending(n);
	Console.WriteLine();

} while (AskForRepeat());

// Methods

static void PrintNaturalNumbersDescending(int num)
{
	if (num > 1)
	{
		Console.Write($"{num}, ");
		PrintNaturalNumbersDescending(num - 1);
	}
	else
	{
		Console.Write("1"); // no comma at the end
	}
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
