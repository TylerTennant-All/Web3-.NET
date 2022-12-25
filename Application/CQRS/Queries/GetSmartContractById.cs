﻿using Domain.Entities;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries
{
    public class GetSmartContractById : IEntity, IRequest<SmartContract>
    {
    }
}
