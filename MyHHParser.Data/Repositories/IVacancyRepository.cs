using MyHHParser.Core.Models;

namespace MyHHParser.Data.Repositories;

public interface IVacancyRepository
{
	Task SaveAsync(IReadOnlyCollection<VacancyInfo> vacancies, CancellationToken cancellationToken = default);
}
