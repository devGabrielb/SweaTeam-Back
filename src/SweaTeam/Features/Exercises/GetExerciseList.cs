using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ardalis.Result;
using Ardalis.Result.AspNetCore;

using Carter;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SweaTeam.Entities.Exercises;
using SweaTeam.Infra;

namespace SweaTeam.Features.Exercises
{


    public record GetExerciseListQuery : IRequest<Result<GetExerciseListResponse>>
    {

        public GetExerciseListQuery(int muscleGroupType, int page = 1, int pageSize = 10)
        {
            MuscleGroupType = muscleGroupType;
            Page = page;
            PageSize = pageSize;
        }
        public int MuscleGroupType { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public record GetExerciseListResponse(IEnumerable<Exercise> Exercises);

    internal sealed class Validator : AbstractValidator<GetExerciseListQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.MuscleGroupType).NotEmpty().GreaterThan(0);
        }
    }
    internal sealed class GetExerciseListHandler : IRequestHandler<GetExerciseListQuery, Result<GetExerciseListResponse>>
    {
        private readonly SweaTeamContext _context;
        private readonly IValidator<GetExerciseListQuery> _validator;

        public GetExerciseListHandler(SweaTeamContext context, IValidator<GetExerciseListQuery> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<GetExerciseListResponse>> Handle(GetExerciseListQuery request, CancellationToken cancellationToken)
        {

            var validationresult = await _validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (!validationresult.IsValid)
            {
                return Result.Invalid(validationresult.Errors.Select(e => new ValidationError(e.ErrorMessage)));
            }

            var exercises = await _context.Exercises
                .Where(x => request.MuscleGroupType == 0 || (int)x.PrimaryMuscleGroup == request.MuscleGroupType)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return Result.Success(new GetExerciseListResponse(exercises));
        }

    }
    public sealed class Endpoint : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapGet("api/exercises", async ([FromServices] ISender sender, int muscleGroupType, int page = 1, int pageSize = 10) =>
            {
                var getExerciseListQuery = new GetExerciseListQuery(muscleGroupType, page, pageSize);
                var result = await sender.Send(getExerciseListQuery).ConfigureAwait(false);
                return result.ToMinimalApiResult();
            });
        }
    }
}
