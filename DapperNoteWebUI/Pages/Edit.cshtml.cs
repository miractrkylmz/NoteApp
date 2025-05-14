using DapperNoteWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace DapperNoteWebUI.Pages
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public NoteDto Note { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("NoteAPI");
            var note = await client.GetFromJsonAsync<NoteDto>($"api/Notes/{id}");
            if(note == null)
            {
                return RedirectToPage("Index");
            }
            Note = note;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("NoteAPI");
            var response = await client.PutAsJsonAsync("api/Notes", Note);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("", "Note could not be updated.");
            return Page();
        }
    }
}
