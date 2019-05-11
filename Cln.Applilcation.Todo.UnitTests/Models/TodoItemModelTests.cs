using Cln.Application.Todo.Models;
using Cln.Application.Common;
using FluentAssertions;
using Xunit;

namespace Cln.Applilcation.Todo.UnitTests
{
    public class TodoItemModelTests
    {
        [Theory]
        [InlineData("GoodTitle", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("getrgkudyctquiakabiudivcvyndqofttwiyzwdpyrdbyavxlutpjpdsrkldfokmgkyyuzgbmuqvfkwfewbtmplnnghtegrsobzrtqzpxaicegqxtxjmcinoekmgrcpl", false)] // 128 chars
        public void TodoItem_TestValidation(string testValue, bool expected)
        {
            var model = new TodoItemModel() { Id = 1, Title = testValue };

            model.IsValid().Should().Be(expected);
        }
    }
}
