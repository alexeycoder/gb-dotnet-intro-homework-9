// Задача 68: Напишите программу вычисления функции Аккермана с помощью рекурсии. Даны два неотрицательных числа m и n.
// m = 3, n = 2 -> A(m,n) = 29

long callsCount;

do
{
	Console.Clear();
	PrintTitle("Вычисление функции Аккермана", ConsoleColor.Cyan);

	int mm = GetUserInputInt("Введите первый параметр (неотрицательное целое число): ", 0);
	int nn = GetUserInputInt("Введите второй параметр (неотрицательное целое число): ", 0);

	bool exceptionHappened = false;

	double ackValue = 0;
	callsCount = 0;
	var timer = System.Diagnostics.Stopwatch.StartNew();
	try
	{
		ackValue = Ackermann(mm, nn);
	}
	catch
	{
		exceptionHappened = true;
	}
	timer.Stop();

	if (exceptionHappened)
	{
		Console.WriteLine();
		PrintError($"Недостаточно ресурсов системы для вычисления значения функции Аккермана с указанными параметрами A({mm}, {nn})", ConsoleColor.Red);
	}
	else
	{
		Console.Write("\nВывод: ");
		PrintColored($"A({mm}, {nn}) = {ackValue}\n", ConsoleColor.Yellow);
	}

	PrintColored($"\nДостигнутое число вызовов функции: {callsCount: ### ### ### ###}", ConsoleColor.DarkGray);
	PrintColored($"\nЗатрачено времени: {timer.Elapsed.Minutes} мин {timer.Elapsed.Seconds} сек {timer.Elapsed.Milliseconds} мс", ConsoleColor.DarkGray);
	Console.WriteLine();

} while (AskForRepeat());

// Methods

double Ackermann(double m, double n)
{
	System.Runtime.CompilerServices.RuntimeHelpers.EnsureSufficientExecutionStack();
	++callsCount;

	if (m == 0)
		return n + 1;

	if (m > 0)
	{
		if (n > 0)
			return Ackermann(m - 1, Ackermann(m, n - 1));

		// n == 0
		return Ackermann(m - 1, 1);
	}

	return 0;
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
