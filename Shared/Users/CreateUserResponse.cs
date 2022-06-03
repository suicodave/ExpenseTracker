using System.Collections;
using System.Collections.Generic;

namespace Shared.Users
{
    public class CreateUserResponse
    {
        public bool IsSuccessful { get; set; }

        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}