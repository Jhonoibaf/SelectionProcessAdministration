﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Recruiters.Application.DTOs;
using Recruiters.Application.Validators;
using Recruiters.Domain.Entities;
using Recruiters.Infraestructure.Data;
using Recruiters.Infraestructure.Models;

namespace Recruiters.Application.CandidatesAdministration.Commands
{
    public class CreateCandidateCommand
    {
        public record Command(CandidateDto Candidate) : IRequest<Candidate>;
        public class Handler : IRequestHandler<Command, Candidate>
        {
            private readonly ApplicationDbContext _dbcontext;
            private readonly IMapper _mapper;
            private readonly CandidateDtoValidator _validator;
            public Handler(ApplicationDbContext dbcontext, IMapper mapper)
            {
                _mapper = mapper;
                _dbcontext = dbcontext;
                _validator = new CandidateDtoValidator();
            }

            public async Task<Candidate> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var validationResult = _validator.Validate(request.Candidate);
                    if (!validationResult.IsValid)
                    {
                        List<string> errorMessages = [];

                        foreach(ValidationFailure error in validationResult.Errors)
                        {
                            errorMessages.Add(error.ErrorMessage);
                        }
                        throw new Exception(string.Join(",", errorMessages));
                    }

                    var candidate = _mapper.Map<Candidate>(request.Candidate);
                    candidate.Validate();

                    var candidateModel = new CandidateModel();
                    var candidateToCreate = _mapper.Map(request.Candidate, candidateModel);
                    if (candidateToCreate == null) throw new Exception("Candidate not be create");

                    _dbcontext.Add(candidateToCreate);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    var candidateCreated = _mapper.Map<Candidate>(candidateToCreate);
                    return candidateCreated;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new Exception($"{message}");
                }
            }
        }

        public record Response(Candidate Candidate);
    }
}
