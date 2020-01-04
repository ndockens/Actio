using System.Net;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Actio.Frontend.Blazor.Data;

namespace Actio.Frontend.Blazor.Services
{
    public interface IAuthService
    {
        Task LoginAsync();
        Task<string> GetTokenAsync();
    }
}