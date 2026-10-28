using MyHHParser.Core.Models;

namespace MyHHParser.Console;

internal static class VacancyConsolePrinter
{
	public static void Print(IReadOnlyList<VacancyInfo> vacancies)
	{
		if (vacancies.Count == 0)
		{
			System.Console.WriteLine("Вакансии не найдены.");
			return;
		}

		System.Console.WriteLine($"Найдено вакансий: {vacancies.Count}");

		for (var index = 0; index < vacancies.Count; index++)
		{
			var vacancy = vacancies[index];
			System.Console.WriteLine();
			System.Console.WriteLine($"{index + 1}. {vacancy.Name}");

			if (!string.IsNullOrWhiteSpace(vacancy.EmployerName))
			{
				System.Console.WriteLine($"   Компания: {vacancy.EmployerName}");
			}

			if (!string.IsNullOrWhiteSpace(vacancy.AreaName))
			{
				System.Console.WriteLine($"   Город: {vacancy.AreaName}");
			}

			System.Console.WriteLine($"   Зарплата: {FormatSalary(vacancy)}");

			if (!string.IsNullOrWhiteSpace(vacancy.Experience))
			{
				System.Console.WriteLine($"   Опыт: {vacancy.Experience}");
			}

			if (!string.IsNullOrWhiteSpace(vacancy.Url))
			{
				System.Console.WriteLine($"   Ссылка: {vacancy.Url}");
			}
		}
	}

	private static string FormatSalary(VacancyInfo vacancy)
	{
		if (vacancy.SalaryFrom is null && vacancy.SalaryTo is null)
		{
			return "не указана";
		}

		var amount = (vacancy.SalaryFrom, vacancy.SalaryTo) switch
		{
			(not null, not null) => $"{vacancy.SalaryFrom:N0}–{vacancy.SalaryTo:N0}",
			(not null, null) => $"от {vacancy.SalaryFrom:N0}",
			(null, not null) => $"до {vacancy.SalaryTo:N0}",
			_ => "не указана"
		};

		return string.IsNullOrWhiteSpace(vacancy.SalaryCurrency)
			? amount
			: $"{amount} {vacancy.SalaryCurrency}";
	}
}
