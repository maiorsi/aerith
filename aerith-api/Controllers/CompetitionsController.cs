using System;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    public class CompetitionsController : BaseController<Competition>
    {
        private readonly ILogger<CompetitionsController> _logger;
        private readonly IRepository<Competition> _repository;
        public CompetitionsController(ILogger<CompetitionsController> logger, IRepository<Competition> repository)
            : base(logger, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}