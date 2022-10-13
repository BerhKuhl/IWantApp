using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Endpoints.Security;
public record LoginRequest(string Email, string Password);
