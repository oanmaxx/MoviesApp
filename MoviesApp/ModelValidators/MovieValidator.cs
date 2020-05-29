using FluentValidation;
using FluentValidation.Validators;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MoviesApp.ModelValidators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
		public MovieValidator()
		{
			//constructor
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Title).MinimumLength(2).MaximumLength(100);
			RuleFor(x => x.Director).MinimumLength(2).MaximumLength(100);
			RuleFor(x => x.Rating).InclusiveBetween(1, 10);
			RuleFor(x => x.YearOfRelease).GreaterThan(DateTime.Parse("1900-01-01"));
			//description, genre, duration in minute, year of release, director, date added, watched(bool)
		}
	}
}
