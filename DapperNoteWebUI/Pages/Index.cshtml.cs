using DapperNoteWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperNoteWebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public List<NoteDto> Notes = new();
        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("NoteAPI");
            var response = await client.GetFromJsonAsync<List<NoteDto>>("api/Notes");
            if (response != null)
            {
                Notes = response;
            }
        }
    }
}
