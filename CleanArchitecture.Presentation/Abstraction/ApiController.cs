﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Abstraction
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public abstract class ApiController : ControllerBase
    {
        public readonly IMediator _mediator;
        protected ApiController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }
    }
}
