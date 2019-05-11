using System;
using System.Collections.Generic;
using System.Text;
using Cln.Application.Domain;

namespace Cln.Application.Todo.Entities
{
    public interface ITodoEntity : IEntity
    {
        int ListId { get; set; }
    }
}
