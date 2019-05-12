﻿using Cln.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace Cln.Application.Todo.Models
{
    public class TodoListModel : TodoItemUpdateModel
    {
        [Required]
        public long Id { get; set; }
    }
}
