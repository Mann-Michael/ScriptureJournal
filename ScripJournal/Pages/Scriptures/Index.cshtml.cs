using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScripJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScripJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScripJournal.Models.ScripJournalContext _context;

        public IndexModel(ScripJournal.Models.ScripJournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string BookFilter { get; set; }
        public string KeywordFilter { get; set; }
        public string CurrentSort { get; set; }

        //public IList<Scripture> Scripture { get;set; }
        public PaginatedList<Scripture> Scripture { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchBook { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKeyword { get; set; }

        public async Task OnGetAsync(string sortOrder, string CurrentBook, string CurrentKeyword, string SearchBook, string SearchKeyword, int? pageIndex)
        {
            CurrentSort = sortOrder;
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            BookFilter = SearchBook;
            KeywordFilter = SearchKeyword;

            if (SearchBook != null)
            {
                pageIndex = 1;
            }
            else if (SearchKeyword != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchBook = CurrentBook;
                SearchKeyword = CurrentKeyword;
            }

            var scriptures = from sc in _context.Scripture
                             select sc;

            if (!string.IsNullOrEmpty(SearchBook))
            {
                scriptures = scriptures.Where(s => s.Book.Contains(SearchBook));
            }

            if (!string.IsNullOrEmpty(SearchKeyword))
            {
                scriptures = scriptures.Where(s => s.Note.Contains(SearchKeyword));
            }

            switch (sortOrder)
            {
                case "book_desc":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    scriptures = scriptures.OrderBy(s => s.DateAdded);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                    scriptures = scriptures.OrderBy(s => s.Book);
                    break;
            }

            int pageSize = 5;
            Scripture = await PaginatedList<Scripture>.CreateAsync(
            scriptures.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
