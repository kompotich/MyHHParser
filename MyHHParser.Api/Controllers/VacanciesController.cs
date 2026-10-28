using Microsoft.AspNetCore.Mvc;
using MyHHParser.Core.Models;
using MyHHParser.Core.Services;
using MyHHParser.Data.Repositories;

namespace MyHHParser.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VacanciesController : ControllerBase
{
	private readonly IVacancySearchService _vacancySearchService;
	private readonly IVacancyRepository _vacancyRepository;

	public VacanciesController(
		IVacancySearchService vacancySearchService,
		IVacancyRepository vacancyRepository)
	{
		_vacancySearchService = vacancySearchService;
		_vacancyRepository = vacancyRepository;
	}

	[HttpGet]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		var vacancies = await _vacancySearchService.GetVacanciesAsync(cancellationToken);
		await _vacancyRepository.SaveAsync(vacancies, cancellationToken);
		return Ok(vacancies);
	}

	[HttpPost]
	public async Task<IActionResult> Save(
		[FromBody] List<VacancyInfo> vacancies,
		CancellationToken cancellationToken)
	{
		if (vacancies.Any(vacancy => string.IsNullOrWhiteSpace(vacancy.ExternalId)))
		{
			return BadRequest("Every vacancy must have an external ID.");
		}

		await _vacancyRepository.SaveAsync(vacancies, cancellationToken);
		return NoContent();
	}
}
