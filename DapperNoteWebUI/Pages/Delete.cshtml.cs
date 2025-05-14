using DapperNoteWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperNoteWebUI.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public NoteDto Note { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("NoteAPI");
            var note = await client.GetFromJsonAsync<NoteDto>($"api/Notes/{id}");
            if (note == null)
            {
                return RedirectToPage("Index");
            }
            Note = note;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("NoteAPI");
            var response = client.DeleteAsync($"api/Notes/{Note.Id}");
            if (response == null)
            {
                return RedirectToPage("Index");
            }
            ModelState.AddModelError("", "Not silinemedi");
            return RedirectToPage("Index");
        }
    }
}